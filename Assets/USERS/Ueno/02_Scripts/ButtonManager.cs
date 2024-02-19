using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject player;

    // �X�^�~�i����̐��l�����߂�ϐ�
    public int subStamina;
    
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void PlayerMove()
    {
        // �v���C���[�̃��[�h��MOVE�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public void CutOpen()
    {//�؂�J����I�񂾏ꍇ

        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        // �v���C���[�̃��[�h��MINING�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;
    }

    public void fill()
    {//���߂��I�񂾏ꍇ

        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        // �v���C���[�̃��[�h��FILL�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;

        // �X�^�~�i�����炷
        //player.GetComponent<Player>().SubStamina(subStamina);
    }

    public void Nothing()
    {
        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        // �v���C���[�̃��[�h��NOTHING�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        // �X�^�~�i�����炷
        player.GetComponent<Player>().SubStamina(subStamina);
    }
}
