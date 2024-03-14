using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BuriedAndHit : MonoBehaviour
{
    GameObject player;  // �v���C���[
    NavMeshAgent agent; // �G�[�W�F���g
    Vector3 startPos;   // �J�n���̍��W

    // �g���K�[�̔��肪���������ǂ���
    bool isBuried;

    // Start is called before the first frame update
    void Start()
    {
        // ���̐e���擾����
        player = transform.parent.gameObject;

        // Agent�擾
        agent = player.GetComponent<NavMeshAgent>();

        // �J�n���̍��W�擾����
        startPos = player.transform.position;
    }

    private async Task OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ

            // �N���X�ϐ����쐬
            RevisionPosAndDropGoldData revisionPosData = new RevisionPosAndDropGoldData();

            Player playerCom = player.GetComponent<Player>();

            if(playerCom.mode == Player.PLAYER_MODE.MINING || playerCom.mode == Player.PLAYER_MODE.FILL)
            {// �̌@�E���߂�Mode�̂Ƃ�
                return;
            }

            if (other.transform.tag == "Block" 
                && playerCom.isInvincible == false  // �_�E����Ԃł͂Ȃ��ꍇ
                && isBuried == false) // ���������x������̂�j�~
            {// �u���b�N�ɖ��܂���

                Debug.Log("���߂�ꂽ");

                // �N���X�ϐ����쐬
                revisionPosData.playerID = ClientManager.Instance.playerID;
                revisionPosData.targetID = ClientManager.Instance.playerID;
                revisionPosData.isBuried = true;
                revisionPosData.targetPosX = 0f;
                revisionPosData.targetPosY = 0.9f;
                revisionPosData.targetPosZ = -5f;

                // [revisionPosData]�T�[�o�[�ɑ��M
                await ClientManager.Instance.Send(revisionPosData, 12);
            }
            else if (other.gameObject.layer == 7)
            {// �G�ɐG�ꂽ || �C�x���g�̗��΂���������

                if (player.GetComponent<Player>().isInvincible == false)
                {// Player���_�E�����Ă��Ȃ��ꍇ

                    Debug.Log("�G�ɍU�����ꂽ");

                    // �N���X�ϐ����쐬
                    revisionPosData.playerID = ClientManager.Instance.playerID;
                    revisionPosData.targetID = ClientManager.Instance.playerID;
                    revisionPosData.isDown = true;

                    // [revisionPosData]�T�[�o�[�ɑ��M
                    await ClientManager.Instance.Send(revisionPosData, 12);
                }
            }
        }
        else
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            if (other.gameObject.layer == 7 && player.GetComponent<Player>().isInvincible == false)
            {// �G�ɐG�ꂽ && Player���_�E�����Ă��Ȃ��ꍇ
                Debug.Log("�^�b�`���ꂽ");

                player.GetComponent<Player>().DownPlayer(4);

                Debug.Log(player.GetComponent<Player>().mode);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        // �U�ɂ���
        isBuried = false;
    }
}
