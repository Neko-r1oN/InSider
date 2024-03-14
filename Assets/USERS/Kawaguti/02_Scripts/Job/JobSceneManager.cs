using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // UI
using Newtonsoft.Json;          // JSONのデシリアライズなど
using System.Net.Sockets;       // NetworkStreamなど
using System.Threading.Tasks;   // スレッドなど
using System.Threading;         // スレッドなど
using System.Linq;              // Skipメソッドなど
using UnityEngine.SceneManagement;  // シーン遷移
using System.Text;
using System;

public class JobSceneManager : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    private bool InSiderJodge = true;  //内通者判定(動作確認用)

    //テキスト
    [SerializeField] GameObject InSider;
    [SerializeField] GameObject Excavator;
    [SerializeField] Text YourText;
    [SerializeField] Text InSiderText;
    [SerializeField] Text ExcavatorText;

    //初めの色
    [SerializeField] Color32 startColor = new Color32(255, 255, 255, 0);

    //プレイヤーモデルのリスト
    [SerializeField] List<GameObject> playerModelList = new List<GameObject>();

    // 役職
    static public string job;

    // 先行のプレイヤーID
    static public int advancePlayerID;

    private void Start()
    {
        //表示切り替え時間を指定
        _repeatSpan = 0.5f;  
        _timeElapsed = 0;

        // 役職判定
        JobJadge();

        // 特定のプレイヤーモデルを表示する
        playerModelList[ClientManager.Instance.playerID].SetActive(true);    // スクリプトからIDを取得

        //テキストカラーを透明にする
        YourText.color = startColor;
        InSiderText.color = startColor;
        ExcavatorText.color = startColor;

        Invoke("SceneChange", 3.0f);
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;     //時間をカウントする

        //経過時間が繰り返す間隔を経過したら
        if (_timeElapsed >= _repeatSpan)
        {//時間経過でテキスト表示
            YourText.color = Color.Lerp(YourText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }    
        if (_timeElapsed >= _repeatSpan+1.0f)
        {//時間経過でテキスト表示(役職)
            InSiderText.color = Color.Lerp(InSiderText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
            ExcavatorText.color = Color.Lerp(ExcavatorText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }
    }

    //内通者判定関数(サーバー完成次第撤去)
    private void JobJadge()
    {
        if (ClientManager.Instance.isInsider == false)
        {// 発掘家の場合
            InSider.SetActive(false);
            Excavator.SetActive(true);
        }
        else
        {// 内密者の場合
            InSider.SetActive(true);
            Excavator.SetActive(false);
        }
    }

    //シーン切り替え
    public void SceneChange()
    {
        // フェード＆シーン遷移
        Initiate.DoneFading();

        Debug.Log("現在のラウンド数：" + ClientManager.Instance.roundNum);

        if (ClientManager.Instance.roundNum == 1)
        {
            Initiate.Fade("GameStage1", Color.black, 1f);
        }
        else if (ClientManager.Instance.roundNum == 2)
        {
            Initiate.Fade("GameStage2", Color.black, 1f);
        }
        else if (ClientManager.Instance.roundNum == 3)
        {
            Initiate.Fade("GameStage3", Color.black, 1f);
        }
    }
}
