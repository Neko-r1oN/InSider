using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject goldPrefab;

    // 混乱の際の混乱テクスチャのリスト
    //GameObject chaos;

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
        player = GameObject.Find("Player1");

        //chaos = GameObject.Find("ChaosList");

        uiManager = GameObject.Find("UIManager");

        //chaos = GameObject.Find("Chaos");

        //chaos.SetActive(false);
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
