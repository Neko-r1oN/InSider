using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // �~�~�b�N���ǂ���

    GameObject textUI;

    // �`�F�X�g�̒��g��\������e�L�X�g
    Text chestText;

    GameObject player;

    public int chestNum;

    bool isPlayer;  // �v���C���[�����m�������ǂ���
    private void Start()
    {
        isPlayer = false;

        textUI = GameObject.Find("TextUIManager");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            //**********************************
            // ray���`�F�X�g�ɓ������Ă�����
            //**********************************
            if (hit.transform.gameObject == this.gameObject)
            {
                //*********************************
                //�T�[�o�[���g���Ă���Ƃ�
                //*********************************
                if (EditorManager.Instance.useServer == true)
                {
                    if (ClientManager.Instance.isInsider)
                    {
                        if (isMimic == true)
                        {// �~�~�b�N��������
                            // �~�~�b�N�̃e�L�X�g��\��
                            textUI.GetComponent<TextUIManager>().OnMouseEnter(10);
                        }
                        else
                        {// �~�~�b�N����Ȃ��Ƃ�
                            // �^�񒆂̃`�F�X�g
                            if (chestNum == 1)
                            {
                                // OnMouseEnter(11)�͂�����΂�
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(11);
                            }
                            // �E�̃`�F�X�g
                            else if (chestNum == 2)
                            {
                                // OnMouseEnter(12)�͂�����΂�
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(12);
                            }
                            // ���̃`�F�X�g
                            else if (chestNum == 3)
                            {
                                // OnMouseEnter(13)�͂�����΂�
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(13);
                            }
                        }
                    }
                }
                //********************************
                // �T�[�o�[���g���Ă��Ȃ��Ƃ�
                //********************************
                else
                {
                    // �^�񒆂̃`�F�X�g
                    if (chestNum == 1)
                    {
                        // OnMouseEnter(11)�͂�����΂�
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(11);
                    }
                    // �E�̃`�F�X�g
                    else if (chestNum == 2)
                    {
                        // OnMouseEnter(12)�͂�����΂�
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(12);
                    }
                    // ���̃`�F�X�g
                    else if (chestNum == 3)
                    {
                        // OnMouseEnter(13)�͂�����΂�
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(13);
                    }
                }
            }
            //***************************************
            // ray���O�ꂽ��
            //***************************************
            else if (hit.transform.gameObject != this.gameObject)
            {
                //*********************************
                //�T�[�o�[���g���Ă���Ƃ�
                //*********************************
                if (EditorManager.Instance.useServer == true)
                {
                    if (ClientManager.Instance.isInsider)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }
                        else
                        {
                            // �^�񒆂̃`�F�X�g
                            if (chestNum == 1)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(11);
                            }
                            // �E�̃`�F�X�g
                            else if (chestNum == 2)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(12);
                            }
                            // ���̃`�F�X�g
                            else if (chestNum == 3)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(13);
                            }
                        }
                    }
                }
                //*********************************
                //�T�[�o�[���g���Ă��Ȃ��Ƃ�
                //*********************************
                else
                {
                    // �^�񒆂̃`�F�X�g
                    if (chestNum == 1)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(11);
                    }
                    // �E�̃`�F�X�g
                    else if (chestNum == 2)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(12);
                    }
                    // ���̃`�F�X�g
                    else if (chestNum == 3)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(13);
                    }
                }
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer == true)
        {
            return;
        }

        if (other.gameObject.layer == 3)
        {// �v���C���[�̏ꍇ

            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// �������g�̏ꍇ

                Debug.Log("OKOKOKOKO:" + other.gameObject.name);

                if (ClientManager.Instance.isInsider == false)
                {// Insider�ł͂Ȃ��ꍇ
                    SendRoundEndData();
                }
            }
        }
    }

    private async void SendRoundEndData()
    {
        if (isPlayer == true)
        {
            return;
        }

        isPlayer = true;

        // �N���X�ϐ����쐬
        RoundEndData roundEndData = new RoundEndData();
        roundEndData.isMimic = isMimic;
        roundEndData.openPlayerID = ClientManager.Instance.playerID;

        if (isMimic == true)
        {// ���g���~�~�b�N�̏ꍇ
            Debug.Log("�~�~�b�N");
        }
        else
        {// �󔠂̏ꍇ
            Debug.Log("��");
        }

        // �T�[�o�[�ɑ��M����
        await ClientManager.Instance.Send(roundEndData, 4);
    }
}
