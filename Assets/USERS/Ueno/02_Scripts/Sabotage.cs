using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    [SerializeField] Text sabotageText;
    TextUIManager textUI;

    [SerializeField] Text timeText;

    GameObject parent;

    // �T�{�^�[�W��(���߂�)������I��������
    public bool isFill;

    // �T�{�^�[�W��(���e)������I��������
    public bool isBomb;

    // �T�{�^�[�W��(�X���[�g���b�v)������I��������
    public bool isTrap;

    public int timeNum;

    // �V���O���g���p
    public static Sabotage Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

        //parent = this.transform.parent.gameObject;

        if(timeNum > 0)
        {
            InvokeRepeating("SubCoolTime", 0, 1);

            Debug.Log("ee");
            //ResetCoolTime();
        }

        isFill = false;
        isBomb = false;
        isTrap = false;
    }

    /// <summary>
    /// �T�{�^�[�W��(���߂�)���[�h
    /// </summary>
    public void SabotageFill()
    {
        if(timeNum <= 0)
        {
            if (isFill == false)
            {
                // ���[�h�ύX
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;

                textUI.saboText.SetActive(true);

                // �T�{�^�[�W���{�^�����\��
                button.sabotageUI.SetActive(false);
                // �L�����Z���{�^����\��
                button.canselButton.SetActive(true);
            }
        }
    }

    /// <summary>
    /// �T�{�^�[�W��(���e)���[�h
    /// </summary>
    public void SabotageBomb()
    {
        if (timeNum <= 0)
        {
            if (isBomb == false)
            {
                // ���[�h�ύX
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;

                textUI.saboText.SetActive(true);

                // �T�{�^�[�W���{�^�����\��
                button.sabotageUI.SetActive(false);
                // �L�����Z���{�^����\��
                button.canselButton.SetActive(true);
            }
        }
            
    }

    /// <summary>
    /// �T�{�^�[�W��(�X���[�g���b�v)���[�h
    /// </summary>
    public void SbotageTrap()
    {
        if (timeNum <= 0)
        {
            if (isTrap == false)
            {
                // ���[�h�ύX
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGETRAP;

                // �T�{�^�[�W���{�^�����\��
                button.sabotageUI.SetActive(false);
                // �L�����Z���{�^����\��
                button.canselButton.SetActive(true);
            }
        }
    }

    /// <summary>
    /// bool��߂�
    /// </summary>
    public void ResetBool()
    {
        if(player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
        {
            isFill = false;
        }
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEBOMB)
        {
            isBomb = false;
        }
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGETRAP)
        {
            isTrap = false;
        }
    }

    /// <summary>
    /// �T�{�^�[�W���̃N�[���^�C������
    /// </summary>
    /// <returns></returns>
    public void SubCoolTime()
    {
        timeNum--;

        if(timeText != null)
        {
            timeText.text = "" + timeNum;
        }
        
        if (timeNum <= 0)
        {
            button.sabotageCoolTime.SetActive(false);
            CancelInvoke("SubCoolTime");
        }
    }

    /// <summary>
    /// �N�[���^�C����߂�
    /// </summary>
    public void ResetCoolTime()
    {
        timeNum = 60;

        InvokeRepeating("SubCoolTime", 0, 1);
    }

    /// <summary>
    ///  �T�{�^�[�W���̐�������\�����鏈��
    /// </summary>
    /// <param name="num"></param>
    public void SabotageText(int num)
    {
        switch (num)
        {
            case 0:
                sabotageText.text = "�؂�J���Ă��铹��4�������߂܂�";
                break;

            case 1:
                sabotageText.text = "���̏�ɔ��e��2�ݒu���܂�";
                break;

            case 2:
                sabotageText.text = "�I�������ꏊ�ɃX���[�g���b�v(3�~3)\n" +
                    "��ݒu���܂�";
                break;
        }
    }
}
