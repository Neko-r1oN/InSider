using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class TitleManager : MonoBehaviour
{
    [SerializeField] InputField nameField;
    [SerializeField] GameObject CavertText;
    [SerializeField] GameObject InputUI;
    [SerializeField] GameObject StartText;

    [SerializeField] Fade fade;

    static public string UserName { get; set; }
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;
    Color color;

    public static bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        fade.FadeOut(0.1f);
        audio = GetComponent<AudioSource>();
        isStart = false;
        nameField.Select();
        UserName = "";

        //this.audio.DOFade(endValue: 100f, duration: 0f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.Escape))
        {//ESC押した際の処理
#if UNITY_EDITOR
           
            //エディター実行時
            UnityEditor.EditorApplication.isPlaying = false;
#else
            //ビルド時
            Application.Quit();
#endif
        }
       

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            StartText.SetActive(false);
            InputUI.SetActive(true);
            audio.PlayOneShot(ClickSound);
            if (isStart == false)
            {
                Invoke("StartTrue", 2.0f);
            }
        }
    }
    public void StartButton()
    {

        if (nameField.text.Length == 0)
        {
            CavertText.SetActive(true);
            return;
        }
        GetComponent<SpriteRenderer>().DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);

        UserName = nameField.text;
        Initiate.DoneFading();

        //OnNextScene();




        fade.FadeIn(1f, () => SceneManager.LoadScene("StandbyScene_copy"));

    }
    public void BackButton()
    {
        InputUI.SetActive(false);
        StartText.SetActive(true);
    }

    void StartTrue()
    {
        isStart = true;
    }
    public void OnNextScene()
    {
        //fade.FadeIn(時間,() => 完了したときにやりたいこと);
        fade.FadeIn(1f, () => SceneManager.LoadScene("StandbyScene_copy"));

    }
}
