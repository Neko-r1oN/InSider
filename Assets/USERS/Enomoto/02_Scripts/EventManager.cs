using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject goldPrefab;
    [SerializeField] GameObject confusionPrefab;    // 混乱イベント用
    [SerializeField] GameObject buffPrefab;         // スタミナ消費量が減るイベント用

    // 格納用
    public List<GameObject> confusionObjList;
    public List<GameObject> buffObjList;

    GameObject playerManager;

    /// <summary>
    /// 発生するイベントのID (ただのメモ)
    /// </summary>
    public enum EventOccurrenceID
    {
        RndFallStones = 0,  // ランダムに空から石が降ってくる
        Confusion,          // 混乱状態になる
        SpownEnemys,        // 敵が出現
        RiStaminaCn,        // スタミナの消費量を減らす
        RndSpawnGold,       // ランダムにゴールドが空から降ってくる
        Decoy,              // デコイ
    }

    // 混乱のエフェクトプレファブ
    public GameObject chaosPrefab;

    // 起こしたいイベントのID
    public int eventNum;

    GameObject uiManager;

    GameObject player;

    // スタミナの消費量を減らすイベントになったかどうか
    public bool isEventStamina; 

    private void Start()
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            playerManager = GameObject.Find("player-List");
        }
        else
        {
            player = GameObject.Find("Player1");
        }

        uiManager = GameObject.Find("UIManager");

        confusionObjList = new List<GameObject>();
        buffObjList = new List<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {// Sキーを押すとイベントが起きる
            switch(eventNum)
            {
                case 0: // RndFallStones

                    // 処理内容

                    break;
                case 1: // Confusion

                    GameObject childObject = Instantiate(chaosPrefab, player.transform);

                    uiManager.GetComponent<UIManager>().isChaos = true;

                    break;
                case 2: // SpownEnemys

                    // 処理内容

                    break;
                case 3: // RiStaminaCn

                    // 処理内容
                    isEventStamina = true;

                    break;
                case 4: // RndSpawnGold

                    // 処理内容

                    break;
                case 5: // Decoy

                    // 処理内容

                    break;
            }
        }
    }

    /// <summary>
    /// 混乱状態にする
    /// </summary>
    public void SetConfusion()
    {
        PlayerManager manager = playerManager.GetComponent<PlayerManager>();

        for (int i = 0; i < manager.players.Count; i++)
        {
            // 格納する
            confusionObjList.Add(Instantiate(chaosPrefab, manager.players[i].transform));
        }

        // 混乱状態にするフラグ(true)
        uiManager.GetComponent<UIManager>().isChaos = true;
    }

    /// <summary>
    /// 混乱イベントを終了する
    /// </summary>
    public void EndConfusion()
    {
        foreach (GameObject particl in confusionObjList)
        {
            Destroy(particl);
        }

        confusionObjList = new List<GameObject>();

        // 混乱状態にするフラグ(false)
        uiManager.GetComponent<UIManager>().isChaos = false;
    }

    /// <summary>
    /// バフをかける
    /// </summary>
    public void SetBuff()
    {
        RoadManager.Instance.buffStamina = 10;

        PlayerManager manager = playerManager.GetComponent<PlayerManager>();

        for (int i = 0; i < manager.players.Count; i++)
        {
            // 格納する
            buffObjList.Add(Instantiate(buffPrefab, manager.players[i].transform));
        }
    }

    /// <summary>
    /// バフイベントを終了する
    /// </summary>
    public void EndBuff()
    {
        foreach (GameObject particl in buffObjList)
        {
            Destroy(particl);
        }

        buffObjList = new List<GameObject>();

        RoadManager.Instance.buffStamina = 0;
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnEnemy(Vector3 pos)
    {
        GameObject enemy = Instantiate(enemyPrefab,pos,Quaternion.identity);

        return enemy;
    }

    /// <summary>
    /// 石の生成
    /// </summary>
    /// <param name="pos"></param>
    public void GenerateEventStone(Vector3 pos)
    {
        // 生成する
        Instantiate(stonePrefab, pos,Quaternion.identity);
    }

    /// <summary>
    /// 金の生成
    /// </summary>
    /// <param name="pos"></param>
    public void GenerateEventGold(Vector3 pos)
    {
        // 生成する
        Instantiate(goldPrefab, pos, Quaternion.identity);
    }
}
