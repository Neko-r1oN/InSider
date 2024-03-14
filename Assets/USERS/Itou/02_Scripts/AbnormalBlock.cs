using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI用

public class NewBehaviourScript : MonoBehaviour
{

    void Update()
    {

        if (this.transform.position.y <= 0.0f)
        {
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;
            pos.y += 0.005f;  // y座標へ0.005加算

            myTransform.position = pos;  // 座標を設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (other.GetComponent<NavMeshAgent>() != null)
            {
                Debug.Log(other.name);

                // プレイヤーのスピードを変更
                other.GetComponent<NavMeshAgent>().speed = 1.5f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if (other.GetComponent<NavMeshAgent>() != null)
            {
                // プレイヤーのスピードを元に戻す
                other.GetComponent<NavMeshAgent>().speed = 4;
            }
        }
    }
}
