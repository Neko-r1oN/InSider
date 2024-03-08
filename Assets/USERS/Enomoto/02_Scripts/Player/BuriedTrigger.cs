using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BuriedTrigger : MonoBehaviour
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
        if(other.transform.tag == "Block" && isBuried == false) // ���������x������̂�j�~
        {// �u���b�N�ɖ��܂���
            Debug.Log("���߂�ꂽ");

            // �N���X�ϐ����쐬
            RevisionPosData revisionPosData = new RevisionPosData();
            revisionPosData.playerID = ClientManager.Instance.playerID;
            revisionPosData.targetID = ClientManager.Instance.playerID;
            revisionPosData.isBuried = true;
            revisionPosData.targetPosX = 0f;
            revisionPosData.targetPosY = 0.9f;
            revisionPosData.targetPosZ = -5f;

            // [revisionPosData]�T�[�o�[�ɑ��M
            await ClientManager.Instance.Send(revisionPosData, 12);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �U�ɂ���
        isBuried = false;
    }
}
