using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// ゲームオブジェクト諸々
    /// </summary>
    [SerializeField] InputField nameField;
    [SerializeField] GameObject CavertText;
    [SerializeField] GameObject NGText;
    [SerializeField] GameObject InputUI;
    [SerializeField] GameObject StartText;


    /// <summary>
    ///NGワード一覧(閲覧注意)
    /// </summary> 
    static Dictionary<string, int> NGwords = new Dictionary<string, int>()
    {
         { "しね",1}, { "しぬ",2},{ "殺",3}, { "クソ",4},{ "くそ",5}, { "バカ",6},{ "ばか",7}, { "馬鹿",8},{ "カス",9}, { "かす",10},
          { "きちがい",11}, { "キチガイ",12},{ "基地外",13}, { "気違い",14},{ "き印",15}, { "クズ",16},{ "くず",17}, { "ゲス",18},{ "鬼畜",19}, { "ﾀﾋ",20},
           { "ブタ",21}, { "デブ",22},{ "ブス",23}, { "うんち",24},{ "うんこ",25}, { "出会い",26},{ "主人",27}, { "レイプ",28},{ "エロ",29}, { "えっち",30},
            { "エッチ",31}, { "変態",32},{ "快感",33}, { "おっぱい",34},{ "オッパイ",35}, { "まんこ",36},{ "マンコ",37}, { "ﾏﾝｺ",38},{ "谷間",39}, { "あそこ",40},
             { "ぼっき",41}, { "ボッキ",42},{ "勃起",43}, { "ちんちん",44},{ "ﾁﾝﾁﾝ",45}, { "ちんこ",46},{ "ﾁﾝｺ",47}, { "ちんぽ",48},{ "ﾁﾝﾎﾟ",49}, { "ザーメン",50},
              { "射精",51}, { "中だし",52},{ "中出し",53}, { "あなる",54},{ "アナル",55}, { "ｱﾅﾙ",56},{ "パンツ",57}, { "ﾊﾟﾝﾂ",58},{ "ぬいで",59}, { "脱いで",60},
               { "ぬげ",61}, { "脱げ",62},{ "おまわり",63}, { "ニート",64},{ "ペロペロ",65}, { "こうかん",66},
    };
   

    //ユーザーネーム
    static public string UserName { get; set; }

    //click音用
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    

    public static bool isStart = false;
    private bool NGJudge;
   
    // Start is called before the first frame update
    void Start()
    {
        NGText.SetActive(false);
        CavertText.SetActive(false);
        //fade.FadeOut(1.0f);
        audio = GetComponent<AudioSource>();
        
        //初期化
        isStart = false;
        NGJudge = false;
        nameField.Select();
        UserName = "";
    }

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
        //NameFieldテキストを代入
        UserName = nameField.text;

        if (nameField.text.Length == 0)
        {//入力文字数が0文字だった場合
            NGText.SetActive(false);
            CavertText.SetActive(true);
            return;
        }
       

        // foreach で Keyと同じ値があるか確認
        foreach (var keyValuePair in NGwords)
        {
            if (UserName.Contains(keyValuePair.Key))
            {// 受け取った文字列にKeyの値が含まれている場合

                NGJudge = false;
                NGText.SetActive(true);
                CavertText.SetActive(false);
               return;
            }
            else
            {//問題ない場合
                NGJudge = true;
            }
          
        }

        Invoke("SceneLoad", 0.1f);

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
    private async void SceneLoad()
    {
        if (NGJudge == true)
        {    
            Initiate.Fade("StandbyScene_copy", Color.black, 1.0f);
        }
    }
}

