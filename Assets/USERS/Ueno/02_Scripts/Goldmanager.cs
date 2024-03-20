using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmanager : MonoBehaviour
{
    int rotY;

    GameObject parentObj;

    // キラキラエフェクト
    [SerializeField] GameObject goldEffect;
    GameObject childObj;

    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.parent.gameObject;

        childObj = Instantiate(goldEffect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z),
             Quaternion.identity);

        if (parentObj.layer != 12)
        {// イベント用ではない場合
         //ドロップする軌道の設定
            Rigidbody rb = parentObj.GetComponent<Rigidbody>();  // rigidbodyを取得
            float randx = Random.Range(-5.0f, 5.0f);
            float randz = Random.Range(-5.0f, 5.0f);
            Vector3 force = new Vector3(randx, 12.0f, randz);  // 力を設定
            rb.AddForce(force, ForceMode.Impulse);          // 力を加える
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Y座標だけを回転させる
        rotY += 1;

        // 金のY座標を回転させる
        parentObj.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

        // Y座標が360度になったら
        if (rotY >= 360)
        {
            // 0に戻す
            rotY = 0;
        }
    }

    /// <summary>
    /// プレイヤーと金が当たったら消す処理 [インスペクター上でレイヤーを指定済み]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null)
        {// 非アクティブのとき
            return;
        }

        // ダウン状態だったらreturn
        if(other.GetComponent<Player>().mode == Player.PLAYER_MODE.DOWN)
        {
            return;
        }

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// プレイヤーオブジェクトのIDが自身のIDの場合
                // 加算するスコアをサーバーに送信する関数
                ScoreMethodList.Instance.SendAddScore();
            }
        }

        Destroy(parentObj);
        Destroy(childObj);
        Debug.Log("当たった");
    }
}
