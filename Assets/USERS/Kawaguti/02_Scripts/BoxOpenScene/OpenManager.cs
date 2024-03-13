using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera1;
    [SerializeField]
    private GameObject Camera2;

    AudioSource audio;
    //[SerializeField] AudioClip ClickSound;


    [SerializeField] AudioClip WinBGM;
    [SerializeField] AudioClip LoseBGM;

    public AudioSource Default;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource Music1;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
  

    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    bool Once;

    // Update処理を開始
    public bool isStart { get; set; } = false;

    // 宝箱とミミックのオブジェクト
    [SerializeField] GameObject Chest;
    [SerializeField] GameObject Mimic;

    // 勝敗テキスト
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;

    // プレイヤーUIのリスト
    [SerializeField] List<GameObject> playerUIList;

    // プレイヤーの名前リスト
    [SerializeField] List<Text> playerNameList;

    // トータルスコアのリスト
    [SerializeField] List<Text> totarScoreList;

    // 加減するスコアのリスト
    [SerializeField] List<Text> allieScoreList;

    // 役職のロゴマークUIのリスト
    [SerializeField] List<GameObject> insiderLogoMarkUI;
    [SerializeField] List<GameObject> excavatorLogoMarkUI;

    // インサイダーのオブジェクトのリスト
    List<GameObject>[] insiderListParent = new List<GameObject>[3];
    [SerializeField] List<GameObject> insiderList1;
    [SerializeField] List<GameObject> insiderList2;
    [SerializeField] List<GameObject> insiderList3;

    // 宝箱を開けるプレイヤーのリスト
    [SerializeField] List<GameObject> openPlayerList;

    // ミミックかどうかの判定
    public bool isMimic { get; set; }

    // シングルトン用
    public static OpenManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Default.Play();
        audio = GetComponent<AudioSource>();

        Music1 = GetComponent<AudioSource>();

        Camera1.SetActive(true);
        Camera2.SetActive(true);

        //表示切り替え時間を指定
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

        isMimic = true;
        Once = false;

        // 配列に格納する
        insiderListParent[0] = insiderList1;
        insiderListParent[1] = insiderList2;
        insiderListParent[2] = insiderList3;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart == false)
        {
            return;
        }

        _timeElapsed += Time.deltaTime;     //時間をカウントする


        if (_timeElapsed  >= _repeatSpan)
        {//時間経過でカメラ変更
           Camera1.SetActive(false);
            //Camera1.SetActive(!Camera1.activeSelf);
           
        }
        if (_timeElapsed  >= _repeatSpan + 1.0f)
        {
            Camera2.SetActive(false);
            //Camera2.SetActive(!Camera2.activeSelf);
        }

        if (_timeElapsed  >= _repeatSpan + 2.5f  && !Once)
        {
            if(!isMimic)
            {
                Default.Stop();
                Music1.PlayOneShot(WinBGM);
            }
            else if(isMimic)
            {
                Default.Stop();
                Music1.PlayOneShot(LoseBGM);
            }
            Once = true;
        }
    }

    /// <summary>
    /// Insiderのオブジェクトを設定 & 宝箱を開けるプレイヤー設定 & ミミックかどうか
    /// </summary>
    public void SetPlayerAndMimic(List<int> insiderID, int openPlayerID,bool ismimic,List<int> totalScore,List<int> allieScore)
    {
        for (int i = 0; i < insiderID.Count; i++)
        {
            List<GameObject> targetList = insiderListParent[i]; // リストを取り出す

            // 表示・非表示
            targetList[insiderID[i]].SetActive(true);           // プレイヤー
            insiderLogoMarkUI[insiderID[i]].SetActive(true);    // Insiderロゴマーク
            excavatorLogoMarkUI[insiderID[i]].SetActive(false); // 採掘者のロゴマーク
        }

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i >= ClientManager.Instance.playerNameList.Count)
            {// 存在しないプレイヤーUIを削除する
                Destroy(playerUIList[i]);
            }
            else
            {// プレイヤーが存在する場合
                playerNameList[i].text = ClientManager.Instance.playerNameList[i];  // 名前更新
                totarScoreList[i].text = "" + totalScore[i];   // 合計スコア更新

                if (allieScore[i] > 0)
                {// ＋の場合
                    allieScoreList[i].text = "(+" + allieScore[i] + ")";   // 加算するスコア数を代入する
                }
                else
                {// -の場合
                    allieScoreList[i].text = "(" + allieScore[i] + ")";    // 減算するスコア数を代入する
                }
            }
        }

        // 宝箱を開けるプレイヤーを表示する
        openPlayerList[openPlayerID].SetActive(true);

        // 代入する
        isMimic = ismimic;

        // オブジェクトを表示する
        if (ismimic == true)
        {// ミミックの場合
            Mimic.SetActive(true);

            // ハズレ
            loseText.SetActive(true);
        }
        else
        {// 宝箱の場合
            Chest.SetActive(true);

            // あたり
            winText.SetActive(true);
        }

        // Update処理を開始
        isStart = true;
    }
}
