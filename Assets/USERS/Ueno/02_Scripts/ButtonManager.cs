using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour
{
    //--------------------------------------
    //ゲームオブジェクト
    //--------------------------------------
    [SerializeField] GameObject roadUI;         // RoadUIオブジェクトの取得
    [SerializeField] GameObject actionButton;   // actionボタンの取得
    [SerializeField] GameObject fillButton;     // fillボタンの取得
    [SerializeField] GameObject moveButton;     // moveボタンの取得
    [SerializeField] GameObject nothingButton;  // nothingボタンの取得
    [SerializeField] GameObject sabotageButton; // sabotageボタンの取得
    [SerializeField] public GameObject canselButton;   // canselボタンの取得

    // ダウトボタンのリスト
    [SerializeField] List<GameObject> doubtButton;

    // ダウトの補足テキスト
    [SerializeField] Text supplementText;

    // 情報を取得
    GameObject player;
    GameObject roadManager;
    UIManager uIManager;
    TextUIManager textUI;
    GameObject cameraManager;
    public GameObject sabotage;

    // ランダム関数
    System.Random rnd = new System.Random();

    // スタミナ
    int stamina = 100;

    // ランダムの数値を入れるための変数
    int rand;

    public bool isCancel;

    // スタミナ消費分の数値を決める変数
    public int subStamina;

    // ダウトを使用したかどうか
    bool isDoubt;
    
    private void Start()
    {
        // 情報を取得
        if (EditorManager.Instance.useServer)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        // ロードマネージャーを取得
        roadManager = GameObject.Find("RoadManager");
        
        // UIマネージャーを取得
        GameObject uiManagerObject = GameObject.Find("UIManager");
        uIManager = uiManagerObject.GetComponent<UIManager>();

        // テキストUIマネージャーを取得
        GameObject textUIObject = GameObject.Find("TextUIManager");
        textUI = textUIObject.GetComponent<TextUIManager>();

        // カメラマネージャーを取得
        cameraManager = GameObject.Find("CameraManager");

        // 偽
        isDoubt = false;
        // サボタージュUIを取得
        sabotage = GameObject.Find("SabotageUI");

        // サボタージュUIを非表示にする
        sabotage.SetActive(false);
        // プレイヤーのモードをNOTHINGに設定
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            sabotageButton.SetActive(true); // true : サボタージュUI表示
        }
        else
        {
            if (ClientManager.Instance.isInsider == false)
            {// 自分自身が発掘者の場合
                sabotageButton.SetActive(false);
            }
            else
            {// 自分自身が内通者の場合
                sabotageButton.SetActive(true);
            }
        }

        isCancel = false;
    }

    public void PlayerMove()
    {
        // プレイヤーのモードをMOVEに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;

        textUI.HideText();
    }

    public void CutOpen()
    {//切り開くを選んだ場合

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (ClientManager.Instance.turnPlayerID != ClientManager.Instance.playerID)
            {// 自身のターンではない場合
                return;
            }
        }

        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中 || 制限時間が0以下の場合
            return;
        }

        // プレイヤーのモードをMININGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;

        canselButton.SetActive(true);

        if (roadUI == true)
        {// RoadUIが表示されていたら

            uIManager.ResetRoadUI();

            // その他のボタンを非表示
            moveButton.SetActive(false);
            fillButton.SetActive(false);
            nothingButton.SetActive(false);
            actionButton.SetActive(false);

            if (EditorManager.Instance.useServer == false)
            {// サーバーを使用しない場合
                sabotageButton.SetActive(false); // true : サボタージュUI表示
            }
            else
            {
                if (ClientManager.Instance.isInsider == true)
                {// 自分自身が内通者の場合
                    sabotageButton.SetActive(false);
                }
            }
        }
    }

    public void fill()
    {//埋めるを選んだ場合

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (ClientManager.Instance.turnPlayerID != ClientManager.Instance.playerID)
            {// 自身のターンではない場合
                return;
            }
        }

        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false
            || player.GetComponent<Player>().stamina < 20 || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中の場合 || スタミナがない場合 || 制限時間が0以下の場合
            return;
        }

        // プレイヤーのモードをFILLに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
    }

    public async void Nothing()
    {// 何もしないを選んだ場合

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (ClientManager.Instance.turnPlayerID != ClientManager.Instance.playerID)
            {// 自身のターンではない場合
                return;
            }
        }

        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中の場合 || 制限時間が0以下の場合
            return;
        }

        // プレイヤーのモードをNOTHINGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        rand = rnd.Next(0, 71); // 0～30までのランダムの数値

        // スタミナを増やす
        player.GetComponent<Player>().AddStamina(rand);

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            // クラス変数を生成
            Action_NothingData restData = new Action_NothingData();
            restData.playerID = ClientManager.Instance.playerID;
            restData.addStamina = rand;
            restData.totalStamina = player.GetComponent<Player>().stamina;

            // 送信する
            await ClientManager.Instance.Send(restData, 8);
        }
    }

    public void Sabotage()
    {
        sabotage.SetActive(true);
    }

    public void ButtonCancel()
    {// キャンセルボタンを選んだ場合
        if (roadUI == true)
        {// RoadUIが表示されていたら
            roadUI.SetActive(false);

            // その他のボタンを表示
            moveButton.SetActive(true);
            fillButton.SetActive(true);
            nothingButton.SetActive(true);
            actionButton.SetActive(true);

            sabotage.SetActive(false);

            if (EditorManager.Instance.useServer == false)
            {// サーバーを使用しない場合
                sabotageButton.SetActive(true); // true : サボタージュUI表示
            }
            else
            {
                if (ClientManager.Instance.isInsider == true)
                {// 自分自身が内通者の場合
                    sabotageButton.SetActive(true);
                }
            }

            for(int i = 0;i< roadManager.GetComponent<RoadManager>().blokObjList.Count; i++)
            {
                roadManager.GetComponent<RoadManager>().blokObjList[i].GetComponent<RoadPanel>().isSelect = false;
            }

            // リストの中身・カウントを初期化
            roadManager.GetComponent<RoadManager>().blokObjList = new List<GameObject>();
            roadManager.GetComponent<RoadManager>().fillCount = 0;
        }

        canselButton.SetActive(false);

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public void DisplayButton()
    {
        // ボタンを表示
        moveButton.SetActive(true);
        fillButton.SetActive(true);
        nothingButton.SetActive(true);
        actionButton.SetActive(true);

        canselButton.SetActive(false);

        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            sabotageButton.SetActive(true); // true : サボタージュUI表示
        }
        else
        {
            if (ClientManager.Instance.isInsider == true)
            {// 自分自身が内通者の場合
                sabotageButton.SetActive(true);
            }
        }
    }

    public void RotRoad()
    {// 道・道UIを回転
        roadManager.GetComponent<RoadManager>().AddRotButton();
        uIManager.RotRoadUI();
    }

    public void ChangeCamera()
    {// カメラ切り替え
        cameraManager.GetComponent<CameraManager>().SwitchCamera();
    }

    /// <summary>
    /// ダウトボタン
    /// </summary>
    /// <param name="indexNumber"></param>
    public async void DoubtButton(int indexNumber)
    {
        if (isDoubt == false)
        {
            isDoubt = true;

            // テキストを更新する
            supplementText.text = "※ 使用済み";

            DoubtData doubtData = new DoubtData();
            doubtData.playerID = ClientManager.Instance.originalID;
            doubtData.targetID = indexNumber;

            // サーバーに送信する
            await ClientManager.Instance.Send(doubtData, 11);
        }
    }
}
