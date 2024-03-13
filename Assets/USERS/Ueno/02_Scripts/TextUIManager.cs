using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextUIManager : MonoBehaviour
{
    //[SerializeField] List<GameObject> textBackList;  // テキスト背景のオブジェクト
    [SerializeField] List<GameObject> textList;      // 行動テキストリスト

    // サボタージュでのいくつ置いたか確認するテキスト
    [SerializeField] public GameObject saboText;

    // サボタージュでのいくつ置けるかのテキスト
    Text saboPosstext;

    // サボタージュでのいくつ置く場所を選択したかのテキスト
    Text saboPlaceText;

    private void Awake()
    {
        // テキストを非表示にする前にサボタージュ回数表示テキストを取得
        saboPosstext = GameObject.Find("SaboPossNum").GetComponent<Text>();
        saboPlaceText = GameObject.Find("SaboPlaceNum").GetComponent<Text>();
    }

    private void Start()
    {
        // テキストを非表示にする
        saboText.SetActive(false);
    }

    /// <summary>
    /// カーソルがあってる時に表示する
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseEnter(int num)
    {
        // テキストの背景を表示
        //textBack.SetActive(true);

        // 表示されていた説明文を非表示
        textList[num].SetActive(true);
    }

    /// <summary>
    /// カーソルが外れたときに非表示にする
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseExit(int num)
    {
        // テキストの背景を非表示
        //textBack.SetActive(false);
        // 表示されていた説明文を非表示
        textList[num].SetActive(false);
    }


    /// <summary>
    /// ボタンが押されたときに非表示にする
    /// </summary>
    public void HideText()
    {
        // テキストの背景を非表示
        //textBack.SetActive(false);

        // サボタージュのテキストを非表示にする
        saboText.SetActive(false);

        // 説明文を非表示
        for (int i = 0;i < textList.Count; i++)
        {
            textList[i].SetActive(false);
        }
    }

    /// <summary>
    /// 埋める道・爆弾の設置位置を選択できる数の表示処理
    /// 引数にいくつ置けるかの最大数を渡す
    /// </summary>
    public void PossibleNum(int num)
    {
        saboPosstext.text = "/ " + num;
    }

    /// <summary>
    /// 埋める道・爆弾の設置位置を選択した数の表示処理
    /// 引数にいくつ置いたかを渡す
    /// </summary>
    /// <param name="num"></param>
    public void PutNum(int num)
    {
        saboPlaceText.text = "" + num;
    }
}
