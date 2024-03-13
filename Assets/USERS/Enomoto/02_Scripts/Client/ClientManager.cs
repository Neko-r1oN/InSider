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

public class ClientManager : MonoBehaviour
{
    //====================================
    //  [Unityインスペクター]フィールド
    //====================================

    // PlayerNameテキストリスト
    public List<GameObject> clientTextList = new List<GameObject>();

    // Playerモデルのリスト
    public List<GameObject> modelList = new List<GameObject>();

    // 準備完了テキストのリスト
    public List<GameObject> readyTextList = new List<GameObject>();

    // リスナーデータリスト
    List<ListenerData> listenerList = new List<ListenerData>();

    // ボタンリスト
    [SerializeField] List<GameObject> buttonObjList;

    // フェード用シリアライズフィールド
    //[SerializeField] Fade fade;

    //===========================
    //  [非公開]フィールド
    //===========================

    // メインスレッドに処理実行を依頼するもの
    SynchronizationContext context;

    // NetworkStreamを使用
    NetworkStream stream;

    // 準備完了(OK)ボタンを押したかどうか
    bool isReadyButton = false;

    // 接続を切断して終了するかどうか
    bool isDisconnect = false;

    // クライアントの名前
    string clientName;

    // プレイヤーのマネージャー(List)
    GameObject playerManager;

    // ブロック(道パネル)マネージャー
    GameObject blockManager;

    // UIマネージャー
    GameObject uiManager;

    // イベントマネージャー
    GameObject eventManager;

    // EnemyManager
    GameObject enemyManager;

    // 必要接続人数
    int RequiredNum = 1;

    //===========================
    //  [公開]フィールド
    //===========================

    /// <summary>
    /// クライアント
    /// </summary>
    public TcpClient tcpClient { get; set; }

    /// <summary>
    /// 自身のプレイヤーID (1P,2Pなど)
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// 元々のプレイヤーID 
    /// </summary>
    public int originalID { get; set; } // 誰かが途中退出したときにプレイヤーIDがずれるため

    /// <summary>
    /// 自身の役職が内通者かどうか
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// 現在行動可能なプレイヤーのID
    /// </summary>
    public int turnPlayerID { get; set; }

    /// <summary>
    /// 最大ターン数
    /// </summary>
    public int turnMaxNum { get; set; }

    /// <summary>
    /// 現在のラウンド数
    /// </summary>
    public int roundNum { get; set; }

    /// <summary>
    /// プレイヤーの名前
    /// </summary>
    public List<string> playerNameList { get; set; }

    /// <summary>
    /// ラウンド開始時の表示するスコアのリスト
    /// </summary>
    public List<int> scoreList { get; set; }

    /// <summary>
    /// サーバーから通知を受け取ったかどうか
    /// </summary>
    public bool isGetNotice { get; set; }

    /// <summary>
    /// 破棄する通信中のテキスト
    /// </summary>
    public GameObject loadingObj { get; set; }

    /// <summary>
    /// リザルトシーンで使用
    /// </summary>
    public List<int> totalScoreList { get; set; }

    /// <summary>
    /// ゲームモード
    /// </summary>
    public enum GAMEMODE
    {
        Title,      // タイトル
        Tutorial,   // チュートリアル
        Standby,    // 待機
        Job,        // 役職
        Game,       // ゲーム
        result      // リザルト
    }

    /// <summary>
    /// 送受信用のID
    /// </summary>
    public enum EventID
    {
        PlayerID = 0,         // プレイヤーID(1P,2P・・・)
        ListenerList,         // リスナーデータ
        ReadyData,            // 準備完了
        JobAndTurn,           // 役職と先行のプレイヤーID
        RoundEnd,             // ラウンド終了通知
        MoveData,             // 移動
        Action_FillData,      // 行動：埋める
        Action_MiningData,    // 行動：切り開く
        Action_NothingData,   // 行動：何もしない
        DelPlayerID,          // 切断したプレイヤーのID
        UdTurns,              // ターンを更新
        DoubtData,            // ダウトのデータ
        RevisionPos,          // 座標の修正
        EventAlertData,       // イベント通知
        AllieScore,           // スコアの加算

