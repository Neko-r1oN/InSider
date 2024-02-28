using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureManager : MonoBehaviour
{

    // ランダム関数
    System.Random rnd = new System.Random();

    // ランダムな数値を入れるための変数
    int rand;

    int treasureCount = 0; // 宝箱の数をカウント

    int mimicCount = 0;    // ミミックの数をカウント;

    // Start is called before the first frame update
    void Start()
    {
        rand = rnd.Next(0, 3); // 0～2の数値が入る
    }

    // Update is called once per frame
    void Update()
    {
        if(rand <= 1)
        {//ランダムの数値が1以下なら
            if (treasureCount <= 2)
            {// 宝箱の個数が2以下なら
                // タグを設定
                this.gameObject.tag = "Treasure";

                // 宝箱カウントを増やす(個数を増やす)
                treasureCount++;
            }
            
        }
        else if(rand >= 2)
        {// ランダムの数値が2以上なら
            if(mimicCount <= 1)
            {// ミミックの個数が1以下なら
                // タグを設定
                this.gameObject.tag = "Mimic";

                // ミミックカウントを増やす(個数を増やす)
                mimicCount++;
            }
        }
    }
}
