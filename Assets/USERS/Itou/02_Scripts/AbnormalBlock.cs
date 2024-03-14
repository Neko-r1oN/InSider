using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI�p

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (other.GetComponent<NavMeshAgent>() != null)
            {
                Debug.Log(other.name);

                // �v���C���[�̃X�s�[�h��ύX
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
                // �v���C���[�̃X�s�[�h�����ɖ߂�
                other.GetComponent<NavMeshAgent>().speed = 4;
            }
        }
    }
}
