using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FallingStone : MonoBehaviour
{
    // 親オブジェクト(Warningmark)
    GameObject parentObj;

    private void Start()
    {
        // 取得する
        parentObj = this.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == parentObj)
        {// 道パネルかスタートパネルの場合

            Debug.Log("石を破棄する");

            // パーティクル生成
            // Instantiate();

            // 親オブジェクトを破棄する
            Destroy(parentObj);
        }
    }
}
