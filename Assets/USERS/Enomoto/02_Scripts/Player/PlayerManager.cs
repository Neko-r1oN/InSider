using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // プレイヤーオブジェクトのリスト
    public List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }

        for (int i = 0;i < players.Count;i++)
        {
            if(i >= ClientManager.Instance.playerNum)
            {// 存在しないプレイヤーの場合
                // 破棄する
                Destroy(players[i]);
                continue;
            }

            if(i == ClientManager.Instance.playerID)
            {// 自身のプレイヤーIDと一致する
                players[i].GetComponent<OtherPlayer>().enabled = false; // スクリプトを無効にする
            }
            else
            {
                // コンポーネントを削除する
                Destroy(players[i].GetComponent<Player>());

                // 装備しているすべてのコライダーを取得する
                Collider[] CollArray = players[i].GetComponents<BoxCollider>();

                foreach(Collider collider in CollArray)
                {
                    // コライダーを破棄する
                    Destroy(collider);
                }
            }

            // アクティブ化
            players[i].SetActive(true);
        }
    }
}
