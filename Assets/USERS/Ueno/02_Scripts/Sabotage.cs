using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    Text sabotageText;

    private void Awake()
    {
        sabotageText = GameObject.Find("SabotageText").GetComponent<Text>();
    }

    private void Start()
    {
        player = GameObject.Find("Player1");

        GameObject buttonManager = GameObject.Find("ButtonManager");
        button = buttonManager.GetComponent<ButtonManager>();
    }

    /// <summary>
    /// �T�{�^�[�W��(���߂�)���[�h
    /// </summary>
    public void SabotageFill()
    {
        // ���[�h�ύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;
        // �T�{�^�[�W���{�^�����\��
        button.sabotage.SetActive(false);
        // �L�����Z���{�^����\��
        button.canselButton.SetActive(true);
    }

    /// <summary>
    /// �T�{�^�[�W��(���e)���[�h
    /// </summary>
    public void SabotageBomb()
    {
        // ���[�h�ύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;
        // �T�{�^�[�W���{�^�����\��
        button.sabotage.SetActive(false);
        // �L�����Z���{�^����\��
        button.canselButton.SetActive(true);
    }

    public void SabotageText(int num)
    {
        switch (num)
        {
            case 0:
                sabotageText.text = "�؂�J���Ă��铹��4�������߂܂�";
                break;

            case 1:
                sabotageText.text = "���̏�ɔ��e��3�ݒu���܂�";
                break;

            case 2:
                sabotageText.text = "�I�������ꏊ�ɃX���[�g���b�v(3�~3)\n" +
                    "��ݒu���܂�";
                break;
        }
    }
}