        //+++++++++++++++++++++++++
        //  発生するイベントのID
        //++++++++++++++++++++++++++
        EvendAlert = 100,           // イベント開始通知
        RndFallStones,              // ランダムに空から石が降ってくる
        Confusion,                  // 混乱状態になる
        SpownEnemys,                // 敵が出現
        RiStaminaCn,                // スタミナの消費量を減らす
        RndSpawnGold,               // ランダムにゴールドが空から降ってくる
                                    //Decoy,                    // デコイ
        EvendFinish = 110,          // イベント終了通知



        //++++++++++++++++++++++++++
        //  サボタージュのID
        //++++++++++++++++++++++++++
    }

    //===========================
    //  [静的]フィールド
    //===========================

    // シングルトン用
    public static ClientManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            // シーン遷移しても破棄しないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 開始時の処理
    /// </summary>
    void Start()
    {
        //フェードアウト
        //fade.FadeOut(1f);

        // 初期化
        tcpClient = new TcpClient();
        context = SynchronizationContext.Current;

        playerID = 0;

        // 取得する
        clientName = TitleManager.UserName;

        // 非同期処理を実行する
        StartConnect();
    }

    /// <summary>
    /// 接続切断(退出)ボタン
    /// </summary>
    public void DisconnectButton()
    {
        // 真
        isDisconnect = true;

        Instance = null;

        // 接続を切断
        tcpClient.Close();

        // フェード＆シーン遷移
        Initiate.DoneFading();
        Initiate.Fade("TitleKawaguchi_copy", Color.black, 3.0f);

        Debug.Log("退出");

        Destroy(this.gameObject);
    }

    /// <summary>
    /// 準備完了(OK)ボタンを押した
    /// </summary>
    public async void ReadyButton()
    {
        ReadyData readyData = new ReadyData();

        readyData.id = playerID;
        readyData.isReady = isReadyButton;

        // 準備完了待ちを送信
        await Send(readyData, 2);
        
        // フラグを切り替え
        isReadyButton = !isReadyButton;

        if (listenerList.Count < RequiredNum)
        {// 接続人数が揃っていない場合
            // 表示・非表示切り替え
            buttonObjList[0].SetActive(false);
            buttonObjList[1].SetActive(false);
            buttonObjList[2].SetActive(true);
        }
        else
        {
            // 表示・非表示切り替え
            buttonObjList[0].SetActive(isReadyButton);
            buttonObjList[1].SetActive(!isReadyButton);
        }
    }

