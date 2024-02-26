using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // UI
using Newtonsoft.Json;          // JSON�̃f�V���A���C�Y�Ȃ�
using System.Net.Sockets;       // NetworkStream�Ȃ�
using System.Threading.Tasks;   // �X���b�h�Ȃ�
using System.Threading;         // �X���b�h�Ȃ�
using System.Linq;              // Skip���\�b�h�Ȃ�
using System.Text;
using System;

public class connectManager : MonoBehaviour
{
    // ���C���X���b�h�ɏ������s���˗��������
    SynchronizationContext context;

    // NetworkStream���g�p
    NetworkStream stream;

    // �N���C�A���g�쐬
    TcpClient tcpClient;

    // ��������(OK)�{�^�������������ǂ���
    bool isReadyButton = false;

    // �ڑ����I�����邩�ǂ���
    bool isconnect = false;

    // �N���C�A���g�̖��O
    string clientName;

    // PlayerName�e�L�X�g���X�g
    public List<GameObject> clientTextList;

    // ���������e�L�X�g�̃��X�g
    public List<GameObject> readyTextList;

    // ���X�i�[�f�[�^���X�g
    List<ListenerData> listenerList;

    // �{�^�����X�g
    [SerializeField] List<GameObject> buttonObjList;

    /// <summary>
    /// �C�x���gID
    /// </summary>
    public enum EventID
    {
        ListenerList = 1,     // ���X�i�[�f�[�^
        ReadyData,            // ��������
        GameStart,            // �Q�[���J�n
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������
        tcpClient = new TcpClient();
        clientTextList = new List<GameObject>();
        readyTextList = new List<GameObject>();
        listenerList = new List<ListenerData>();
        context = SynchronizationContext.Current;

        Debug.Log("����"+clientTextList.Count);

        // �擾����
        clientName = TitleManager.UserName;

        // �񓯊����������s����
        StartConnect();
    }

    /// <summary>
    /// ��������(OK)�{�^����������
    /// </summary>
    public async void ReadyButton()
    {
        ReadyData readyData = new ReadyData();

        readyData.name = clientName;
        readyData.isReady = isReadyButton;

        // ���������҂��𑗐M
        await Send(readyData, tcpClient, 2);
        
        // �t���O��؂�ւ�
        isReadyButton = !isReadyButton;

        // �\���E��\���؂�ւ�
        buttonObjList[0].SetActive(isReadyButton);
        buttonObjList[1].SetActive(!isReadyButton);
    }

