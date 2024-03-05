using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class BuriedTrigger : MonoBehaviour
{
    GameObject player;  // �v���C���[
    NavMeshAgent agent; // �G�[�W�F���g
    Vector3 startPos;   // �J�n���̍��W

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Block")
        {// �u���b�N�ɖ��܂���
            Debug.Log("���߂�ꂽ");

            // �R���|�[�l���g�𖳌��ɂ���
            agent.enabled = false;

            // �J�n�ʒu�ֈړ�
            player.transform.position = new Vector3(0f,0.9f,-5f);

            // �R���|�[�l���g��L���ɂ���
            agent.enabled = true;

            // �����p�[�e�B�N�����o��
            //Instantiate();
        }
    }
}
