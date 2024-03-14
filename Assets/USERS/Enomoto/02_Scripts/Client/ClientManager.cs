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

    // �t�F�[�h�p�V���A���C�Y�t�B�[���h
    //[SerializeField] Fade fade;

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

    // UI�}�l�[�W���[
    GameObject uiManager;

    // �C�x���g�}�l�[�W���[
    GameObject eventManager;

    // EnemyManager
    GameObject enemyManager;

    // �K�v�ڑ��l��
    int RequiredNum = 3;

    // �Q�[�����I���������ǂ���
    bool isGameSet;

    // �I�[�f�B�I�\�[�X�n
    AudioSource audio;
    [SerializeField] AudioClip eventAndSubotageSE;
    [SerializeField] AudioClip getScoreSE;
    [SerializeField] AudioClip relaxSE;
    [SerializeField] AudioClip fillSE;
    [SerializeField] AudioClip mineSE;

    // �p�[�e�B�N��
    [SerializeField] GameObject heelingPrefab;

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

    public int PLAYERID;

    /// <summary>
    /// ���X�̃v���C���[ID 
    /// </summary>
    public int originalID { get; set; } // �N�����r���ޏo�����Ƃ��Ƀv���C���[ID������邽��

    /// <summary>
    /// ���g�̖�E�����ʎ҂��ǂ���
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// ���ݍs���\�ȃv���C���[��ID
    /// </summary>
    public int turnPlayerID { get; set; }

    /// <summary>
    /// �ő�^�[����
    /// </summary>
    public int turnMaxNum { get; set; }

    /// <summary>
    /// ���݂̃��E���h��
    /// </summary>
    public int roundNum { get; set; }

    /// <summary>
    /// �v���C���[�̖��O
    /// </summary>
    public List<string> playerNameList { get; set; }

    /// <summary>
    /// ���E���h�J�n���̕\������X�R�A�̃��X�g
    /// </summary>
    public List<int> scoreList { get; set; }

    /// <summary>
    /// �T�[�o�[����ʒm���󂯎�������ǂ���
    /// </summary>
    public bool isGetNotice { get; set; }

    /// <summary>
    /// �j������ʐM���̃e�L�X�g
    /// </summary>
    public GameObject loadingObj { get; set; }

    /// <summary>
    /// ���U���g�V�[���Ŏg�p
    /// </summary>
    public List<int> totalScoreList { get; set; }

    /// <summary>
    /// �Q�[�����[�h
    /// </summary>
    public enum GAMEMODE
    {
        Title,      // �^�C�g��
        Tutorial,   // �`���[�g���A��
        Standby,    // �ҋ@
        Job,        // ��E
        Game,       // �Q�[��
        result      // ���U���g
    }

    /// <summary>
    /// ����M�p��ID
    /// </summary>
    public enum EventID
    {
        PlayerID = 0,         // �v���C���[ID(1P,2P�E�E�E)
        ListenerList,         // ���X�i�[�f�[�^
        ReadyData,            // ��������
        RoundReady,           // ���E���h�J�n�ɕK�v�ȏ��
        RoundEnd,             // ���E���h�I���ʒm
        MoveData,             // �ړ�
        Action_FillData,      // �s���F���߂�
        Action_MiningData,    // �s���F�؂�J��
        Action_NothingData,   // �s���F�������Ȃ�
        DelPlayerID,          // �ؒf�����v���C���[��ID
        UdTurns,              // �^�[�����X�V
        DoubtData,            // �_�E�g�̃f�[�^
        RevisionPos,          // ���W�̏C��
        Start_Game,           // �ڑ����̃N���C�A���g�S�����Q�[���V�[���ɓ˓�������Q�[���X�^�[�g�ʒm�𑗂�
        AllieScore,           // �X�R�A�̉��Z
        Start_RoundReady,     // �ڑ����̃N���C�A���g�S����OpenBox�V�[������J�ڂ���Ƃ��ɒʒm�𑗂�
        MapData,              // �󔠂̒��g�̏������L����ۂɁA�E�\�������ǂ���
        PlayerMiningAnim,     // �̌@�A�j���[�V����������v���C���[��ID

        //+++++++++++++++++++++++++
        //  ��������C�x���g��ID
        //++++++++++++++++++++++++++
        EvendAlert = 100,           // �C�x���g�J�n�ʒm
        RndFallStones,              // �����_���ɋ󂩂�΂��~���Ă���
        Confusion,                  // ������ԂɂȂ�
        SpownEnemys,                // �G���o��
        RiStaminaCn,                // �X�^�~�i�̏���ʂ����炷
        RndSpawnGold,               // �����_���ɃS�[���h���󂩂�~���Ă���
        EvendFinish = 110,          // �C�x���g�I���ʒm

        //++++++++++++++++++++++++++
        //  �T�{�^�[�W����ID
        //++++++++++++++++++++++++++
        Sabotage_Set = 200,         // �T�{�^�[�W���̐���
        Sabotage_Bomb_Cancell,      // ���e�̉���
        Sabotage_Bomb_Explosion,    // ���e�̔���
    }

    /// <summary>
    /// �T�{�^�[�W����ID
    /// </summary>
    public enum SabotageID
    {
        Fill = 0,   // ���𖄂߂�
        Bomb,       // ���e�ݒu
        Trap        // �g���b�v�ݒu
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
        }
        else
        {
            Destroy(gameObject);
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
        audio = GetComponent<AudioSource>();
        roundNum = 0;

        playerID = 0;
        isGameSet = false;

        // �擾����
        clientName = TitleManager.UserName;

        // �񓯊����������s����
        StartConnect();
    }

    private void Update()
    {
        PLAYERID = playerID;
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
        Initiate.Fade("Title", Color.black, 3.0f);

        Debug.Log("�ޏo");

        Destroy(this.gameObject);
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

        // Enemy�̃��X�g
        List<GameObject> enemyObjList = new List<GameObject>();

        // �������g���^��ꂽ��
        int doubtNum = 0;

        while (true)
        {
            // ��M�ҋ@����
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // ��M����

            // �ڑ��ؒf�`�F�b�N
            if (isDisconnect == true || isGameSet == true)
            {
                // �^
                Instance = null;

                // �ڑ���ؒf
                tcpClient.Close();

                Destroy(this.gameObject);

                return;
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
                    case 3: // ���E���h�̊J�n���� [JobScene�ɓ��鎞�Ɏ�M]

                        // �^��ꂽ�񐔂�������
                        doubtNum = 0;

                        isGetNotice = false;

                        // �v���C���[�̖��O���X�g������������
                        playerNameList = new List<string>();

                        // �v���C���[�̖��O���擾����
                        foreach(ListenerData nameData in listenerList)
                        {
                            playerNameList.Add(nameData.name);

                            Debug.Log("�ꏏ�ɂ��v���C���[���F" + nameData.name);
                        }

                        // ���X�̃v���C���[ID���X�V
                        originalID = playerID;

                        // �}�l�[�W���[������������
                        playerManager = null;
                        blockManager = null;
                        uiManager = null;
                        enemyManager = null;
                        eventManager = null;

                        Debug.Log("��E�Ɛ�s�̃v���C���[ID���擾");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        RoundRadyData data = JsonConvert.DeserializeObject<RoundRadyData>(jsonString);

                        // �S�Ẵv���C���[�̃X�R�A���擾����
                        scoreList = data.scoreList;

                        // �������
                        isInsider = data.isInsider;
                        turnPlayerID = data.advancePlayerID;
                        turnMaxNum = data.turnMaxNum;
                        roundNum = data.roundNum;

                        Debug.Log("���ʎҁF" + data.isInsider);
                        Debug.Log("��s�ƂȂ�v���C���[ID�F" + data.advancePlayerID);

                        // �t�F�[�h���V�[���J��
                        Initiate.DoneFading();
                        Initiate.Fade("Job", Color.black, 10f);

                        break;
                    case 4: // ���E���h�I���ʒm

                        Debug.Log("���E���h�I���ʒm����M");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        RoundEndData roundEndData = JsonConvert.DeserializeObject<RoundEndData>(jsonString);

                        totalScoreList = roundEndData.totalScore;

                        if (roundEndData.isTurnEnd == true)
                        {// ���E���h�I���ɂ��ʒm�̏ꍇ

                            // �t�F�[�h���V�[���J��
                            Initiate.DoneFading();
                            Initiate.Fade("RoundResultScene", Color.black, 1f);

                            // �J�ڐ�ɂ���I�u�W�F�N�g�̊֐����ĂԂ��߂̃R���[�`��
                            StartCoroutine(SetRoundResultUI(roundEndData));
                        }
                        else
                        {
                            // �t�F�[�h���V�[���J��
                            Initiate.DoneFading();
                            Initiate.Fade("BoxOpenScene", Color.black, 1f);

                            // �J�ڐ�ɂ���I�u�W�F�N�g�̊֐����ĂԂ��߂̃R���[�`��
                            StartCoroutine(SetPlayerAndMimic(roundEndData));
                        }

                        break;
                    case 5: // �ړ�

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        MoveData moveData = JsonConvert.DeserializeObject<MoveData>(jsonString);

                        // �ړI�n�̍��W���擾
                        Vector3 targetPos = new Vector3(moveData.targetPosX, moveData.targetPosY, moveData.targetPosZ);

                        Debug.Log("[" + moveData.playerID + "]" + " : �ړ�");

                        // �ړ�����
                        GameObject movePlayer = playerManager.GetComponent<PlayerManager>().players[moveData.playerID];
                        movePlayer.GetComponent<Player>().MoveAgent(targetPos);

                        break;
                    case 6: // ���߂�

                        audio.PlayOneShot(fillSE);

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_FillData fillData = JsonConvert.DeserializeObject<Action_FillData>(jsonString);

                        Debug.Log("[" + fillData.playerID + "]" + " : ���߂�");

                        // ���𖄂߂鏈��
                        blockManager.GetComponent<BlockManager>().StartCoroutine(
                            blockManager.GetComponent<BlockManager>().FillObject(fillData.objeID)); 

                        //****************************************************************
                        //  �T�{�^�[�W���őI�𒆂̃p�l�������߂��ď��������̏���
                        //****************************************************************
                        for (int i = 0; i < RoadManager.Instance.selectPanelList.Count; i++)
                        {
                            GameObject selectPanel = RoadManager.Instance.selectPanelList[i];

                            if(selectPanel.GetComponent<RoadPanel>().objeID == fillData.objeID)
                            {
                                // �I�𒆂̃��X�g����폜���I�𐔂����Z
                                RoadManager.Instance.selectPanelList.RemoveAt(i);
                                RoadManager.Instance.selectPanelCount--;
                                if(RoadManager.Instance.selectPanelCount <= 0)
                                {
                                    RoadManager.Instance.selectPanelCount = 0;
                                }

                                // �e�L�X�g���X�V����
                                TextUIManager.Instance.PutNum(RoadManager.Instance.selectPanelCount);
                            }
                        }

                        break;
                    case 7: // �؂�J��

                        audio.PlayOneShot(mineSE);

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_MiningData mineData = JsonConvert.DeserializeObject<Action_MiningData>(jsonString);

                        Debug.Log("[" + mineData.playerID + "]" + " : [" + mineData.prefabID + "]�؂�J��  *"+ mineData.isGetGold+"*");

                        // �؂�J������
                        blockManager.GetComponent<BlockManager>().MineObject(mineData.playerID, mineData.objeID, mineData.prefabID, mineData.rotY,mineData.isGetGold);

                        break;
                    case 8: // �₷��(�X�^�~�i��)

                        audio.PlayOneShot(relaxSE);

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Action_NothingData restData = JsonConvert.DeserializeObject<Action_NothingData>(jsonString);

                        Debug.Log("[" + restData.playerID + "]���x�񂾁�(�񕜗ʁF" + restData.addStamina + ") ���v�F" + restData.totalStamina);

                        GameObject heelPlayer = playerManager.GetComponent<PlayerManager>().players[restData.playerID];

                        Instantiate(heelingPrefab, new Vector3(heelPlayer.transform.position.x, 0.9f, heelPlayer.transform.position.z),Quaternion.identity);

                        break;
                    case 9: // �Q�[�����ɐؒf�����v���C���[��ID����M

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        DelPlayerData delPlayerData = JsonConvert.DeserializeObject<DelPlayerData>(jsonString);

                        // �ڑ����̃v���C���[�����X�V
                        listenerList = delPlayerData.listeners;

                        Debug.Log("�ؒf�����v���C���[ID : " + delPlayerData.playerID);

                        // �v���C���[�̖��O���X�g����v�f���폜
                        playerNameList.RemoveAt(delPlayerData.playerID);

                        // �r���ޏo�������Ƃ���������UI��\������
                        uiManager.GetComponent<UIManager>().UdOutUI(delPlayerData.playerID);

                        // �r���ޏo�����v���C���[UI�̈ʒu�𐳂�
                        uiManager.GetComponent<UIManager>().ReturnPlayerUI(delPlayerData.playerID);

                        // UI�}�l�[�W���[�������X�g����v�f���폜
                        uiManager.GetComponent<UIManager>().RemoveElement(delPlayerData.playerID);

                        // �v���C���[�̃��f�����X�g���擾����
                        List<GameObject> objeList = playerManager.GetComponent<PlayerManager>().players;

                        // �ؒf�����v���C���[�̃��f����j������
                        Destroy(objeList[delPlayerData.playerID]);

                        // �v���C���[���X�g����폜����
                        objeList.RemoveAt(delPlayerData.playerID);

                        // �e�v���C���[�I�u�W�F�N�g��ID���Đݒ肷��
                        for (int i = 0; i < objeList.Count; i++)
                        {
                            // �I�u�W�F�N�gID���X�V����
                            objeList[i].GetComponent<Player>().playerObjID = i;
                        }

                        break;
                    case 10:    // �^�[�����̍X�V�����ɍs���ł���v���C���[ID�̍X�V

                        Debug.Log("�^�[�����X�V���܂��B");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        UpdateTurnsData udTurnsData = JsonConvert.DeserializeObject<UpdateTurnsData>(jsonString);

                        // ���ɍs���ł���v���C���[��ID���X�V����
                        Debug.Log("���ɍs���ł���v���C���[ID�F" + udTurnsData.nextPlayerID);
                        turnPlayerID = udTurnsData.nextPlayerID;
                        uiManager.GetComponent<UIManager>().UdTurnPlayerUI(playerNameList[turnPlayerID], turnPlayerID);   // UI���X�V

                        // �c��^�[�������X�V����
                        uiManager.GetComponent<UIManager>().UdRemainingTurns(udTurnsData.turnNum);

                        // �������Ԃ�0�ɂ���
                        TimeUI.Instance.FinishTimer();

                        if(turnPlayerID == playerID)
                        {// ���g�̃^�[���̏ꍇ
                            TimeUI.Instance.GenerateTimer(doubtNum);    // �������Ԃ�ݒ肷��
                        }

                        break;
                    case 11:    // �_�E�g�̃f�[�^����M

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        DoubtData doubtData = JsonConvert.DeserializeObject<DoubtData>(jsonString);

                        Debug.Log("[����ID]" + doubtData.playerID + "��" + doubtData.targetID + "���^��");

                        if(doubtData.targetID == originalID)
                        {// ���g���^��ꂽ�ꍇ
                            doubtNum++;
                        }

                        // �_�E�gUI���X�V����
                        uiManager.GetComponent<UIManager>().UdDoubt(doubtData.targetID, doubtData.playerID);

                        break;
                    case 12: // �Ώۂ̃v���C���[�I�u�W�F�N�g�̍��W���C������

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        RevisionPosAndDropGoldData revisionPos = JsonConvert.DeserializeObject<RevisionPosAndDropGoldData>(jsonString);

                        // �v���C���[�̃��f�����X�g���擾����
                        List<GameObject> objeList1 = playerManager.GetComponent<PlayerManager>().players;

                        if (revisionPos.isDown == true)
                        {// �G�ɂ����W�C��

                            Debug.Log(revisionPos.targetID + "���_�E������");

                            // �v���C���[�̃_�E������
                            objeList1[revisionPos.targetID].GetComponent<Player>().DownPlayer(revisionPos.goldDropNum);
                        }
                        else
                        {
                            // ���W���擾����
                            Vector3 targetPos1 = new Vector3(revisionPos.targetPosX, revisionPos.targetPosY, revisionPos.targetPosZ);

                            Debug.Log("���W���C������ [�I�u�W�F�N�gID�F" + revisionPos.targetID + "] **���M���̃v���C���[ID�F" + revisionPos.playerID);

                            // ���W���C������
                            objeList1[revisionPos.targetID].GetComponent<Player>().RevisionPos(targetPos1);
                        }

                        break;
                    case 13:    // �Q�[���J�n�ʒm (�ǂ̕󔠂��~�~�b�N�ɂ��邩��M)

                        Debug.Log("���E���h�J�n�ʒm����M");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        RoundStartData roundStartData = JsonConvert.DeserializeObject<RoundStartData>(jsonString);

                        GameObject chestManager1 = GameObject.Find("ChestList");

                        // �~�~�b�N��ݒ肷��
                        chestManager1.GetComponent<MimicManager>().SetMimic(roundStartData.isMimicList);

                        isGetNotice = true;

                        if(loadingObj != null)
                        {
                            Destroy(loadingObj);
                        }

                        // �^�[���e�L�X�g���X�V�i��s�̃v���C���[�j
                        uiManager.GetComponent<UIManager>().UdTurnPlayerUI(playerNameList[turnPlayerID], turnPlayerID);

                        break;
                    case 14: // �X�R�A�̃e�L�X�g���X�V����

                        audio.PlayOneShot(getScoreSE);

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        AllieScoreData allieScoreData = JsonConvert.DeserializeObject<AllieScoreData>(jsonString);

                        // �X�R�A���X�g�̗v�f���X�V����
                        scoreList[allieScoreData.playerID] = allieScoreData.allieScore;

                        // �X�R�A�e�L�X�g���X�V����
                        uiManager.GetComponent<UIManager>().UdScoreText(allieScoreData.originalID, allieScoreData.allieScore);

                        break;
                    case 15:    // ���U���g��ʂɑJ�ڂ���Ƃ�������M����

                        Debug.Log("���U���g�V�[���J��");

                        isGameSet = true;
                        
                        // JSON�f�V���A���C�Y�Ŏ擾����
                        GameEndData gameEndData = JsonConvert.DeserializeObject<GameEndData>(jsonString);
                        scoreList = gameEndData.scoreList;

                        // �t�F�[�h���V�[���J��
                        Initiate.DoneFading();
                        Initiate.Fade("ResultScene", Color.black, 1.0f);

                        break;

                    case 16:    // �󔠂̒��g�̏��

                        Debug.Log("��̒n�}���g�p���ꂽ�I�I");

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonString);

                        GameObject chestManager2 = GameObject.Find("ChestList");
                        chestManager2.GetComponent<MimicManager>().SetImg(mapData.isLie, mapData.playerID);   // ��̒n�}�̓��e�����L����

                        break;
                    case 17:    // �̌@�A�j���[�V����������v���C���[ID

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        PlayerIdData idData = JsonConvert.DeserializeObject<PlayerIdData>(jsonString);

                        Debug.Log("�A�j���[�V�����v���C���[ID�F" + idData.id);

                        // �v���C���[���̌@����A�j���[�V�������Đ�����
                        playerManager.GetComponent<PlayerManager>().players[idData.id].GetComponent<Animator>().SetBool("Mining", true);

                        break;

                    //*****************************
                    //  �C�x���g��Data����M
                    //*****************************

                    case 100:   // �C�x���g�����ʒm

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        EventAlertData eventData = JsonConvert.DeserializeObject<EventAlertData>(jsonString);

                        Debug.Log("�C�x���g���� : " + eventData.eventID);

                        // �C�x���g�p�e�L�X�g���X�V����
                        uiManager.GetComponent<UIManager>().UdEventText(eventData.eventID);

                        audio.PlayOneShot(eventAndSubotageSE);

                        break;

                    case 101:   // �����_������

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Event_RndFallData event_stoneData = JsonConvert.DeserializeObject<Event_RndFallData>(jsonString);

                        Debug.Log("�C�x���g�F����");

                        // ���W��ݒ�
                        Vector3 stonePos = blockManager.GetComponent<BlockManager>().blocks[event_stoneData.panelID].transform.position;
                        stonePos = new Vector3(stonePos.x + event_stoneData.addPosX, 0.5f, stonePos.z + event_stoneData.addPosZ);

                        // ���΃I�u�W�F�N�g�̐�������
                        eventManager.GetComponent<EventManager>().GenerateEventStone(stonePos);

                        break;
                    case 102:   // ����

                        // �p�[�e�B�N���������t���O��True
                        eventManager.GetComponent<EventManager>().SetConfusion();

                        break;
                    case 103:   // �G�o��

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Event_SpawnEnemyData event_enemyData = JsonConvert.DeserializeObject<Event_SpawnEnemyData>(jsonString);

                        Debug.Log("�C�x���g�F�G�o��");

                        // ���W��ݒ肷��
                        Vector3 enemyPos = blockManager.GetComponent<BlockManager>().blocks[event_enemyData.panelID].transform.position;
                        enemyPos = new Vector3(enemyPos.x, 0.55f, enemyPos.z);

                        // �G�𐶐������烊�X�g�ɒǉ�����
                        enemyObjList.Add(eventManager.GetComponent<EventManager>().SpawnEnemy(enemyPos));

                        break;
                    case 104:   // �X�^�~�i�o�t

                        // �p�[�e�B�N���������X�^�~�i����ʂ�}����l��ݒ�
                        eventManager.GetComponent<EventManager>().SetBuff();

                        break;
                    case 105:   // �����_���ɋ����~���Ă���

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Event_RndFallData event_goldData = JsonConvert.DeserializeObject<Event_RndFallData>(jsonString);

                        // ���W��ݒ�
                        Vector3 goldPos = blockManager.GetComponent<BlockManager>().blocks[event_goldData.panelID].transform.position;
                        goldPos = new Vector3(goldPos.x + event_goldData.addPosX, 10f, goldPos.z + event_goldData.addPosZ);

                        // ���̃I�u�W�F�N�g�̐�������
                        eventManager.GetComponent<EventManager>().GenerateEventGold(goldPos);

                        Debug.Log("�C�x���g�F��");

                        break;

                    case 110:   // �C�x���g�I���ʒm

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        EventAlertData eventFinishData = JsonConvert.DeserializeObject<EventAlertData>(jsonString);

                        Debug.Log("�I������C�x���g : " + eventFinishData.eventID);

                        if(eventFinishData.eventID == 103)
                        {// �G�̏ꍇ

                            List<GameObject> oldEnemys = new List<GameObject>();

                            for (int i = 0; i < 2; i++)
                            {
                                oldEnemys.Add(enemyObjList[0]);

                                // �Â��G�̗v�f���폜����
                                enemyObjList.RemoveAt(0);
                            }

                            // �_�ł̃R���[�`�����J�n
                            enemyManager.GetComponent<EnemyManager>().StartCoroutine(
                                enemyManager.GetComponent<EnemyManager>().StartBlink(oldEnemys));
                        }
                        else if(eventFinishData.eventID == 102)
                        {// �����C�x���g�̏ꍇ

                            Debug.Log("������");

                            eventManager.GetComponent<EventManager>().EndConfusion();
                        }
                        else if (eventFinishData.eventID == 104)
                        {// �o�t�C�x���g�̏ꍇ

                            Debug.Log("������");

                            eventManager.GetComponent<EventManager>().EndBuff();
                        }

                        break;

                    //*****************************
                    //  �T�{�^�[�W����Data����M
                    //*****************************
                    case 200:   // �T�{�^�[�W����������

                        audio.PlayOneShot(eventAndSubotageSE);

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Sabotage_SetData setSabotageData = JsonConvert.DeserializeObject<Sabotage_SetData>(jsonString);

                        Debug.Log("�T�{�^�[�W������ : " + setSabotageData.sabotageID);

                        if(setSabotageData.sabotageID == 0)
                        {// ���𖄂߂�
                            for (int i = 0; i < setSabotageData.objID.Count; i++)
                            {
                                // �u���b�N���������
                                blockManager.GetComponent<BlockManager>().FillObject(setSabotageData.objID[i]);
                            }

                            // �N���ɂ���Ďg��ꂽ�T�{�^�[�W�����g�p�֎~�ɂ���
                            Sabotage.Instance.GetComponent<Sabotage>().isFill = true;

                            Debug.Log(Sabotage.Instance.GetComponent<Sabotage>().isFill);
                        }
                        else if (setSabotageData.sabotageID == 1)
                        {// ���e�𐶐�

                            Debug.Log(setSabotageData.objID.Count);

                            for (int i = 0; i < setSabotageData.objID.Count; i++)
                            {
                                Debug.Log(setSabotageData.objID[i] + ":" + setSabotageData.bombID[i]);

                                // �������
                                blockManager.GetComponent<BlockManager>().SetSabotage_Bomb(
                                    setSabotageData.objID[i], setSabotageData.bombID[i]);
                            }

                            // �N���ɂ���Ďg��ꂽ�T�{�^�[�W�����g�p�֎~�ɂ���
                            Sabotage.Instance.GetComponent<Sabotage>().isBomb = true;

                            Debug.Log(Sabotage.Instance.GetComponent<Sabotage>().isBomb);

                        }
                        else if (setSabotageData.sabotageID == 2)
                        {// �g���b�v����
                            blockManager.GetComponent<BlockManager>().SetSabotage_Trap(setSabotageData.objID[0]);

                            // �N���ɂ���Ďg��ꂽ�T�{�^�[�W�����g�p�֎~�ɂ���
                            Sabotage.Instance.GetComponent<Sabotage>().isTrap = true;

                            Debug.Log(Sabotage.Instance.GetComponent<Sabotage>().isTrap);
                        }

                        break;
                    case 201:   // ���e�̉��� [���M�p�̂��ߎ�M���Ȃ�]

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Sabotage_Bomb_CancellData cancellBombData = JsonConvert.DeserializeObject<Sabotage_Bomb_CancellData>(jsonString);

                        Debug.Log("���e���������ꂽ : " + cancellBombData.bombID);

                        // ���e��j������
                        blockManager.GetComponent<BlockManager>().bombs[cancellBombData.bombID].
                        GetComponent<Bomb>().DestroyBomb();

                        break;
                    case 202:   // ���e�𔚔�������

                        // JSON�f�V���A���C�Y�Ŏ擾����
                        Sabotage_Bomb_ExplosionData explosionData = JsonConvert.DeserializeObject<Sabotage_Bomb_ExplosionData>(jsonString);

                        Debug.Log("���e������ [ �c��" + explosionData.restTurnNum + "�^�[�� ]");

                        for (int i = 0; i < blockManager.GetComponent<BlockManager>().bombs.Length; i++)
                        {
                            if(blockManager.GetComponent<BlockManager>().bombs[i] != null)
                            {// ���݂���ꍇ
                                // ����������
                                blockManager.GetComponent<BlockManager>().bombs[i].
                                GetComponent<Bomb>().AtackbombPrefab();
                            }
                        }

                        // �c��^�[�������X�V����
                        UIManager manager = uiManager.GetComponent<UIManager>();
                        manager.StartCoroutine(manager.UdRestTurnTextAnim(explosionData.restTurnNum));

                        break;
                }

            }, null);
        }
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
            await tcpClient.ConnectAsync("20.249.92.21", 20000);
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
    /// �}�l�[�W���[���擾����
    /// </summary>
    public void GetManagers()
    {
        // �擾����
        blockManager = GameObject.Find("BlockList");
        playerManager = GameObject.Find("player-List");
        uiManager = GameObject.Find("UIManager");
        eventManager = GameObject.Find("EventManager");
        enemyManager = GameObject.Find("EnemyManager");
    }

    IEnumerator SetPlayerAndMimic(RoundEndData roundEndData)
    {
        yield return new WaitForSeconds(1.1f);

        // �V�[���ݒ�
        OpenManager.Instance.SetPlayerAndMimic(
            roundEndData.insiderID,
            roundEndData.openPlayerID,
            roundEndData.isMimic,
            roundEndData.totalScore,
            roundEndData.allieScore);
    }

    IEnumerator SetRoundResultUI(RoundEndData roundEndData)
    {
        yield return new WaitForSeconds(1.05f);

        // �V�[���ݒ�
        RoundResultManager.Instance.SetUI(
            roundEndData.totalScore,
            roundEndData.allieScore,
            roundEndData.insiderID);
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