    /// <summary>
    /// ��M����(�X���b�h)
    /// </summary>
    /// <param name="arg"></param>
    async void RecvProc(object arg)
    {
        // �N���C�A���g�쐬
        TcpClient tcpClient = (TcpClient)arg;

        // NetworkStream���g�p
        stream = tcpClient.GetStream();

        while (true)
        {
            // ��M�ҋ@����
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // ��M����

            // �ڑ��ؒf�`�F�b�N
            if (isconnect == true)
            {
                break;
            }

            // ��M�f�[�^����C�x���gID�����o��
            int eventID = receiveBuffer[0];

            // ��M�f�[�^����JSON����������o��
            byte[] bufferJson = receiveBuffer.Skip(1).ToArray();    // �P�o�C�g�ڂ��X�L�b�v
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);  // ��M�������̂�byte����string�ɕϊ�

            //*************************************
            //  ���C���X���b�h�ɏ������s���˗�
            //*************************************

            context.Post(_ =>
            {
                switch (eventID)
                {
                    case 1: // �C�x���gID���P�̏������s

                        Debug.Log("�v���C���[������M����");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        listenerList = JsonConvert.DeserializeObject<List<ListenerData>>(jsonString);

                        // �v���C���[�̖��O�𔽉f
                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (listenerList.Count > i)
                            {// �C���f�b�N�X���͈͓��̏ꍇ
                                // Text���X�V����
                                clientTextList[i].GetComponent<Text>().text = listenerList[i].name;
                            }
                            else
                            {// ����������
                                // Text���X�V����
                                clientTextList[i].GetComponent<Text>().text = (i + 1) + "";
                            }
                        }

                        break;
                    case 2: // �C�x���gID���Q�̏������s

                        Debug.Log("��������|�����҂��ʒm����M");

                        List<ReadyData> readyData = JsonConvert.DeserializeObject<List<ReadyData>>(jsonString);

                        for (int cnt = 0; cnt < clientTextList.Count; cnt++)
                        {
                            for (int i = 0; i < readyData.Count; i++)
                            {
                                // �ύX�_�FclientTextList��Client�X�N���v�g���������Ă���

                                if (clientTextList[cnt].GetComponent<Text>().text == readyData[i].name)
                                {// ���O����v����
                                    if (readyData[i].isReady == true)
                                    {// �^
                                        readyTextList[cnt].SetActive(true);
                                    }
                                    else
                                    {// �U
                                        readyTextList[cnt].SetActive(false);
                                    }

                                    // ���̃��[�v���ʂ���
                                    break;
                                }
                                else
                                {// �U
                                    readyTextList[cnt].SetActive(false);
                                }
                            }
                        }

                        break;
                    case 3:
                        // �A�N�e�B�u��
                        
                        break;
                }
            }, null);
        }

        // �ڑ���ؒf
        tcpClient.Close();

        tcpClient = null;
    }

    /// <summary>
    /// �ڑ����� && �X���b�h�N��
    /// </summary>
    private async void StartConnect()
    {
        // ���g�̏����擾
        ListenerData listener = new ListenerData(); // �C���X�^���X��
        listener.name = clientName;  // ���g�̖��O����

        // ���g�̏��������f�[�^���擾
        ReadyData readyData = new ReadyData();
        readyData.name = clientName;
        readyData.isReady = false;

        try
        {
            //**********************************
            //  �ڑ��̗v�� & �ʐM�̊m��
            //**********************************

            // ����M�̃^�C���A�E�g��ݒ�(msec)
            tcpClient.SendTimeout = 1000;       // ���M
            tcpClient.ReceiveTimeout = 1000;    // ��M

            // �T�[�o�[�֐ڑ��v��
            await tcpClient.ConnectAsync("127.0.0.1", 20000);
            Debug.Log("***�T�[�o�[�ƒʐM�m��***");

            //**********************************
            //  ���g�̏��𑗐M����
            //**********************************

            // ���M����
            await Send(listener, tcpClient, 1);

            //**********************************
            //  ���������f�[�^�𑗐M����
            //**********************************

            // ����M����
            ReadyButton();
            //await Send(readyData, tcpClient, 2);

            //**********************************
            //  ��M�p�̃X���b�h���N��
            //**********************************

            Thread thread = new Thread(new ParameterizedThreadStart(RecvProc));
            thread.Start(tcpClient);
        }
        catch (Exception ex)
        {
            Debug.Log("�ڑ��ł��Ȃ��B��ł܂�������");
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// ���M����
    /// </summary>
    /// <param name="param"></param>
    /// <param name="arg"></param>
    public async Task Send(object data, object arg, int eventID)
    {
        // ��������N���C�A���g���L���X�g�Ŏ擾����
        TcpClient tcpClientList = (TcpClient)arg;

        // JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(data);  // �N���X�^�̕ϐ����w��

        // Json�������byte�z��ɕϊ�
        byte[] sendData = Encoding.UTF8.GetBytes(json);

        // ���M�f�[�^�̐擪�ɃC�x���gID��ǉ�����
        sendData = sendData.Prepend((byte)eventID).ToArray();    // ���K��ID���m�F���邱��!!!!!!!!!!

        // ���M�f�[�^�̔z�񐔂��Œ�ɕύX
        Array.Resize(ref sendData, 1024);   // �T�C�Y���Œ肵�Ĉ���ɑ���M�ł���悤�ɂ���

        // ���M����
        NetworkStream stream = tcpClientList.GetStream();
        await stream.WriteAsync(sendData, 0, sendData.Length);  // ���M
    }

    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            // �ڑ���ؒf
            tcpClient.Close();
        }
    }
}
