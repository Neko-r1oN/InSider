using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class RoadManager : MonoBehaviour
{
    // ロードプレハブを格納
    [SerializeField] GameObject[] RoadPrefab = new GameObject[5];

    // ベイクオブジェクトを取得
    GameObject Baker;

    // UIManager
    GameObject uiMnager;

    // プレイヤー
    GameObject player;

    // 敵
    GameObject enemy;

    // ボタンマネージャーを取得
    ButtonManager buttonManager;

    // ランダム関数
    System.Random rnd = new System.Random();

    public GameObject targetBlock;
    public int rotY;

    // ランダムの数値を入れる変数
    int rand;

    private int roadNum; 

    // Start is called before the first frame update
    void Start()
    {
        rotY = 0;
        targetBlock = null;

        // Bake
        Baker = GameObject.Find("StageManager");

        // UIManager
        uiMnager = GameObject.Find("UIManager");

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

        rand = rnd.Next(1, 20);
    }

    private void Update()
    {
        rand = rnd.Next(1, 20);
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

            // 送信する
            await ClientManager.Instance.Send(mineData, 7);
        }
        else
        {// サーバーを使用しない
            if(rand <= 15)
            {
                roadPrefab.tag = "RoadPanel";

                // 生成 → 破棄 → ベイク
                Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);
            }
            else if(rand > 15)
            {
                roadPrefab.tag = "EventPanel";

                // 生成 → 破棄 → ベイク
                Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);

                //enemy.GetComponent<EnemyManager>().CreateEnemy(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z);
            }
        }

        // 道選択UIを閉じる
        uiMnager.GetComponent<UIManager>().HideRoad(player.GetComponent<Player>().selectRoadNum);

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

        if(num == 0 && script.stamina >= 20)
        {// I字
            player.GetComponent<Player>().SubStamina(20);
        }
        else if(num == 1 && script.stamina >= 15)
        {// L字
            player.GetComponent<Player>().SubStamina(15);
        }
        else if (num == 2 && script.stamina >= 30)
        {// T字
            player.GetComponent<Player>().SubStamina(30);
        }
        else if (num == 3 && script.stamina >= 40)
        {// 十字
            player.GetComponent<Player>().SubStamina(40);
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

        roadNum = num;

        Road(RoadPrefab[num]);

        player.GetComponent<Player>().selectRoadNum = num;
    }
   
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
        Baker.GetComponent<StageBake>().StartBake();

        // 初期化
        targetBlock = null;
        rotY = 0;
    }
}
