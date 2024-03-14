//************************************************************
//
//  サーバー使用時にブロックの生成、道パネルの生成をする
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;   // AI用

public class BlockManager : MonoBehaviour
{
    // 格納用
    public GameObject[] blocks;
    public GameObject[] bombs;

    // 道パネルのプレファブ
    [SerializeField] List<GameObject> roadPrefab;

    // ブロックのプレファブ
    [SerializeField] GameObject blockPrefab;

    // ゴールドのプレファブ
    [SerializeField] GameObject goldPrefab;

    // 爆弾のプレファブ
    [SerializeField] GameObject bombPrefab;

    // トラップのプレファブ
    [SerializeField] GameObject trapPrefab;

    // 埋めるときのアニメプレハブ
    [SerializeField] GameObject fillAnim;

    // 切り開くときのアニメプレハブ
    [SerializeField] GameObject mineAnim;

    // 格納用
    GameObject stageManager;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        stageManager = GameObject.Find("StageManager");

        // 親オブジェクトを取得
        GameObject parentObject = GameObject.Find("ParentObject");

        // 子オブジェクトの数分の配列を作成
        blocks = new GameObject[this.transform.childCount];

        // 子オブジェクトを順に取得する
        for (int i = 0; i < blocks.Length; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            childTransform.gameObject.GetComponent<Block>().objeID = i; // IDを設定する
            blocks[i] = childTransform.gameObject;  // 子オブジェクトを格納する
        }

        Debug.Log("ブロックの数:"+blocks.Length);

        // 爆弾の最大数
        bombs = new GameObject[2];
    }

    /// <summary>
    /// 道を埋める
    /// </summary>
    /// <param name="objeID">破棄するオブジェクトID</param>
    /// <param name="fillPos">座標</param>
    public IEnumerator FillObject(int objeID)
    {
        // 座標を設定
        Vector3 fillPos = blocks[objeID].gameObject.transform.position;
        fillPos.y = 1.47f;

        // 切り開くときのアニメプレファブ
        Instantiate(fillAnim, new Vector3(fillPos.x, 1f, fillPos.z), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);    // ↑↑のアニメーションの都合で遅らせる

        // オブジェクトを生成する
        GameObject block = Instantiate(blockPrefab, fillPos, Quaternion.identity);

        // ブロック情報を更新＆破棄する
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = block;
        Destroy(dieObje);

        // IDを設定
        block.GetComponent<Block>().objeID = objeID;

        Debug.Log("ブロックID：" + blocks[objeID].GetComponent<Block>().objeID);

        // ベイクを開始
        stageManager.GetComponent<StageManager>().StartBake();
    }

    /// <summary>
    /// ブロックを切り開く
    /// </summary>
    /// <param name="objeID">破棄するオブジェクトID</param>
    /// <param name="prefabID">プレファブのナンバー</param>
    /// <param name="rotY">回転度</param>
    public void MineObject(int playerID,int objeID,int prefabID,int rotY,bool isGetGold)
    {
        // 座標を設定
        Vector3 minePos = blocks[objeID].gameObject.transform.position;
        minePos.y = 0f;

        // オブジェクトを生成する
        GameObject road = Instantiate(roadPrefab[prefabID], minePos, Quaternion.Euler(0, rotY, 0));

        // 切り開くときのアニメプレファブ
        Instantiate(mineAnim, new Vector3(road.transform.position.x,1f,road.transform.position.z),Quaternion.identity);

        // ブロック情報を更新＆破棄する
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = road;
        Destroy(dieObje);

        // IDを設定
        road.GetComponent<RoadPanel>().objeID = objeID;

        Debug.Log("道ID：" + blocks[objeID].GetComponent<RoadPanel>().objeID);

        // ベイクを開始
        stageManager.GetComponent<StageManager>().StartBake();

        if(isGetGold == true)
        {// フラグが真の場合

            Debug.Log("金の生成");
            Debug.Log(playerID);

            //ゴールドを生成
            GameObject gold = Instantiate(goldPrefab, minePos, Quaternion.identity);

            // Update処理を開始 & 追尾させるプレイヤーIDを代入する
            gold.GetComponent<BlockGoldManager>().StartMove(playerID);
        }
    }

    /// <summary>
    /// 爆弾を生成する
    /// </summary>
    public void SetSabotage_Bomb(int objID,int bombID)
    {
        Debug.Log("生成する");

        // 座標を設定
        Vector3 minePos = blocks[objID].gameObject.transform.position;
        minePos.y = 0.5f;

        // 爆弾を生成する
        GameObject bomb = Instantiate(bombPrefab, minePos, Quaternion.identity);

        // 爆弾にIDを設定
        bomb.GetComponent<Bomb>().bombID = bombID;

        // オブジェクトの情報を格納する
        bombs[bombID] = bomb;

        // 爆弾の下にあるパネルの情報を爆弾に渡す
        bomb.GetComponent<Bomb>().roadPanel = blocks[objID];

        // パネルのタグをAbnormalPanelに変更
        blocks[objID].tag = "AbnormalPanel";    // 爆弾が存在する限り、「埋める」の対象外になる
    }

    /// <summary>
    /// トラップを設定する
    /// </summary>
    public void SetSabotage_Trap(int objID)
    {
        // 座標を設定
        Vector3 minePos = blocks[objID].gameObject.transform.position;
        minePos.y = 0f;

        // トラップを生成する
        GameObject bomb = Instantiate(trapPrefab, minePos, Quaternion.identity);
    }
}
