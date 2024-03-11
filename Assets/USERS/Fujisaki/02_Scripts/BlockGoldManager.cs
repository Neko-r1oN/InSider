using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockGoldManager : MonoBehaviour
{
    /// <summary>
    /// �����@�����v���C���[��ǂ������鏈��
    /// </summary>
    GameObject player;
    GameObject parentObj;
    public float speed = 5.0f;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.turnPlayerID];

            // ������ŏC�����邩��
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }

        parentObj = transform.parent.gameObject;
    }

    void Update()
    {
        //�X�^�[�g�ʒu�A�^�[�Q�b�g�̍��W�A���x
        parentObj.transform.position = Vector3.MoveTowards(
          parentObj.transform.position,
          player.transform.position,
          speed * Time.deltaTime);
    }

    /// <summary>
    /// �v���C���[�Ƌ�������������������� [�C���X�y�N�^�[��Ń��C���[���w��ς�]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            if(other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// �v���C���[�I�u�W�F�N�g��ID�����g��ID�̏ꍇ
                // ���Z����X�R�A���T�[�o�[�ɑ��M����֐�
                ScoreMethodList.Instance.SendAddScore();
            }
        }

        // �j������
        Destroy(parentObj);
        Debug.Log("�������");
    }
}
