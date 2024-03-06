using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // 格納用
    public GameObject[] blocks;

    // 道パネルのプレファブ
    [SerializeField] List<GameObject> roadPrefab;

    // ブロックのプレファブ
    [SerializeField] GameObject blockPrefab;

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
    }

    /// <summary>
    /// 道を埋める
    /// </summary>
    /// <param name="objeID">破棄するオブジェクトID</param>
    /// <param name="fillPos">座標</param>
    public void FillObject(int objeID)
    {
        // 座標を設定
        Vector3 fillPos = blocks[objeID].gameObject.transform.position;
        fillPos.y = 1.47f;

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
    public void MineObject(int objeID,int prefabID,int rotY)
    {
        // 座標を設定
        Vector3 minePos = blocks[objeID].gameObject.transform.position;
        minePos.y = 0f;

        // オブジェクトを生成する
        GameObject road = Instantiate(roadPrefab[prefabID], minePos, Quaternion.Euler(0, rotY, 0));

        // ブロック情報を更新＆破棄する
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = road;
        Destroy(dieObje);

        // IDを設定
        road.GetComponent<RoadPanel>().objeID = objeID;

        Debug.Log("道ID：" + blocks[objeID].GetComponent<RoadPanel>().objeID);

        // ベイクを開始
        stageManager.GetComponent<StageManager>().StartBake();
    }
}
