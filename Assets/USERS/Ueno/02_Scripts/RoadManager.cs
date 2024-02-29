using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ボタンマネージャーを取得
    ButtonManager buttonManager;

    public GameObject targetBlock;
    public int rotY;


    public int roadNum; 

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
        player = GameObject.Find("Player1");

        // Button
        GameObject buttonManagerObject = GameObject.Find("ButtonManager");

        buttonManager = buttonManagerObject.GetComponent<ButtonManager>();
    }

    public void Road(GameObject roadPrefab)
    {
        if (targetBlock == null)
        {// ターゲットのブロックが存在しない
            return;
        }

        // ロードプレハブの角度を変える
        roadPrefab.transform.Rotate(0f, rotY, 0f);

        // 生成 → 破棄 → ベイク
        Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);

        // 道選択UIを閉じる
        uiMnager.GetComponent<UIManager>().HideRoad(player.GetComponent<Player>().selectRoadNum);

        // 消えているボタンを表示する
        buttonManager.DisplayButton();
    }

   //====================
   // 道を選択
   //====================
   public void Road(int num)
    {
        Road(RoadPrefab[num]);

        if(num == 0)
        {
            player.GetComponent<Player>().SubStamina(20);
        }
        else if(num == 1)
        {
            player.GetComponent<Player>().SubStamina(15);
        }
        else if (num == 2)
        {
            player.GetComponent<Player>().SubStamina(30);
        }
        else if (num == 3)
        {
            player.GetComponent<Player>().SubStamina(40);
        }
        else if (num == 4)
        {
            player.GetComponent<Player>().SubStamina(10);
        }

        player.GetComponent<Player>().selectRoadNum = num;

        //roadNum = num;
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
