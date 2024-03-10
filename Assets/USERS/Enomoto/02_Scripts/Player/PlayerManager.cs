using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // プレイヤーオブジェクトのリスト
    public List<GameObject> players;

    // トリガーのリスト (今だけ)
    public List<GameObject> triggerList;

    // Start is called before the first frame update
    void Start()
    {
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }

        for (int i = 0;i < players.Count;i++)
        {
            if(i >= ClientManager.Instance.playerNameList.Count)
            {// 存在しないプレイヤーの場合
                // 破棄する
                Destroy(players[i]);

                // リストから要素を削除
                players.RemoveAt(i);

                continue;
            }
            else
            {
                // IDを設定する
                players[i].GetComponent<Player>().playerObjID = i;
            }

            if (i != ClientManager.Instance.playerID)
            {// 自身のプレイヤーIDと一致しない場合
                // トリガー用のオブジェクトを破棄する
                Destroy(triggerList[i]);

                // 装備しているすべてのコライダーを取得する
                Collider[] CollArray = players[i].GetComponents<BoxCollider>();

                foreach (Collider collider in CollArray)
                {
                    // コライダーを破棄する
                    Destroy(collider);
                }
            }

            // アクティブ化
            players[i].SetActive(true);
        }
    }

    private void Update()
    {
        for(int i = 0;i < players.Count; i++)
        {
            Player player = players[i].GetComponent<Player>();

            if (player.mode == Player.PLAYER_MODE.DOWN)
            {
                player.BlinkPlayer();

                if (player.cnt >= 200)
                {
                    player.RecoverPlayer();
                }
            }
        }
    }
}
