using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FallingStone : MonoBehaviour
{
    // 親オブジェクト(Warningmark)
    GameObject parentObj;

    [SerializeField] GameObject smoke;

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
           GameObject childObj = Instantiate(smoke,
               new Vector3(this.gameObject.transform.position.x,0,this.gameObject.transform.position.z), Quaternion.identity);

            // 親オブジェクトを破棄する
            Destroy(parentObj);
        }
    }
}
