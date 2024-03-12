using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // ロードプレハブを格納
    [SerializeField] GameObject[] RoadPrefab = new GameObject[5];

    [SerializeField] GameObject blockObj;

    [SerializeField] GameObject bombPrefab;

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

    // 煙のアニメーション
    [SerializeField] GameObject smoke;

    // 敵
    GameObject enemy;

    // ステージの管理
    GameObject stageManager;

    // ボタンマネージャーを取得
    ButtonManager buttonManager;

    // ロードパネルを取得
    RoadPanel roadPanel;

    public GameObject targetBlock;
    public int rotY;

    // ランダムの数値を入れる変数
    int rand;

    private int roadNum;

    private bool isGold;

    public int selectPanelCount;

    // Start is called before the first frame update
    void Start()
    {
        rotY = 0;
        targetBlock = null;
        isGold = false;

        // Bake
        Baker = GameObject.Find("StageManager");

        // UIManager
        uiMnager = GameObject.Find("UIManager");

        stageManager = GameObject.Find("StageManager");

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
    }

    private void Update()
    {
        if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
        {
            // 埋める場所を選択した数が4回以上なら
            if (selectPanelCount >= 4)
            {
                for (int i = 0; i < selectPanelList.Count; i++)
                {
                    // オブジェクトを生成する
                    GameObject block = Instantiate(blockObj, new Vector3(selectPanelList[i].transform.position.x,
                        1.47f, selectPanelList[i].transform.position.z), Quaternion.identity);

                    selectPanelList[i].GetComponent<RoadPanel>().isColor = false;

                    // オブジェクトを破棄
                    Destroy(selectPanelList[i]);
                }

                //リストの中身・カウントを初期化
                selectPanelList = new List<GameObject>();
                selectPanelCount = 0;

                buttonManager.GetComponent<ButtonManager>().DisplayButton();

                // ベイクを開始
                stageManager.GetComponent<StageManager>().StartBake();
            }
        }
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEBOMB)
        {
            // ボムを設置する場所を2回以上選択されたら
            if (selectPanelCount >= 2)
            {
                for (int n = 0; n < selectPanelList.Count; n++)
                {
                    // オブジェクトを生成する
                    GameObject bomb = Instantiate(bombPrefab, new Vector3(selectPanelList[n].transform.position.x,
                                       0.5f, selectPanelList[n].transform.position.z), Quaternion.identity);

                    bomb.GetComponent<Bomb>().roadPanel = selectPanelList[n];

                    selectPanelList[n].GetComponent<RoadPanel>().isColor = false;

                    selectPanelList[n].tag = "AbnormalPanel";
                }

                //リストの中身・カウントを初期化
                selectPanelList = new List<GameObject>();
                selectPanelCount = 0;

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

   //====================
   // 道を選択
   //====================
   public void Road(int num)
    {
        Player script = player.GetComponent<Player>();

        isGold = true;

        if(num == 0 && script.stamina >= 30)
        {// I字
            player.GetComponent<Player>().SubStamina(30);
        }
        else if(num == 1 && script.stamina >= 40)
        {// L字
            player.GetComponent<Player>().SubStamina(40);
        }
        else if (num == 2 && script.stamina >= 60)
        {// T字
            player.GetComponent<Player>().SubStamina(60);
        }
        else if (num == 3 && script.stamina >= 80)
        {// 十字
            player.GetComponent<Player>().SubStamina(80);
        }
        else if (num == 4 && script.stamina >= 10)
        {// ゴミみたいな道
            player.GetComponent<Player>().SubStamina(10);
        }
        else
        {
            Debug.Log("スタミナ不足のため切り開けない");

            return;
        }

        ShowRoad(num);

        roadNum = num;

        Road(RoadPrefab[num]);

        // 道の選択肢を非表示
        uiMnager.GetComponent<UIManager>().road.SetActive(false);

        //uiMnager.GetComponent<UIManager>().selectRoadNum = num;
    }
   
    public void ShowRoad(int num)
    {
        // 2ターン非表示のカウントを持たせる
        selectRoad[num] = 3;  // 強制的にマイナスされるため1カウント多く

        // 選択した道UIを非表示にする
        uiMnager.GetComponent<UIManager>().roadUIList[num].SetActive(false);

        for (int i = 0; i < selectRoad.Length; i++)
        {
            if (selectRoad[i] > 0)
            {
                // カウントを減らす
                selectRoad[i] --;  // カウントをここで2にすることで2ターン非表示が可能
            }

            if (selectRoad[i] <= 0)
            {// カウントが0だったら
                
                // 2ターン前に選択した道UI表示する
                uiMnager.GetComponent<UIManager>().roadUIList[i].SetActive(true);
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

        // ゴールドを生成
        Instantiate(gold, new Vector3(block.transform.position.x,1.0f, block.transform.position.z), Quaternion.identity);

        // ゴールドを生成
        Instantiate(smoke, block.transform.position, Quaternion.identity);

        // 初期化
        targetBlock = null;
        rotY = 0;
    }
}
