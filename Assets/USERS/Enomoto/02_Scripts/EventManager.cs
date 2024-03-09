using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
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

    // 起こしたいイベントのID
    public int eventNum;

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

                    // 処理内容

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
}
