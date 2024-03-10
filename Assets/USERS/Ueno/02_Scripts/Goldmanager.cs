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
        // Y���W��������]������
        rotY += 1;

        // ����Y���W����]������
        parentObj.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

        // Y���W��360�x�ɂȂ�����
        if (rotY >= 360)
        {
            // 0�ɖ߂�
            rotY = 0;
        }
    }

    /// <summary>
    /// �v���C���[�Ƌ��������������������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            // ���Z����X�R�A���T�[�o�[�ɑ��M����֐�
            ScoreMethodList.Instance.SendAddScore();
        }

        Destroy(parentObj);
        Debug.Log("��������");
    }
}
