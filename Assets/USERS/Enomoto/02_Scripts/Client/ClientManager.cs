using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // UI
using Newtonsoft.Json;          // JSON�̃f�V���A���C�Y�Ȃ�
using System.Net.Sockets;       // NetworkStream�Ȃ�
using System.Threading.Tasks;   // �X���b�h�Ȃ�
using System.Threading;         // �X���b�h�Ȃ�
using System.Linq;              // Skip���\�b�h�Ȃ�
using UnityEngine.SceneManagement;  // �V�[���J��
using System.Text;
using System;

public class ClientManager : MonoBehaviour
{
    //====================================
    //  [Unity�C���X�y�N�^�[]�t�B�[���h
    //====================================

    // PlayerName�e�L�X�g���X�g
    public List<GameObject> clientTextList = new List<GameObject>();

    // Player���f���̃��X�g
    public List<GameObject> modelList = new List<GameObject>();

    // ���������e�L�X�g�̃��X�g
    public List<GameObject> readyTextList = new List<GameObject>();

    // ���X�i�[�f�[�^���X�g
    List<ListenerData> listenerList = new List<ListenerData>();

    // �{�^�����X�g
    [SerializeField] List<GameObject> buttonObjList;

    //===========================
    //  [����J]�t�B�[���h
    //===========================

    // ���C���X���b�h�ɏ������s���˗��������
    SynchronizationContext context;

    // NetworkStream���g�p
    NetworkStream stream;

    // ��������(OK)�{�^�������������ǂ���
    bool isReadyButton = false;

    // �ڑ���ؒf���ďI�����邩�ǂ���
    bool isDisconnect = false;

    // �N���C�A���g�̖��O
    string clientName;

    // �v���C���[�̃}�l�[�W���[(List)
    GameObject playerManager;

    // �u���b�N(���p�l��)�}�l�[�W���[
    GameObject blockManager;

    // �N�̃^�[����
    GameObject text;

    // �K�v�ڑ��l��
    int RequiredNum = 2;

    //===========================
    //  [���J]�t�B�[���h
    //===========================

    /// <summary>
    /// �N���C�A���g
    /// </summary>
    public TcpClient tcpClient { get; set; }

    /// <summary>
    /// ���g�̃v���C���[ID (1P,2P�Ȃ�)
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// ��E�����ʎ҂��ǂ���
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// ��s�̃v���C���[ID
    /// </summary>
    public int advancePlayerID { get; set; }

    /// <summary>
    /// �v���C���[�l��
    /// </summary>
    public int playerNum { get; set; }

    /// <summary>
    /// �C�x���gID
    /// </summary>
    public enum EventID
    {
        PlayerID = 0,         // �v���C���[ID(1P,2P�E�E�E)
        ListenerList,         // ���X�i�[�f�[�^
        ReadyData,            // ��������
        JobAndTurn,           // ��E�Ɛ�s�̃v���C���[ID
        ChatData,             // �`���b�g
        MoveData,             // �ړ�
        Action_FillData,      // �s���F���߂�
        Action_MiningData,    // �s���F�؂�J��
        Action_NothingData,   // �s���F�������Ȃ�
    }

    //===========================
    //  [�ÓI]�t�B�[���h
    //===========================

    // �V���O���g���p
    public static ClientManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            // �V�[���J�ڂ��Ă��j�����Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);

