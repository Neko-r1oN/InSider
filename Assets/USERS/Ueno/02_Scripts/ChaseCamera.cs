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
        player = GameObject.Find("Player");

        // �T�u�J�����ƃv���C���[�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