    /// <summary>
    /// 受信処理(スレッド)
    /// </summary>
    /// <param name="arg"></param>
    async void RecvProc(object arg)
    {
        // クライアント作成
        TcpClient tcpClient = (TcpClient)arg;

        // NetworkStreamを使用
        stream = tcpClient.GetStream();

        // Enemyのリスト
        List<GameObject> enemyObjList = new List<GameObject>();

        // 自分自身が疑われた回数
        int doubtNum = 0;

        while (true)
        {
            // 受信待機する
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // 受信する

            // 接続切断チェック
            if (isDisconnect == true)
            {
                return;
            }

            // 受信データからイベントIDを取り出す
            int eventID = receiveBuffer[0];

            // 受信データからJSON文字列を取り出す
            byte[] bufferJson = receiveBuffer.Skip(1).ToArray();    // １バイト目をスキップ
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);  // 受信したものをbyteからstringに変換

            //*************************************
            //  メインスレッドに処理実行を依頼
            //*************************************

            context.Post(_ =>
            {
                switch (eventID)
                {
                    case 0: // 自身のプレイヤーIDを取得する

                        // JSONデシリアライズで取得する
                        PlayerIdData receiveData = JsonConvert.DeserializeObject<PlayerIdData>(jsonString);

                        Debug.Log("新しく受信したID : " + receiveData.id);

                        playerID = receiveData.id;   // 代入

                        

                        break;
                    case 1: // イベントIDが１の処理実行

                        Debug.Log("接続中のプレイヤー情報を受信した");

                        // JSONデシリアライズで取得する
                        listenerList = JsonConvert.DeserializeObject<List<ListenerData>>(jsonString);

                        // プレイヤーの名前を反映
                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (listenerList.Count > i)
                            {// インデックスが範囲内の場合
                                // Textを更新する
                                clientTextList[i].GetComponent<Text>().text = listenerList[i].name;

                                // Playerモデルを表示する
                                modelList[i].SetActive(true);
                            }
                            else
                            {// 初期化する
                                // Textを更新する
                                clientTextList[i].GetComponent<Text>().text = "";

                                // Playerモデルを非表示する
                                modelList[i].SetActive(false);
                            }
                        }

                        // 接続人数が４人以上の場合
                        if (listenerList.Count >= RequiredNum)
                        {
                            // 表示・非表示切り替え
                            buttonObjList[0].SetActive(isReadyButton);
                            buttonObjList[1].SetActive(!isReadyButton);
                            buttonObjList[2].SetActive(false);
                        }
                        else
                        {// 人数が揃っていない
                            if (isReadyButton == false)
                            {// 準備完了している場合
                                // 準備完了をキャンセルさせる
                                ReadyButton();
                            }
                            else
                            {
                                // 表示・非表示切り替え
                                buttonObjList[0].SetActive(false);
                                buttonObjList[1].SetActive(false);
                                buttonObjList[2].SetActive(true);
                            }
                        }

                        break;
                    case 2: // イベントIDが２の処理実行

                        Debug.Log("準備完了|準備待ち通知を受信");

                        List<ReadyData> readyDataList = JsonConvert.DeserializeObject<List<ReadyData>>(jsonString);

                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (readyDataList.Count > i)
                            {// インデックスが範囲内の場合
                                if (readyDataList[i].isReady == true)
                                {// 真
                                    readyTextList[i].SetActive(true);
                                }
                                else
                                {// 偽
                                    readyTextList[i].SetActive(false);
                                }
                            }
                            else
                            {// 範囲外の場合
                                // 偽
                                readyTextList[i].SetActive(false);
                            }
                        }

                        break;
                    case 3: // ラウンドの開始準備 [JobSceneに入る時に受信]

                        // 疑われた回数を初期化
                        doubtNum = 0;

                        isGetNotice = false;

                        // プレイヤーの名前リストを初期化する
                        playerNameList = new List<string>();

                        // プレイヤーの名前を取得する
                        foreach(ListenerData nameData in listenerList)
                        {
                            playerNameList.Add(nameData.name);

                            Debug.Log("一緒にやるプレイヤー名：" + nameData.name);
                        }

                        // 元々のプレイヤーIDを更新
                        originalID = playerID;

                        // マネージャーを初期化する
                        playerManager = null;
                        blockManager = null;
                        uiManager = null;
                        enemyManager = null;
                        eventManager = null;

                        Debug.Log("役職と先行のプレイヤーIDを取得");

                        // JSONデシリアライズで取得する
                        RoundRadyData data = JsonConvert.DeserializeObject<RoundRadyData>(jsonString);

                        // 全てのプレイヤーのスコアを取得する
                        scoreList = data.scoreList;

                        // 代入する
                        isInsider = data.isInsider;
                        turnPlayerID = data.advancePlayerID;
                        turnMaxNum = data.turnMaxNum;
                        roundNum = data.roundNum;

                        Debug.Log("内通者：" + data.isInsider);
                        Debug.Log("先行となるプレイヤーID：" + data.advancePlayerID);

                        // フェード＆シーン遷移
                        Initiate.DoneFading();
                        Initiate.Fade("JobScene_copy", Color.black, 10f);

                        break;
                    case 4: // ラウンド終了通知

                        Debug.Log("ラウンド終了通知を受信");

                        // JSONデシリアライズで取得する
                        RoundEndData roundEndData = JsonConvert.DeserializeObject<RoundEndData>(jsonString);

                        totalScoreList = roundEndData.totalScore;

                        if (roundEndData.isTurnEnd == true)
                        {// ラウンド終了による通知の場合

                            // 途中結果のリザルトを表示する

                            // フェード＆シーン遷移
                            //Initiate.DoneFading();
                            //Initiate.Fade("BoxOpenScene", Color.black, 1.0f);

                            // 遷移先にあるオブジェクトの関数を呼ぶためのコルーチン
                            //StartCoroutine(SetResultUI(roundEndData));
                        }
                        else
                        {
                            // フェード＆シーン遷移
                            Initiate.DoneFading();
                            Initiate.Fade("BoxOpenScene", Color.black, 2f);

                            // 遷移先にあるオブジェクトの関数を呼ぶためのコルーチン
                            StartCoroutine(SetPlayerAndMimic(roundEndData));
                        }

                        break;
                    case 5: // 移動

                        // JSONデシリアライズで取得する
                        MoveData moveData = JsonConvert.DeserializeObject<MoveData>(jsonString);

                        // 目的地の座標を取得
                        Vector3 targetPos = new Vector3(moveData.targetPosX, moveData.targetPosY, moveData.targetPosZ);

                        Debug.Log("[" + moveData.playerID + "]" + " : 移動");

                        // 移動処理
                        GameObject movePlayer = playerManager.GetComponent<PlayerManager>().players[moveData.playerID];
                        movePlayer.GetComponent<Player>().MoveAgent(targetPos);

                        break;
                    case 6: // 埋める

                        // JSONデシリアライズで取得する
                        Action_FillData fillData = JsonConvert.DeserializeObject<Action_FillData>(jsonString);

                        Debug.Log("[" + fillData.playerID + "]" + " : 埋める");

                        // 道を埋める処理
                        blockManager.GetComponent<BlockManager>().FillObject(fillData.objeID);

                        break;
                    case 7: // 切り開く

                        // JSONデシリアライズで取得する
                        Action_MiningData mineData = JsonConvert.DeserializeObject<Action_MiningData>(jsonString);

                        Debug.Log("[" + mineData.playerID + "]" + " : [" + mineData.prefabID + "]切り開く  *"+ mineData.isGetGold+"*");

                        // 切り開く処理
                        blockManager.GetComponent<BlockManager>().MineObject(mineData.playerID, mineData.objeID, mineData.prefabID, mineData.rotY,mineData.isGetGold);

                        break;
                    case 8: // やすむ(スタミナ回復)

                        // JSONデシリアライズで取得する
                        Action_NothingData restData = JsonConvert.DeserializeObject<Action_NothingData>(jsonString);

                        Debug.Log("[" + restData.playerID + "]が休んだ→(回復量：" + restData.addStamina + ") 合計：" + restData.totalStamina);

                        break;
                    case 9: // ゲーム中に切断したプレイヤーのIDを受信

                        // JSONデシリアライズで取得する
                        DelPlayerData delPlayerData = JsonConvert.DeserializeObject<DelPlayerData>(jsonString);

                        // 接続中のプレイヤー情報を更新
                        listenerList = delPlayerData.listeners;

                        Debug.Log("切断したプレイヤーID : " + delPlayerData.playerID);

                        // プレイヤーの名前リストから要素を削除
                        playerNameList.RemoveAt(delPlayerData.playerID);

                        // 途中退出したことを示唆するUIを表示する
                        uiManager.GetComponent<UIManager>().UdOutUI(delPlayerData.playerID);

                        // 途中退出したプレイヤーUIの位置を正す
                        uiManager.GetComponent<UIManager>().ReturnPlayerUI(delPlayerData.playerID);

                        // UIマネージャーが持つリストから要素を削除
                        uiManager.GetComponent<UIManager>().RemoveElement(delPlayerData.playerID);

                        // プレイヤーのモデルリストを取得する
                        List<GameObject> objeList = playerManager.GetComponent<PlayerManager>().players;

                        // 切断したプレイヤーのモデルを破棄する
                        Destroy(objeList[delPlayerData.playerID]);

                        // プレイヤーリストから削除する
                        objeList.RemoveAt(delPlayerData.playerID);

                        // 各プレイヤーオブジェクトのIDを再設定する
                        for (int i = 0; i < objeList.Count; i++)
                        {
                            // オブジェクトIDを更新する
                            objeList[i].GetComponent<Player>().playerObjID = i;
                        }

                        break;
                    case 10:    // ターン数の更新＆次に行動できるプレイヤーIDの更新

                        Debug.Log("ターンを更新します。");

                        // JSONデシリアライズで取得する
                        UpdateTurnsData udTurnsData = JsonConvert.DeserializeObject<UpdateTurnsData>(jsonString);

                        // 次に行動できるプレイヤーのIDを更新する
                        Debug.Log("次に行動できるプレイヤーID：" + udTurnsData.nextPlayerID);
                        turnPlayerID = udTurnsData.nextPlayerID;
                        uiManager.GetComponent<UIManager>().UdTurnPlayerUI(playerNameList[turnPlayerID], turnPlayerID);   // UIを更新

                        // 残りターン数を更新する
                        uiManager.GetComponent<UIManager>().UdRemainingTurns(udTurnsData.turnNum);

                        // 制限時間を0にする
                        TimeUI.Instance.FinishTimer();

                        if(turnPlayerID == playerID)
                        {// 自身のターンの場合
                            TimeUI.Instance.GenerateTimer(doubtNum);    // 制限時間を設定する
                        }

                        break;
                    case 11:    // ダウトのデータを受信

                        // JSONデシリアライズで取得する
                        DoubtData doubtData = JsonConvert.DeserializeObject<DoubtData>(jsonString);

                        Debug.Log("[元のID]" + doubtData.playerID + "が" + doubtData.targetID + "を疑う");

                        if(doubtData.targetID == originalID)
                        {// 自身が疑われた場合
                            doubtNum++;
                        }

                        // ダウトUIを更新する
                        uiManager.GetComponent<UIManager>().UdDoubt(doubtData.targetID, doubtData.playerID);

                        break;
                    case 12: // 対象のプレイヤーオブジェクトの座標を修正する

                        // JSONデシリアライズで取得する
                        RevisionPosData revisionPos = JsonConvert.DeserializeObject<RevisionPosData>(jsonString);

                        // プレイヤーのモデルリストを取得する
                        List<GameObject> objeList1 = playerManager.GetComponent<PlayerManager>().players;

                        if (revisionPos.isEnemy == true)
                        {// 敵による座標修正

                            Debug.Log(revisionPos.targetID + "がダウンした");

                            objeList1[revisionPos.targetID].GetComponent<Player>().DownPlayer();
                        }
                        else
                        {
                            // 座標を取得する
                            Vector3 targetPos1 = new Vector3(revisionPos.targetPosX, revisionPos.targetPosY, revisionPos.targetPosZ);

                            Debug.Log("座標を修正する [オブジェクトID：" + revisionPos.targetID + "] **送信元のプレイヤーID：" + revisionPos.playerID);

                            // 座標を修正する
                            objeList1[revisionPos.targetID].GetComponent<Player>().RevisionPos(targetPos1);
                        }

                        break;
                    case 13:    // ゲーム開始通知 (どの宝箱をミミックにするか受信)

                        // JSONデシリアライズで取得する
                        RoundStartData roundStartData = JsonConvert.DeserializeObject<RoundStartData>(jsonString);

                        GameObject chestManager = GameObject.Find("ChestList");

                        // ミミックを設定する
                        chestManager.GetComponent<ChestManager>().SetMimic(roundStartData.isMimicList);

                        isGetNotice = true;

                        if(loadingObj != null)
                        {
                            Destroy(loadingObj);
                        }

                        // ターンテキストを更新（先行のプレイヤー）
                        uiManager.GetComponent<UIManager>().UdTurnPlayerUI(playerNameList[turnPlayerID], turnPlayerID);

                        break;
                    case 14: // スコアのテキストを更新する

                        // JSONデシリアライズで取得する
                        AllieScoreData allieScoreData = JsonConvert.DeserializeObject<AllieScoreData>(jsonString);

                        // スコアリストの要素を更新する
                        scoreList[allieScoreData.playerID] = allieScoreData.allieScore;

                        // スコアテキストを更新する
                        uiManager.GetComponent<UIManager>().UdScoreText(allieScoreData.originalID, allieScoreData.allieScore);

                        break;
                    case 15:    // 受信しない
                        break;

                    //*****************************
                    //  イベントのDataを受信
                    //*****************************

                    case 100:   // イベント発生通知

                        // JSONデシリアライズで取得する
                        EventAlertData eventData = JsonConvert.DeserializeObject<EventAlertData>(jsonString);

                        Debug.Log("イベント発生 : " + eventData.eventID);

                        // イベント用テキストを更新する
                        uiManager.GetComponent<UIManager>().UdEventText(eventData.eventID);

                        break;

                    case 101:   // ランダム落石

                        // JSONデシリアライズで取得する
                        Event_RndFallData event_stoneData = JsonConvert.DeserializeObject<Event_RndFallData>(jsonString);

                        Debug.Log("イベント：落石");

                        // 座標を設定
                        Vector3 stonePos = blockManager.GetComponent<BlockManager>().blocks[event_stoneData.panelID].transform.position;
                        stonePos = new Vector3(stonePos.x + event_stoneData.addPosX, 0.5f, stonePos.z + event_stoneData.addPosZ);

                        // 落石オブジェクトの生成処理
                        eventManager.GetComponent<EventManager>().GenerateEventStone(stonePos);

                        break;
                    case 102:   // 混乱
                        break;
                    case 103:   // 敵出現

                        // JSONデシリアライズで取得する
                        Event_SpawnEnemyData event_enemyData = JsonConvert.DeserializeObject<Event_SpawnEnemyData>(jsonString);

                        Debug.Log("イベント：敵出現");

                        // 座標を設定する
                        Vector3 enemyPos = blockManager.GetComponent<BlockManager>().blocks[event_enemyData.panelID].transform.position;
                        enemyPos = new Vector3(enemyPos.x, 0.55f, enemyPos.z);

                        // 敵を生成したらリストに追加する
                        enemyObjList.Add(eventManager.GetComponent<EventManager>().SpawnEnemy(enemyPos));

                        break;
                    case 104:   // スタミナバフ
                        break;
                    case 105:   // ランダムに金が降ってくる

                        // JSONデシリアライズで取得する
                        Event_RndFallData event_goldData = JsonConvert.DeserializeObject<Event_RndFallData>(jsonString);

                        // 座標を設定
                        Vector3 goldPos = blockManager.GetComponent<BlockManager>().blocks[event_goldData.panelID].transform.position;
                        goldPos = new Vector3(goldPos.x + event_goldData.addPosX, 10f, goldPos.z + event_goldData.addPosZ);

                        // 金のオブジェクトの生成処理
                        eventManager.GetComponent<EventManager>().GenerateEventStone(goldPos);

                        Debug.Log("イベント：金");

                        // 金の生成

                        break;

                    case 110:   // イベント終了通知

                        // JSONデシリアライズで取得する
                        EventAlertData eventFinishData = JsonConvert.DeserializeObject<EventAlertData>(jsonString);

                        Debug.Log("終了するイベント : " + eventFinishData.eventID);

                        if(eventFinishData.eventID == 103)
                        {// 敵の場合
                            // 点滅のコルーチンを開始
                            enemyManager.GetComponent<EnemyManager>().StartCoroutine
                            (enemyManager.GetComponent<EnemyManager>().StartBlink(enemyObjList));
                            enemyObjList = new List<GameObject>();  // 初期化する
                        }

                        break;
                }

            }, null);
        }
    }

    /// <summary>
    /// 接続処理 && スレッド起動
    /// </summary>
    private async void StartConnect()
    {
        // 自身の情報格納用
        ListenerData listener = new ListenerData(); // インスタンス化
        listener.name = clientName;  // 自身の名前を代入

        try
        {
            //**********************************
            //  接続の要求 & 通信の確立
            //**********************************

            // 送受信のタイムアウトを設定(msec)
            tcpClient.SendTimeout = 1000;       // 送信
            tcpClient.ReceiveTimeout = 1000;    // 受信

            // サーバーへ接続要求    (IP:"20.249.92.21"  "127.0.0.1")
            await tcpClient.ConnectAsync("127.0.0.1", 20000);
            Debug.Log("***サーバーと通信確立***");

            //**********************************
            //  プレイヤーIDを受信する
            //**********************************

            // NetworkStreamを使用
            stream = tcpClient.GetStream();

            // 受信待機する
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // 受信する

            // 受信データからJSON文字列を取り出す
            byte[] bufferJson = receiveBuffer.Skip(1).ToArray();    // １バイト目をスキップ
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);  // 受信したものをbyteからstringに変換

            // JSONデシリアライズで取得する
            PlayerIdData receiveData = JsonConvert.DeserializeObject<PlayerIdData>(jsonString);

            playerID = receiveData.id;   // 代入

            Debug.Log("受信したプレイヤーID：" + playerID);

            //**********************************
            //  自身の情報を送信する
            //**********************************

            // プレイヤーIDを代入
            listener.id = playerID;

            // 送信する
            await Send(listener, 1);

            //**********************************
            //  準備完了データを送信する
            //**********************************

            // 送受信する
            ReadyButton();

            //**********************************
            //  受信用のスレッドを起動
            //**********************************

            Thread thread = new Thread(new ParameterizedThreadStart(RecvProc));
            thread.Start(tcpClient);
        }
        catch (Exception ex)
        {
            Debug.Log("接続できない。後でまた試して");
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// マネージャーを取得する
    /// </summary>
    public void GetManagers()
    {
        // 取得する
        blockManager = GameObject.Find("BlockList");
        playerManager = GameObject.Find("player-List");
        uiManager = GameObject.Find("UIManager");
        eventManager = GameObject.Find("EventManager");
        enemyManager = GameObject.Find("EnemyManager");
    }

    IEnumerator SetPlayerAndMimic(RoundEndData roundEndData)
    {
        yield return new WaitForSeconds(2.1f);

        // シーン設定
        OpenManager.Instance.SetPlayerAndMimic(
            roundEndData.insiderID,
            roundEndData.openPlayerID,
            roundEndData.isMimic,
            roundEndData.totalScore,
            roundEndData.allieScore);
    }

    /// <summary>
    /// 送信する
    /// </summary>
    /// <param name="param"></param>
    /// <param name="arg"></param>
    public async Task Send(object data, /*object arg, */int eventID)
    {
        //// 引数からクライアントをキャストで取得する
        //TcpClient tcpClientList = (TcpClient)arg;

        // JSONシリアライズ
        string json = JsonConvert.SerializeObject(data);  // クラス型の変数を指定

        // Json文字列をbyte配列に変換
        byte[] sendData = Encoding.UTF8.GetBytes(json);

        // 送信データの先頭にイベントIDを追加する
        sendData = sendData.Prepend((byte)eventID).ToArray();    // ※必ずIDを確認すること!!!!!!!!!!

        // 送信データの配列数を固定に変更
        Array.Resize(ref sendData, 1024);   // サイズを固定して安定に送受信できるようにする

        // 送信処理
        NetworkStream stream = tcpClient.GetStream();
        await stream.WriteAsync(sendData, 0, sendData.Length);  // 送信

        Debug.Log("OKOKOK");
    }

    /// <summary>
    /// 別のものスクリプトからデータを送信する
    /// </summary>
    /// <param name="data"></param>
    /// <param name="eventID"></param>
    public async void SendData(object data, int eventID)
    {
        await Send(data, eventID);

        Debug.Log("OK");
    }

    /// <summary>
    /// exeを終了したときの処理
    /// </summary>
    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            // 接続を切断
            tcpClient.Close();
        }

        Instance = null;
    }
}
