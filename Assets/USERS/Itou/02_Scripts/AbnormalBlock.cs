using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void Update()
    {

        if (this.transform.position.y <= 0.0f)
        {
            // transform���擾
            Transform myTransform = this.transform;

            // ���W���擾
            Vector3 pos = myTransform.position;
            pos.y += 0.005f;  // y���W��0.005���Z

            myTransform.position = pos;  // ���W��ݒ�
        }
    }
}
