using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    [SerializeField] Text sabotageText;

    // �T�{�^�[�W��(�X���[�g���b�v)�̃{�^��
    [SerializeField] GameObject sabotage3;

    TextUIManager textUI;

    // �T�{�^�[�W��(���߂�)������I��������
    public int fillCount;

    // �T�{�^�[�W��(���e)������I��������
    public int bombCount;

    // �T�{�^�[�W��(�X���[�g���b�v)������I��������
    public int trapCount;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }

        GameObject textUIObj = GameObject.Find("TextUIManager");
        textUI = textUIObj.GetComponent<TextUIManager>();

        GameObject buttonManager = GameObject.Find("ButtonManager");
        button = buttonManager.GetComponent<ButtonManager>();
    }

    /// <summary>
    /// �T�{�^�[�W��(���߂�)���[�h
    /// </summary>
    public void SabotageFill()
    {
        if(fillCount <= 0)
        {
            // ���[�h�ύX
            player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;

            textUI.saboText.SetActive(true);

            // �T�{�^�[�W���{�^�����\��
            button.sabotageUI.SetActive(false);
            // �L�����Z���{�^����\��
            button.canselButton.SetActive(true);

            fillCount++;
        }
    }

    /// <summary>
    /// �T�{�^�[�W��(���e)���[�h
    /// </summary>
    public void SabotageBomb()
    {
        if(bombCount <= 0)
        {
            // ���[�h�ύX
            player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;

            textUI.saboText.SetActive(true);

            // �T�{�^�[�W���{�^�����\��
            button.sabotageUI.SetActive(false);
            // �L�����Z���{�^����\��
            button.canselButton.SetActive(true);

            bombCount++;
        }
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
