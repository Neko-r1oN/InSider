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

    // 道ごとの説明テキストのリスト
    [SerializeField] List<GameObject> roadTextUIList;

    // ダウトの補足テキスト
    [SerializeField] Text supplementText;

    // サボタージュ(爆弾)のボタン
    [SerializeField] GameObject sabotage1;

    // サボタージュ(埋める)のボタン
    [SerializeField] GameObject sabotage2;

    // サボタージュ(スロートラップ)のボタン
    [SerializeField] GameObject sabotage3;

    [SerializeField] public GameObject sabotageCoolTime;

    // スタミナ不足UI
    [SerializeField] GameObject noStaminaUI;

    // 情報を取得
    GameObject player;
    GameObject roadManager;
    UIManager uiManager;
    TextUIManager textUI;
    GameObject cameraManager;
    GameObject eventManager;
    [SerializeField] public GameObject sabotageUI;

    // サボタージュのカウントを使うために情報を格納
    [SerializeField] GameObject sabotage;

    // ランダム関数
    System.Random rnd = new System.Random();

    // ランダムの数値を入れるための変数
    int rand;

    int randRoad;

    public bool isCancel;

    // スタミナ消費分の数値を決める変数
    public int subStamina;

    // ダウトを使用したかどうか
    bool isUseDoubt;

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
        uiManager = uiManagerObject.GetComponent<UIManager>();

        // テキストUIマネージャーを取得
        GameObject textUIObject = GameObject.Find("TextUIManager");
        textUI = textUIObject.GetComponent<TextUIManager>();

        // カメラマネージャーを取得
        cameraManager = GameObject.Find("CameraManager");

        // イベントマネージャー
        eventManager = GameObject.Find("EventManager");

        // 偽
        isUseDoubt = false;

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

        // サボタージュUIを非表示にする
        sabotageUI.SetActive(false);

        isCancel = false;

        // キャンセルボタンを非表示 
        canselButton.SetActive(false);

        // スタミナ不足UIを非表示にする
        noStaminaUI.SetActive(false);

        sabotageCoolTime.SetActive(false);
    }

    private void Update()
    {
        if(sabotage.GetComponent<Sabotage>().isBomb)
        {// サボタージュのボムカウントが1以上なら
            // サボタージュのボムを使えなくする
            uiManager.OutSabotage(0);
        }

        if(sabotage.GetComponent<Sabotage>().isFill)
        {// サボタージュの埋めるカウントが1以上なら
            // サボタージュの埋めるを使えなくする
            uiManager.OutSabotage(1);
        }

        if(sabotage.GetComponent<Sabotage>().isTrap)
        {
            uiManager.OutSabotage(2);
        }

        randRoad = rnd.Next(1, 101); // 1～100までのランダムの数値
    }

    public void PlayerMove()
    {
        // プレイヤーのモードをMOVEに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;

        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // 行動説明テキストを非表示
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
        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // 行動説明テキストを非表示
        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中 || 制限時間が0以下の場合
            return;
        }

        // プレイヤーのモードをMININGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;

        // キャンセルボタンを表示
        canselButton.SetActive(true);

        if (roadUI == true)
        {// RoadUIが表示されていたら

            // 道UIの回転を元に戻す
            uiManager.ResetRoadUI();

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

        // スタミナがない場合 
        if (player.GetComponent<Player>().stamina < 20)
        {
            noStaminaUI.SetActive(true);

            return;
        }

        // 全てのボタンを非表示
        HideButton();

        // キャンセルボタンを表示
        canselButton.SetActive(true);

        // 行動説明テキストを非表示
        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false
            || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中の場合 || 制限時間が0以下の場合
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

        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // 行動説明テキストを非表示にする
        textUI.HideText();

        if (player.GetComponent<Player>().isEnd == false || TimeUI.Instance.nowTime <= 0)
        {// プレイヤーが移動中の場合 || 制限時間が0以下の場合
            return;
        }

        // プレイヤーのモードをNOTHINGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        rand = rnd.Next(0, 71); // 0～70までのランダムの数値

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

    /// <summary>
    /// サボタージュのボタン表示
    /// </summary>
    public void Sabotage()
    {
        // サボタージュの行動ボタン表示
        sabotageUI.SetActive(true);
        
        if(sabotage.GetComponent<Sabotage>().timeNum > 0)
        {
            //DisplayCoolTime();
            sabotageCoolTime.SetActive(true);
        }

        textUI.HideText();

        // その他ボタン削除
        HideButton();
    }

    public void ButtonCancel()
    {// キャンセルボタンを選んだ場合

        textUI.saboText.SetActive(false);

        if (roadUI == true)
        {// RoadUIが表示されていたら
            roadUI.SetActive(false);

            // その他のボタンを表示
            moveButton.SetActive(true);
            fillButton.SetActive(true);
            nothingButton.SetActive(true);
            actionButton.SetActive(true);

            // サボタージュUIを非表示にする
            sabotageUI.SetActive(false);

            sabotageCoolTime.SetActive(false);

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

            // 埋める場所の選択を無くする
            for(int i = 0;i< roadManager.GetComponent<RoadManager>().selectPanelList.Count; i++)
            {
                // 選択した回数を0にする
                roadManager.GetComponent<RoadManager>().selectPanelList[i].GetComponent<RoadPanel>().isFillSelect = false;

                // 選択した回数を0にする
                roadManager.GetComponent<RoadManager>().selectPanelList[i].GetComponent<RoadPanel>().isbombSelect = false;

                // 選択した場所のカラー変更を元に戻す
                roadManager.GetComponent<RoadManager>().selectPanelList[i].GetComponent<RoadPanel>().isColor = false;
            }
        }

        // リストの中身・カウントを初期化
        roadManager.GetComponent<RoadManager>().selectPanelList = new List<GameObject>();
        roadManager.GetComponent<RoadManager>().selectPanelCount = 0;

        textUI.GetComponent<TextUIManager>().PutNum(0);

        // サボタージュのカウントを戻す
        sabotage.GetComponent<Sabotage>().ResetBool();

        // キャンセルボタンを非表示にする
        canselButton.SetActive(false);

        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // プレイヤーのモードを元に戻す
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    /// <summary>
    /// ボタンの表示処理
    /// </summary>
    public void DisplayButton()
    {
        // ボタンを表示
        moveButton.SetActive(true);
        fillButton.SetActive(true);
        nothingButton.SetActive(true);
        actionButton.SetActive(true);

        // キャンセルボタン・テキスト背景を非表示
        canselButton.SetActive(false);
        //textBack.SetActive(false);

        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // 全ての道UIのテキストを非表示
        for (int i = 0;i < roadTextUIList.Count; i++)
        {
            roadTextUIList[i].SetActive(false);
        }

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

    /// <summary>
    /// ボタンの非表示処理
    /// </summary>
    public void HideButton()
    {
        // その他のボタンを非表示
        moveButton.SetActive(false);
        fillButton.SetActive(false);
        nothingButton.SetActive(false);
        actionButton.SetActive(false);

        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

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

    /// <summary>
    /// 道UIの回転処理
    /// </summary>
    public void RotRoad()
    {
        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // 道・道UIを回転
        roadManager.GetComponent<RoadManager>().AddRotButton();
        uiManager.RotRoadUI();
    }

    /// <summary>
    /// カメラの切り替え処理
    /// </summary>
    public void ChangeCamera()
    {
        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        // カメラ切り替え
        cameraManager.GetComponent<CameraManager>().SwitchCamera();
    }

    /// <summary>
    /// ダウトボタン
    /// </summary>
    /// <param name="indexNumber"></param>
    public async void DoubtButton(int indexNumber)
    {
        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        if (EditorManager.Instance.useServer == true)
        {
            foreach (int num in uiManager.disabledIndexNumList)
            {
                if (num == indexNumber)
                {// 使用できないダウトのボタンが押されている場合
                    return;
                }
            }

            if (isUseDoubt == false)
            {
                isUseDoubt = true;

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

    /// <summary>
    /// 道の説明テキストの表示処理
    /// </summary>
    /// <param name="num"></param>
    public void DisplayRoadText(int num)
    {
        //textBack.SetActive(true);
        roadTextUIList[num].SetActive(true);
    }

    // <summary>
    /// 道の説明テキストの非表示処理
    /// </summary>
    /// <param name="num"></param>
    public void HideRoadText(int num)
    {
        // スタミナ不足UIを非表示
        noStaminaUI.SetActive(false);

        //textBack.SetActive(false);
        roadTextUIList[num].SetActive(false);
    }

    /// <summary>
    /// 混乱時にランダムで道を生成
    /// </summary>
    public void randChaosRoad()
    {
        roadManager.GetComponent<RoadManager>().Road(randRoad);
    }

    //public void DisplayCoolTime()
    //{
    //    sabotageCoolTime.SetActive(true);

    //    if(sabotage.GetComponent<Sabotage>().timeNum > 0)
    //    {
    //        sabotage.GetComponent<Sabotage>().InvokeRepeating("SubCoolTime", 0, 1);
    //    }

    //    Debug.Log("aa");
    //}
}
