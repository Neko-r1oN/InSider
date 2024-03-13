using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockGoldManager : MonoBehaviour
{
    /// <summary>
    /// �����@�����v���C���[��ǂ������鏈��
    /// </summary>
    GameObject playerManager;
    GameObject targetObj;
    public float speed = 5.0f;
    bool isStart;

    // �ǔ�����v���C���[��ID
    public int targetID;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ�

            isStart = false;

            // ID��������
            targetID = 0;

            playerManager = GameObject.Find("player-List");

            StartMove(0);
        }

        Debug.Log("�������͋�����");
    }

    void Update()
    {
        if(isStart == false)
        {
            return;
        }

        //�X�^�[�g�ʒu�A�^�[�Q�b�g�̍��W�A���x
        transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, speed * Time.deltaTime);

        // Y���W���Œ肷��
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    /// <summary>
    /// Update�������J�n
    /// </summary>
    public void StartMove(int id)
    {
        targetID = id;
        playerManager = GameObject.Find("player-List");
        targetObj = playerManager.GetComponent<PlayerManager>().players[targetID];
        isStart = true;

        Debug.Log("�X�^�[�g");
    }

    /// <summary>
    /// �v���C���[�Ƌ�������������������� [�C���X�y�N�^�[��Ń��C���[���w��ς�]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ

            if(other.GetComponent<Player>().playerObjID == targetID)
            {// �v���C���[�I�u�W�F�N�g��ID���^�[�Q�b�g��ID�ƈ�v����

                if (ClientManager.Instance.playerID == targetID)
                {// �^�[�Q�b�g��ID���������g��ID�̏ꍇ�͑��M����
                    // ���Z����X�R�A���T�[�o�[�ɑ��M����֐�
                    ScoreMethodList.Instance.SendAddScore();
                }

                // �j������
                Destroy(this.gameObject);
                Debug.Log("�������");
            }
        }
        else
        {
            // �j������
            Destroy(this.gameObject);
            Debug.Log("�������");
        }
    }
}
