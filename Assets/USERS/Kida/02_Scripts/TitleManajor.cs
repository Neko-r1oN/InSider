using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManajor : MonoBehaviour
{
    public GameObject spaceText; //PushSpace
    public GameObject titleMenu; //タイトルメニュー
    [SerializeField] InputField nameField; //名前を入力するとこ
    [SerializeField] Text playerName; //表示用プレイヤーネーム
    static private string userName = "";　//プレイヤーネーム格納用

    public static string UserName
    {
        get { return userName; }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {//スペースを押すとメニューが出る
            spaceText.SetActive(false);
            titleMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {//ESCキーを押した場合
#if UNITY_EDITOR //Unityエディタの場合
            UnityEditor.EditorApplication.isPlaying = false;
#else //ビルドの場合
                Application.Quit();
#endif
        }

    }
    /// <summary>
    /// 名前入力を受け付けて表示
    /// </summary>
    public void OnNameClick()
    {
        userName = nameField.text;
        playerName.text = "Player: "+userName;

    }

    /// <summary>
    /// スタートボタンを押すと次のシーンへ
    /// </summary>
    public void OnStartClick()
    {
        Initiate.Fade("game", Color.black, 0.5f);
    }

    /// <summary>
    /// Endボタンを押すとソフト終了
    /// </summary>
    public void OnEndClick()
    {
#if UNITY_EDITOR //Unityエディタの場合
        UnityEditor.EditorApplication.isPlaying = false;
#else //ビルドの場合
                Application.Quit();
#endif
    }

    /// <summary>
    /// メニューを閉じる処理
    /// </summary>
    public void OnCloseClick()
    {
        titleMenu.SetActive(false);
        spaceText.SetActive(true);
    }


}
