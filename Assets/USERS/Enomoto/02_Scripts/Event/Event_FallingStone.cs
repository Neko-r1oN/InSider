using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FallingStone : MonoBehaviour
{
    // �e�I�u�W�F�N�g(Warningmark)
    GameObject parentObj;

    private void Start()
    {
        // �擾����
        parentObj = this.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == parentObj)
        {// ���p�l�����X�^�[�g�p�l���̏ꍇ

            Debug.Log("�΂�j������");

            // �p�[�e�B�N������
            // Instantiate();

            // �e�I�u�W�F�N�g��j������
            Destroy(parentObj);
        }
    }
}