            Debug.Log("OK");
        }
        else
        {
            Destroy(gameObject);

            Debug.Log("NO");
        }
    }

    /// <summary>
    /// �J�n���̏���
    /// </summary>
    void Start()
    {
        // ������
        tcpClient = new TcpClient();
        context = SynchronizationContext.Current;

        playerID = 0;

        // �擾����
        clientName = TitleManager.UserName;

        // �񓯊����������s����
        StartConnect();
    }

    /// <summary>
    /// �ڑ��ؒf(�ޏo)�{�^��
    /// </summary>
    public void DisconnectButton()
    {
        // �^
        isDisconnect = true;

        Instance = null;

        // �ڑ���ؒf
        tcpClient.Close();

        // �t�F�[�h���V�[���J��
        Initiate.DoneFading();
        SceneManager.LoadScene("TitleKawaguchi");

        Debug.Log("�ޏo");
    }

    /// <summary>
    /// ��������(OK)�{�^����������
    /// </summary>
    public async void ReadyButton()
    {
        ReadyData readyData = new ReadyData();

        readyData.id = playerID;
        readyData.isReady = isReadyButton;

        // ���������҂��𑗐M
        await Send(readyData, 2);
        
        // �t���O��؂�ւ�
        isReadyButton = !isReadyButton;

        if (listenerList.Count < RequiredNum)
        {// �ڑ��l���������Ă��Ȃ��ꍇ
            // �\���E��\���؂�ւ�
            buttonObjList[0].SetActive(false);
            buttonObjList[1].SetActive(false);
            buttonObjList[2].SetActive(true);
        }
        else
        {
            // �\���E��\���؂�ւ�
            buttonObjList[0].SetActive(isReadyButton);
            buttonObjList[1].SetActive(!isReadyButton);
        }
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
            if (isDisconnect == true)
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
                    case 0: // ���g�̃v���C���[ID���擾����

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        PlayerIdData receiveData = JsonConvert.DeserializeObject<PlayerIdData>(jsonString);

                        Debug.Log("�V������M����ID : " + receiveData.id);

                        playerID = receiveData.id;   // ���

                        break;
                    case 1: // �C�x���gID���P�̏������s

                        Debug.Log("�ڑ����̃v���C���[������M����");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        listenerList = JsonConvert.DeserializeObject<List<ListenerData>>(jsonString);

                        // �v���C���[�̖��O�𔽉f
                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (listenerList.Count > i)
                            {// �C���f�b�N�X���͈͓��̏ꍇ
                                // Text���X�V����
                                clientTextList[i].GetComponent<Text>().text = listenerList[i].name;

                                // Player���f����\������
                                modelList[i].SetActive(true);
                            }
                            else
                            {// ����������
                                // Text���X�V����
                                clientTextList[i].GetComponent<Text>().text = "";

                                // Player���f�����\������
                                modelList[i].SetActive(false);
                            }
                        }

                        // �ڑ��l�����S�l�ȏ�̏ꍇ
                        if (listenerList.Count >= RequiredNum)
                        {
                            // �\���E��\���؂�ւ�
                            buttonObjList[0].SetActive(isReadyButton);
                            buttonObjList[1].SetActive(!isReadyButton);
                            buttonObjList[2].SetActive(false);
                        }
                        else
                        {// �l���������Ă��Ȃ�
                            if (isReadyButton == false)
                            {// �����������Ă���ꍇ
                                // �����������L�����Z��������
                                ReadyButton();
                            }
                            else
                            {
                                // �\���E��\���؂�ւ�
                                buttonObjList[0].SetActive(false);
                                buttonObjList[1].SetActive(false);
                                buttonObjList[2].SetActive(true);
                            }
                        }

                        break;
                    case 2: // �C�x���gID���Q�̏������s

                        Debug.Log("��������|�����҂��ʒm����M");

                        List<ReadyData> readyDataList = JsonConvert.DeserializeObject<List<ReadyData>>(jsonString);

                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (readyDataList.Count > i)
                            {// �C���f�b�N�X���͈͓��̏ꍇ
                                if (readyDataList[i].isReady == true)
                                {// �^
                                    readyTextList[i].SetActive(true);
                                }
                                else
                                {// �U
                                    readyTextList[i].SetActive(false);
                                }
                            }
                            else
                            {// �͈͊O�̏ꍇ
                                // �U
                                readyTextList[i].SetActive(false);
                            }
                        }

                        break;
                    case 3: // ���g�̖�E�Ɛ�s�̃v���C���[ID���擾 & �v���C���[�l�����擾

                        // �v���C���[�l�����擾����
                        playerNum = listenerList.Count;

                        // �}�l�[�W���[������������
                        playerManager = null;
                        blockManager = null;

                        Debug.Log("��E�Ɛ�s�̃v���C���[ID���擾");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        JobAndTurnData data = JsonConvert.DeserializeObject<JobAndTurnData>(jsonString);

                        // �������
                        isInsider = data.isInsider;
                        advancePlayerID = data.advancePlayerID;

                        Debug.Log("���ʎҁF" + data.isInsider);
                        Debug.Log("��s�ƂȂ�v���C���[ID�F" + data.advancePlayerID);

                        // �t�F�[�h���V�[���J��
                        Initiate.DoneFading();
                        SceneManager.LoadScene("JobScene_copy");

                        break;
                    case 4: // �`���b�g
                        break;
                    case 5: // �ړ�

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        MoveData moveData = JsonConvert.DeserializeObject<MoveData>(jsonString);

                        // �ړI�n�̍��W���擾
                        Vector3 targetPos = new Vector3(moveData.targetPosX, moveData.targetPosY, moveData.targetPosZ);

                        Debug.Log("[" + moveData.playerID + "]" + " : �ړ�");

                        if (playerManager == null)
                        {// �܂��擾���Ă��Ȃ��ꍇ
                            // �擾����
                            playerManager = GameObject.Find("player-List");
                        }

                        if (playerID != moveData.playerID)
                        {// ��M�����̂��������g�ł͂Ȃ��ꍇ
                            // �ړ�����
                            GameObject movePlayer = playerManager.GetComponent<PlayerManager>().players[moveData.playerID];
                            movePlayer.GetComponent<OtherPlayer>().MoveAgent(targetPos);
                        }

                        break;
                    case 6: // ���߂�

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_FillData fillData = JsonConvert.DeserializeObject<Action_FillData>(jsonString);

                        Debug.Log("[" + fillData.playerID + "]" + " : ���߂�");

                        if (blockManager == null)
                        {// �܂��擾���Ă��Ȃ��ꍇ
                            // �擾����
                            blockManager = GameObject.Find("BlockList");
                        }

                        //if (playerID != fillData.playerID)
                        //{// ��M�����̂��������g�ł͂Ȃ��ꍇ
                        // ���𖄂߂鏈��
                        blockManager.GetComponent<BlockManager>().FillObject(fillData.objeID);
                        //}

                        advancePlayerID++;
                        if(advancePlayerID >= listenerList.Count)
                        {
                            advancePlayerID = 0;
                        }
                        PlayerNamber.Instance.UpdateText(advancePlayerID);

                        break;
                    case 7: // �؂�J��

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_MiningData mineData = JsonConvert.DeserializeObject<Action_MiningData>(jsonString);

                        Debug.Log("[" + mineData.playerID + "]" + " : [" + mineData.prefabID + "]�؂�J��");

                        if (blockManager == null)
                        {// �܂��擾���Ă��Ȃ��ꍇ
                            // �擾����
                            blockManager = GameObject.Find("BlockList");
                        }

                        //if (playerID != mineData.playerID)
                        //{// ��M�����̂��������g�ł͂Ȃ��ꍇ
                        // �؂�J������
                        blockManager.GetComponent<BlockManager>().MineObject(mineData.objeID, mineData.prefabID, mineData.rotY);
                        //}

                        advancePlayerID++;
                        if (advancePlayerID >= listenerList.Count)
                        {
                            advancePlayerID = 0;
                        }
                        PlayerNamber.Instance.UpdateText(advancePlayerID);

                        break;
                    case 8: // �₷��(�X�^�~�i��)

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_NothingData restData = JsonConvert.DeserializeObject<Action_NothingData>(jsonString);

                        Debug.Log("[" + restData.playerID + "]���x�񂾁�(�񕜗ʁF" + restData.addStamina + ") ���v�F" + restData.totalStamina);

                        advancePlayerID++;
                        if(advancePlayerID >= listenerList.Count)
                        {
                            advancePlayerID = 0;
                        }
                        PlayerNamber.Instance.UpdateText(advancePlayerID);

                        break;
                    case 9: // �N�̃^�[����
                        break;
                }

            }, null);
        }

        Instance = null;

        // �ڑ���ؒf
        tcpClient.Close();

        // �t�F�[�h���V�[���J��
        Initiate.DoneFading();
        SceneManager.LoadScene("TitleKawaguchi");
    }

    /// <summary>
    /// �ڑ����� && �X���b�h�N��
    /// </summary>
    private async void StartConnect()
    {
        // ���g�̏��i�[�p
        ListenerData listener = new ListenerData(); // �C���X�^���X��
        listener.name = clientName;  // ���g�̖��O����

        try
        {
            //**********************************
            //  �ڑ��̗v�� & �ʐM�̊m��
            //**********************************

            // ����M�̃^�C���A�E�g��ݒ�(msec)
            tcpClient.SendTimeout = 1000;       // ���M
            tcpClient.ReceiveTimeout = 1000;    // ��M

            // �T�[�o�[�֐ڑ��v��    (IP:"20.249.92.21"  "127.0.0.1")
            await tcpClient.ConnectAsync("127.0.0.1", 20000);
            Debug.Log("***�T�[�o�[�ƒʐM�m��***");

            //**********************************
            //  �v���C���[ID����M����
            //**********************************

            // NetworkStream���g�p
            stream = tcpClient.GetStream();

            // ��M�ҋ@����
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // ��M����

            // ��M�f�[�^����JSON����������o��
            byte[] bufferJson = receiveBuffer.Skip(1).ToArray();    // �P�o�C�g�ڂ��X�L�b�v
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);  // ��M�������̂�byte����string�ɕϊ�

            // JSON�f�V���A���C�Y�Ŏ擾����
            PlayerIdData receiveData = JsonConvert.DeserializeObject<PlayerIdData>(jsonString);

            playerID = receiveData.id;   // ���

            Debug.Log("��M�����v���C���[ID�F" + playerID);

            //**********************************
            //  ���g�̏��𑗐M����
            //**********************************

            // �v���C���[ID����
            listener.id = playerID;

            // ���M����
            await Send(listener, 1);

            //**********************************
            //  ���������f�[�^�𑗐M����
            //**********************************

            // ����M����
            ReadyButton();

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
    public async Task Send(object data, /*object arg, */int eventID)
    {
        //// ��������N���C�A���g���L���X�g�Ŏ擾����
        //TcpClient tcpClientList = (TcpClient)arg;

        // JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(data);  // �N���X�^�̕ϐ����w��

        // Json�������byte�z��ɕϊ�
        byte[] sendData = Encoding.UTF8.GetBytes(json);

        // ���M�f�[�^�̐擪�ɃC�x���gID��ǉ�����
        sendData = sendData.Prepend((byte)eventID).ToArray();    // ���K��ID���m�F���邱��!!!!!!!!!!

        // ���M�f�[�^�̔z�񐔂��Œ�ɕύX
        Array.Resize(ref sendData, 1024);   // �T�C�Y���Œ肵�Ĉ���ɑ���M�ł���悤�ɂ���

        // ���M����
        NetworkStream stream = tcpClient.GetStream();
        await stream.WriteAsync(sendData, 0, sendData.Length);  // ���M

        Debug.Log("OKOKOK");
    }

    /// <summary>
    /// �ʂ̂��̃X�N���v�g����f�[�^�𑗐M����
    /// </summary>
    /// <param name="data"></param>
    /// <param name="eventID"></param>
    public async void SendData(object data, int eventID)
    {
        await Send(data, eventID);

        Debug.Log("OK");
    }

    /// <summary>
    /// exe���I�������Ƃ��̏���
    /// </summary>
    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            // �ڑ���ؒf
            tcpClient.Close();
        }

        Instance = null;
    }
}
