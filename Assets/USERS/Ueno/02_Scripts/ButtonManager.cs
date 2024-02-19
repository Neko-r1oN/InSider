using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject roadUI; // RoadUI�I�u�W�F�N�g�̎擾
    [SerializeField] GameObject actionButton; // action�{�^���̎擾
    [SerializeField] GameObject fillButton; // fill�{�^���̎擾
    [SerializeField] GameObject moveButton; // move�{�^���̎擾
    [SerializeField] GameObject nothingButton; //nothing�{�^���̎擾
    [SerializeField] GameObject sabotageButton; //sabotage�{�^���̎擾
    int stamina = 100;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void PlayerMove()
    {
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i" + stamina);
    }

    public void CutOpen()
    {//�؂�J����I�񂾏ꍇ

        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;

        //roadUI.SetActive(true);

        if (roadUI == true)
        {
            // ���̑��̃{�^�����\��
            moveButton.SetActive(false);
            fillButton.SetActive(false);
            nothingButton.SetActive(false);
            sabotageButton.SetActive(false);
            actionButton.SetActive(false);
        }

        
        stamina -= 10;
        Debug.Log("�c��X�^�~�i"+ stamina);
    }

    public void fill()
    {//���߂��I�񂾏ꍇ

        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i" + stamina);
    }

    public void Nothing()
    {
        if (player.GetComponent<Player>().isEnd == false)
        {// �v���C���[���ړ����̏ꍇ
            return;
        }

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i" + stamina);
    }

    public void ButtonCancel()
    {
        if(roadUI == true)
        {
            roadUI.SetActive(false);

            // ���̑��̃{�^����\��
            moveButton.SetActive(true);
            fillButton.SetActive(true);
            nothingButton.SetActive(true);
            sabotageButton.SetActive(true);
            actionButton.SetActive(true);
        }
    }
}
