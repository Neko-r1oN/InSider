using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMethodList : MonoBehaviour
{
    // シングルトン用
    public static ScoreMethodList Instance;

    // ゴールド1つに対してのスコアの値
    const int scoreNum = 1;

    // サーバーに送信するためのクラス変数
    AllieScoreData scoreData = new AllieScoreData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }
    }

    /// <summary>
    /// 加算するスコアをサーバーに送信
    /// </summary>
    public async void SendAddScore()
    {
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }

        // サーバーに送信する変数の値を設定
        scoreData.playerID = ClientManager.Instance.playerID;   // playerIDは変動する可能性があるためここで代入する
        scoreData.allieScore = scoreNum;

        // サーバーに送信する
        await ClientManager.Instance.Send(scoreData, 14);
    }

    /// <summary>
    /// 減算するスコアをサーバーに送信
    /// </summary>
    /// <param name="loseGoldNum">ゴールドを失う数</param>
    public async void SendSubScore(int loseGoldNum)
    {
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }

        // サーバーに送信する変数の値を設定
        scoreData.playerID = ClientManager.Instance.playerID;   // playerIDは変動する可能性があるためここで代入する
        scoreData.allieScore = loseGoldNum * scoreNum * -1;

        // サーバーに送信する
        await ClientManager.Instance.Send(scoreData, 14);
    }
}
