using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // ロードプレハブを格納
    [SerializeField] GameObject[] RoadPrefab = new GameObject[5];

    [SerializeField] GameObject blockObj;

    // 爆弾のプレファブ
    [SerializeField] GameObject bombPrefab;

    // スロートラップのプレファブ
    [SerializeField] GameObject throwPrefab;

    public int[] selectRoad = new int[5];

    // sabotageの時に使うどこを選択したかのリスト
    public List<GameObject> selectPanelList;

    // ベイクオブジェクトを取得
    GameObject Baker;

    // UIManager
    GameObject uiMnager;

    // プレイヤー
    GameObject player;

    // ゴールド
    [SerializeField] GameObject gold;
    
    // 煙アニメーション
    [SerializeField] GameObject smoke;

    // 敵
    GameObject enemy;

    // ステージの管理
    GameObject stageManager;

    // ボタンマネージャー
    ButtonManager buttonManager;

    // テキストUI
    GameObject textUI;

    // スタミナ不足UI
    [SerializeField] GameObject staminaUI;

    GameObject sabotage;

    Animator animator;

    public bool isAnimMining;

    public GameObject targetBlock;
    public int rotY;

    // ランダムの数値を入れる変数
    int rand;

    private int roadNum;

    private bool isGold;

    public int selectPanelCount;

    // 消費量を抑えるスタミナの値
    public int buffStamina;

    // シングルトン用
    public static RoadManager Instance;

    // オーディオソース系
    AudioSource audio;
    [SerializeField] AudioClip noStaminaSE;

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
        rotY = 0;
        targetBlock = null;
        isGold = false;
        buffStamina = 0;
        audio = GetComponent<AudioSource>();

        // Bake
        Baker = GameObject.Find("StageManager");

        // UIManager
        uiMnager = GameObject.Find("UIManager");

        textUI = GameObject.Find("TextUIManager");

        stageManager = GameObject.Find("StageManager");

        sabotage = GameObject.Find("Sabotage");

        // Player
        if (EditorManager.Instance.useServer)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        //enemy = GameObject.Find("enemy");

        //enemy.SetActive(false);

        // Button
        GameObject buttonManagerObject = GameObject.Find("ButtonManager");
        buttonManager = buttonManagerObject.GetComponent<ButtonManager>();

        // スタミナ不足UIの取得
        staminaUI.SetActive(false);
    }

    private async Task Update()
    {
        // 送信用のクラス変数
        Sabotage_SetData setData = new Sabotage_SetData();
        setData.objID = new List<int>();

        //*************************************************
        // プレイヤーモードがサボタージュ(埋める)だったら
        //*************************************************
        if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
        {
            // 埋める場所を選択した数が4回以上なら
            if (selectPanelCount >= 4)
            {
                for (int i = 0; i < selectPanelList.Count; i++)
                {
                    if (EditorManager.Instance.useServer == true)
                    {// サーバーを使用する場合

                        GameObject panel = selectPanelList[i];

                        // データ変数を設定
                        setData.objID.Add(panel.GetComponent<RoadPanel>().objeID);

                        Debug.Log("選択したオブジェクトID : " + panel.GetComponent<RoadPanel>().objeID);

                        if (i == selectPanelList.Count - 1)
                        {// クラス変数に代入し終えたら

                            // プレイヤーID・サボタージュのID設定
                            setData.playerID = ClientManager.Instance.playerID;
                            setData.sabotageID = 0;

                            // 送信処理
                            await ClientManager.Instance.Send(setData, 200);
                        }
                    }
                    else
                    {
                        // オブジェクトを生成する
                        GameObject block = Instantiate(blockObj, new Vector3(selectPanelList[i].transform.position.x,
                            1.47f, selectPanelList[i].transform.position.z), Quaternion.identity);

                        // オブジェクトを破棄
                        Destroy(selectPanelList[i]);
                    }

                    // カラー変更を元に戻す
                    selectPanelList[i].GetComponent<RoadPanel>().isColor = false;
                }

                sabotage.GetComponent<Sabotage>().isFill = true;

                sabotage.GetComponent<Sabotage>().ResetCoolTime();

                //sabotage.GetComponent<Sabotage>().timeNum = 60;

                //リストの中身・カウントを初期化
                selectPanelList = new List<GameObject>();
                selectPanelCount = 0;

                // テキストを全て非表示にする
                textUI.GetComponent<TextUIManager>().HideText();

                // 非表示にしていたボタンをすべて戻す
                buttonManager.GetComponent<ButtonManager>().DisplayButton();

                // ベイクを開始
                stageManager.GetComponent<StageManager>().StartBake();
            }
        }
        //************************************************
        // プレイヤーモードがサボタージュ(爆弾)だったら
        //************************************************
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEBOMB)
        {
            // ボムを設置する場所を2回以上選択されたら
            if (selectPanelCount >= 2)
            {
                for (int n = 0; n < selectPanelList.Count; n++)
                {
                    if (EditorManager.Instance.useServer == true)
                    {// サーバーを使用する場合

                        GameObject panel = selectPanelList[n];

                        // データ変数を設定
                        setData.objID.Add(panel.GetComponent<RoadPanel>().objeID);

                        Debug.Log("選択したオブジェクトID : " + setData.objID[n]);

                        if (n == selectPanelList.Count - 1)
                        {// クラス変数に代入し終えたら

                            // プレイヤーID・サボタージュのID設定
                            setData.playerID = ClientManager.Instance.playerID;
                            setData.sabotageID = 1;

                            // 送信処理
                            await ClientManager.Instance.Send(setData, 200);
                        }
                    }
                    else
                    {
                        // オブジェクトを生成する
                        GameObject bomb = Instantiate(bombPrefab, new Vector3(selectPanelList[n].transform.position.x,
                                       0.5f, selectPanelList[n].transform.position.z), Quaternion.identity);

                        // リストのn番目の情報を渡す
                        bomb.GetComponent<Bomb>().roadPanel = selectPanelList[n];

                        // リストのn番目のタグをAbnormalPanelに変更
                        selectPanelList[n].tag = "AbnormalPanel";
                    }

                    // リストのn番目のカラーを元に戻す
                    selectPanelList[n].GetComponent<RoadPanel>().isColor = false;
                }

                sabotage.GetComponent<Sabotage>().isBomb = true;

                sabotage.GetComponent<Sabotage>().ResetCoolTime();

                //リストの中身・カウントを初期化
                selectPanelList = new List<GameObject>();
                selectPanelCount = 0;

                // テキストを全て非表示にする
                textUI.GetComponent<TextUIManager>().HideText();

                // 非表示にしていたボタンをすべて戻す
                buttonManager.GetComponent<ButtonManager>().DisplayButton();

                // ベイクを開始
                stageManager.GetComponent<StageManager>().StartBake();
            }   
        }

        //**********************************************************
        // プレイヤーモードがサボタージュ(スロートラップ)だったら
        //**********************************************************
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGETRAP)
        {
            for (int n = 0; n < selectPanelList.Count; n++)
            {
                if (EditorManager.Instance.useServer == true)
                {// サーバーを使用する場合

                    GameObject panel = selectPanelList[n];

                    // データ変数を設定
                    setData.objID.Add(panel.GetComponent<RoadPanel>().objeID);

                    Debug.Log("選択したオブジェクトID : " + panel.GetComponent<RoadPanel>().objeID);

                    // プレイヤーID・サボタージュのID設定
                    setData.playerID = ClientManager.Instance.playerID;
                    setData.sabotageID = 2;

                    // 送信処理
                    await ClientManager.Instance.Send(setData, 200);
                }
                else
                {
                    // オブジェクトを生成する
                    GameObject throwTrap = Instantiate(throwPrefab, new Vector3(selectPanelList[n].transform.position.x,
                                       0f, selectPanelList[n].transform.position.z), Quaternion.identity);
                }

                // リストのn番目のカラーを元に戻す
                selectPanelList[n].GetComponent<RoadPanel>().isColor = false;

                sabotage.GetComponent<Sabotage>().isTrap = true;

                //リストの中身・カウントを初期化
                selectPanelList = new List<GameObject>();
                selectPanelCount = 0;

                sabotage.GetComponent<Sabotage>().ResetCoolTime();

                // テキストを全て非表示にする
                textUI.GetComponent<TextUIManager>().HideText();

                // 非表示にしていたボタンをすべて戻す
                buttonManager.GetComponent<ButtonManager>().DisplayButton();

                // ベイクを開始
                stageManager.GetComponent<StageManager>().StartBake();
            }
        }
    }

    public async void Road(GameObject roadPrefab)
    {
        if (targetBlock == null)
        {// ターゲットのブロックが存在しない
            return;
        }

        // ロードプレハブの角度を変える
        roadPrefab.transform.Rotate(0f, rotY, 0f);

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            // データ変数を設定
            Action_MiningData mineData = new Action_MiningData();
            mineData.playerID = ClientManager.Instance.playerID;
            mineData.objeID = targetBlock.GetComponent<Block>().objeID;
            mineData.prefabID = roadNum;
            mineData.rotY = rotY;

            if (targetBlock.tag == "EventBlock")
            {// 対象のブロックがイベントブロックの場合
                mineData.isEventBlock = true;   // 真
            }
            else
            {
                mineData.isEventBlock = false;   // 偽
            }
            

            // 送信する
            await ClientManager.Instance.Send(mineData, 7);
        }
        else
        {// サーバーを使用しない

            // 生成 → 破棄 → ベイク
            Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);

        }

        //// 前回選択した道UIを表示 & 道選択UI(Parent)を閉じる
        //uiMnager.GetComponent<UIManager>().HideRoad(uiMnager.GetComponent<UIManager>().selectRoadNum);

        // 消えているボタンを表示する
        buttonManager.DisplayButton();

        // 初期化
        targetBlock = null;
        rotY = 0;
    }

    public bool IsDigPossible(int num)
    {
        int stamina = player.GetComponent<Player>().stamina;

        if (uiMnager.GetComponent<UIManager>().isChaos == true)
        {
            // スタミナが40以上なら
            if (stamina >= 40)
            {
                return true;
            }
        }
        else
        {
            if (num == 0 && stamina >= 30)
            {// I字 & スタミナが30以上なら
                return true;
            }
            else if (num == 1 && stamina >= 40)
            {// L字 & スタミナが40以上なら
                return true;
            }
            else if (num == 2 && stamina >= 60)
            {// T字 & スタミナ60以上なら
                return true;
            }
            else if (num == 3 && stamina >= 80)
            {// 十字 & スタミナが80以上なら
                return true;
            }
            else if (num == 4 && stamina >= 10)
            {// ゴミみたいな道 & スタミナが10以上なら
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 道を選択
    /// </summary>
    /// <param name="num"></param>
    public void Road(int num)
    {
        Player script = player.GetComponent<Player>();

        isGold = true;

        //************************************
        // 混乱イベントが発生してるとき
        //************************************
        if (uiMnager.GetComponent<UIManager>().isChaos == true)
        {
            // スタミナが40以上なら
            if (script.stamina >= 40 - buffStamina)
            {
                if (num >= 76 && num <= 85)
                {// 数値が76～85の間なら(多分10%)
                 // I字
                    num = 0;
                }
                else if (num >= 61 && num <= 75)
                {// 数値が61～75の間なら(多分15%)
                 // L字
                    num = 1;
                }
                else if (num >= 86 && num <= 95)
                {// 数値が86～95の間なら(多分10%)
                 // T字
                    num = 2;
                }
                else if (num >= 96 && num <= 100)
                {// 数値が96～100の間なら(多分5%)
                 // 十字
                    num = 3;
                }
                else if (num >= 1 && num <= 60)
                {// 数値が1～60の間なら(多分60%)
                 // ゴミみたいな道
                    num = 4;
                }

                // スタミナを全て40固定で減らす
                player.GetComponent<Player>().SubStamina(40 - buffStamina);
            }
            else
            {
                Debug.Log("スタミナ不足のため切り開けない");

                // スタミナ不足UIの表示
                staminaUI.SetActive(true);

                audio.PlayOneShot(noStaminaSE);

                return;
            }
        }
        //************************************
        // 混乱イベントが発生してないとき
        //************************************
        else
        {
            if (num == 0 && script.stamina >= 30 - buffStamina)
            {// I字 & スタミナが30以上なら
                player.GetComponent<Player>().SubStamina(30 - buffStamina);
            }
            else if (num == 1 && script.stamina >= 40 - buffStamina)
            {// L字 & スタミナが40以上なら
                player.GetComponent<Player>().SubStamina(40 - buffStamina);
            }
            else if (num == 2 && script.stamina >= 60 - buffStamina)
            {// T字 & スタミナ60以上なら
                player.GetComponent<Player>().SubStamina(60 - buffStamina);
            }
            else if (num == 3 && script.stamina >= 80 - buffStamina)
            {// 十字 & スタミナが80以上なら
                player.GetComponent<Player>().SubStamina(80 - buffStamina);
            }
            else if (num == 4 && script.stamina >= 10 - buffStamina)
            {// ゴミみたいな道 & スタミナが10以上なら
                player.GetComponent<Player>().SubStamina(10 - buffStamina);
            }
            else
            {
                Debug.Log("スタミナ不足のため切り開けない");

                // スタミナ不足UIの表示
                staminaUI.SetActive(true);

                return;
            }
        }

        // 前回、前々回選択した道UIを非表示にしてその他を表示する
        ShowRoad(num);

        roadNum = num;

        // 選択されたUI番号を渡す
        Road(RoadPrefab[num]);

        // 道の選択肢を非表示
        uiMnager.GetComponent<UIManager>().road.SetActive(false);

        // プレイヤーのモードを戻す
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        //uiMnager.GetComponent<UIManager>().selectRoadNum = num;
    }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    public void ShowRoad(int num)
    {
        // 2ターン非表示のカウントを持たせる
        selectRoad[num] = 3;  // 強制的にマイナスされるため1カウント多く

        // 選択した道UIを非表示にする
        uiMnager.GetComponent<UIManager>().roadUIList[num].SetActive(false);

        if(uiMnager.GetComponent<UIManager>().isChaos != true)
        {// イベント(混乱)が起こっていない時
            for (int i = 0; i < selectRoad.Length; i++)
            {
                if (selectRoad[i] > 0)
                {
                    // カウントを減らす
                    selectRoad[i]--;  // カウントをここで2にすることで2ターン非表示が可能
                }

                if (selectRoad[i] <= 0)
                {// カウントが0だったら

                    // 2ターン前に選択した道UI表示する
                    uiMnager.GetComponent<UIManager>().roadUIList[i].SetActive(true);
                }
            }
        }
    }


    /// <summary>
    /// 道の回転処理
    /// </summary>
    public void AddRotButton()
    { //道の回転
        rotY += 90;

        if (rotY >= 360)
        {
            rotY = 0;
        }
    }

    /// <summary>
    /// 生成、破棄、ベイクする
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="pos">生成する座標</param>
    /// <param name="desObject">破棄するオブジェクト</param>
    public void Bake(GameObject prefab, Vector3 pos, GameObject dieObject)
    {
        // オブジェクトを生成する
        GameObject block = Instantiate(prefab, pos, Quaternion.Euler(0, rotY, 0));

        // 破棄する
        Destroy(dieObject);

        // ベイクを開始
        Baker.GetComponent<StageManager>().StartBake();

        //ゴールドを生成
        Instantiate(gold, block.transform.position, Quaternion.identity);
        
        //煙アニメーションを生成
        Instantiate(smoke, block.transform.position, Quaternion.identity);

        // 初期化
        targetBlock = null;
        rotY = 0;
    }
}
