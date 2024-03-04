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

        Debug.Log("�J�n�ʒu:"+startPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Block")
        {// �u���b�N�ɖ��܂���
            Debug.Log("���߂�ꂽ");

            // �ړI�n��ύX����
            //agent.destination = startPos;

            // �J�n���̈ʒu�֖߂�
            //transform.parent.transform.position = new Vector3(startPos.x, startPos.y, startPos.y);

            // �����p�[�e�B�N�����o��
            //Instantiate();
        }
    }
}
