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

    GameObject player;

    // �A�C�e�����E�����Ƃ��̃G�t�F�N�g
    [SerializeField] GameObject goldEffect;

    // �L���L�����Ă���
    [SerializeField] GameObject goldEffect2;

    GameObject childEffect;

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

        player = GameObject.Find("Player1");

        childEffect = Instantiate(goldEffect2,
            new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y,this.gameObject.transform.position.z),
             Quaternion.identity);

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

            if (other.gameObject.layer == 3)
            {// Player�̃��C���[

                if(other.gameObject.GetComponent<Player>() == null)
                {
                    return;
                }

                if (other.gameObject.GetComponent<Player>().playerObjID == targetID)
                {// �v���C���[�I�u�W�F�N�g��ID���^�[�Q�b�g��ID�ƈ�v����

                    if (ClientManager.Instance.playerID == targetID)
                    {// �^�[�Q�b�g��ID���������g��ID�̏ꍇ�͑��M����
                     // ���Z����X�R�A���T�[�o�[�ɑ��M����֐�
                        ScoreMethodList.Instance.SendAddScore();
                    }

                    GameObject childObject = Instantiate(goldEffect, player.transform);

                    childObject.transform.position = new Vector3(player.transform.position.x, 0.9f, player.transform.position.z);

                    Debug.Log(childObject);

                    // �j������
                    Destroy(this.gameObject);
                    Destroy(childEffect);
                    Debug.Log("�������");
                }

            }

        }
        else
        {
            if (other.gameObject.layer == 3)
            {
                GameObject childObject = Instantiate(goldEffect, player.transform);

                childObject.transform.position = new Vector3(player.transform.position.x, 0.9f, player.transform.position.z);

                // �j������
                Destroy(this.gameObject);
                Destroy(childEffect);
                Debug.Log("�������");
            }
               
        }
    }
}
