using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmanager : MonoBehaviour
{
    int rotY;

    GameObject parentObj;

    // �L���L���G�t�F�N�g
    [SerializeField] GameObject goldEffect;
    GameObject childObj;

    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.parent.gameObject;

        childObj = Instantiate(goldEffect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z),
             Quaternion.identity);

        if (parentObj.layer != 12)
        {// �C�x���g�p�ł͂Ȃ��ꍇ
         //�h���b�v����O���̐ݒ�
            Rigidbody rb = parentObj.GetComponent<Rigidbody>();  // rigidbody���擾
            float randx = Random.Range(-5.0f, 5.0f);
            float randz = Random.Range(-5.0f, 5.0f);
            Vector3 force = new Vector3(randx, 12.0f, randz);  // �͂�ݒ�
            rb.AddForce(force, ForceMode.Impulse);          // �͂�������
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
    /// �v���C���[�Ƌ�������������������� [�C���X�y�N�^�[��Ń��C���[���w��ς�]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null)
        {// ��A�N�e�B�u�̂Ƃ�
            return;
        }

        // �_�E����Ԃ�������return
        if(other.GetComponent<Player>().mode == Player.PLAYER_MODE.DOWN)
        {
            return;
        }

        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// �v���C���[�I�u�W�F�N�g��ID�����g��ID�̏ꍇ
                // ���Z����X�R�A���T�[�o�[�ɑ��M����֐�
                ScoreMethodList.Instance.SendAddScore();
            }
        }

        Destroy(parentObj);
        Destroy(childObj);
        Debug.Log("��������");
    }
}
