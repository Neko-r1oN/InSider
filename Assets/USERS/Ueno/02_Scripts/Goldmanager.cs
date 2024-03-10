using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmanager : MonoBehaviour
{
    int rotY;

    GameObject parentObj;

    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
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
    /// プレイヤーと金が当たったら消す処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            // 加算するスコアをサーバーに送信する関数
            ScoreMethodList.Instance.SendAddScore();
        }

        Destroy(parentObj);
        Debug.Log("当たった");
    }
}
