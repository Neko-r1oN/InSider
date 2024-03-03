using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    private void Start()
    {
        // �v���C���[���̎擾
        player = GameObject.Find("player-List");
        player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];

        // �T�u�J�����ƃv���C���[�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        //Start�ŋ��߂��v���C���[�Ƃ̈ʒu�֌W����ɃL�[�v����悤�ɃJ�����𓮂���
        transform.position = player.transform.position + offset;
    }
}
