using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    /// <summary>
    /// �����h���b�v���鏈��
    /// 
    /// </summary>

    bool isFirst; // �ŏ��̈��𔻒肷��t���O

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isFirst = true;
        }
    }

    void FixedUpdate()
    {
        // ��񂾂��Ă΂��
        if (isFirst)
        {
            isFirst = false;  // ���͂�����
            Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbody���擾
            Vector3 force = new Vector3(0.0f, 8.0f, 1.0f);  // �͂�ݒ�
            rb.AddForce(force, ForceMode.Impulse);          // �͂�������
        }
    }
}
