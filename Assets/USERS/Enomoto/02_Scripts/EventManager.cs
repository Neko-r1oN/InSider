using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject goldPrefab;

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

    GameObject player;

    // シングルトン用
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
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

    private void Start()
    {
        player = GameObject.Find("Player1");
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

                    break;
                case 2: // SpownEnemys

                    // 処理内容

                    break;
                case 3: // RiStaminaCn

                    // 処理内容

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
