using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockGoldManager : MonoBehaviour
{
    /// <summary>
    /// 金が掘ったプレイヤーを追いかける処理
    /// </summary>
    GameObject player;
    GameObject parentObj;
    public float speed = 5.0f;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        parentObj = transform.parent.gameObject;
    }

    void Update()
    {
        //スタート位置、ターゲットの座標、速度
        parentObj.transform.position = Vector3.MoveTowards(
          parentObj.transform.position,
          player.transform.position,
          speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 加算するスコアをサーバーに送信する関数
        ScoreMethodList.Instance.SendAddScore();

        // 破棄する
        Destroy(parentObj);
        Debug.Log("あたりめ");
    }
}
