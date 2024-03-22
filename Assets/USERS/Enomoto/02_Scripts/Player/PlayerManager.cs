using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // プレイヤーオブジェクトのリスト
    public List<GameObject> players;

    // トリガーのリスト
    public List<GameObject> triggerList;

    private void Awake()
    {
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない場合
            return;
        }

        // インスペクター上で設定してあるオブジェクトがあるので初期化する
        players = new List<GameObject>();

        Debug.Log("プレイヤー人数：" + ClientManager.Instance.playerNameList.Count);

        // 子オブジェクトの数分の配列を作成
        GameObject[] children = new GameObject[this.transform.childCount];

        // 子オブジェクトを順に取得する
        for (int i = 0; i < children.Length; i++)
        {
            // 子オブジェクトを取得
            Transform childTransform = this.transform.GetChild(i);

            // Playerオブジェクトをリストに追加する
            players.Add(childTransform.gameObject);

            // IDを設定する
            players[i].GetComponent<Player>().playerObjID = i;

            if (i < ClientManager.Instance.playerNameList.Count && ClientManager.Instance.isConnectList[i] == true)
            {// 存在するプレイヤーの場合

                Debug.Log(i + "追加");

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
            else
            {// 存在しないプレイヤーの場合

                Debug.Log(i + "破棄");

                Destroy(childTransform.gameObject);
            }
        }

        
    }

    /// <summary>
    /// 点滅のコルーチンを開始
    /// </summary>
    public IEnumerator StartBlink(int indexNum)
    {
        bool isActive = false;

        for (int i = 0; i < 20; i++)
        {
            players[indexNum].SetActive(isActive);

            // フラグを切り替え
            isActive = !isActive;

            yield return new WaitForSeconds(0.25f);
        }

        // ダウン状態を解除する
        players[indexNum].GetComponent<Player>().RevisionPos(new Vector3(0f, 0.9f, -5f));

        yield return new WaitForSeconds(5f);

        // 無敵状態を解除する
        players[indexNum].GetComponent<Player>().isInvincible = false;
    }
}
