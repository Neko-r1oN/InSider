using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> roadUIList;

    // �c��^�[���\���̃e�L�X�g
    [SerializeField] GameObject remainingTurnsText;

    // ���݂̃��E���h����\��
    [SerializeField] GameObject roundCounterText;

    // "�����̃^�[��"�̃e�L�X�g
    [SerializeField] GameObject turnText;

    // �v���C���[��UI
    [SerializeField] List<GameObject> playerUIList;

    // �v���C���[�̖��O
    [SerializeField] List<GameObject> playerName;
    [SerializeField] List<GameObject> scorePlayerName;  // �X�R�A�\���̕��̃v���C���[Name

    // �X�R�A�\���̕��̃v���C���[UI
    [SerializeField] List<GameObject> scorePlayerUIList;

    // �X�R�A�̃e�L�X�g
    [SerializeField] List<Text> scoreText;

    // �v���C���[���r���ޏo�����Ƃ���UI
    [SerializeField] List<GameObject> outImageUI;

    // �_�E�gUI�̐e
    [SerializeField] List<GameObject> doubtImageParent;

    // �_�E�g��UI
    GameObject[,] doubtImageUiList = new GameObject[6,6];

    // �_�E�g�̃{�^���̃��X�g
    [SerializeField] List<GameObject> doubtButtonList;

    // �_�E�g�̃{�^���p�̓r���ޏo�����Ƃ���UI
    [SerializeField] List<GameObject> outImageList_DoubtUI;

    // �g�p�s�ɂ���_�E�g�̃{�^���̃C���f�b�N�X�ԍ�
    public List<int> disabledIndexNumList;

    private Quaternion _initialRotation; // ������]

    // �A���I�����ł��Ȃ��悤�O��̑I���������l��ۑ�
    public int selectRoadNum;

    // �����擾
    RoadManager roadManager;
    GameObject player;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����擾
        road = GameObject.Find("RoadUI");

        // new����
        disabledIndexNumList = new List<int>();

        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }
        
        // ���[�h�}�l�[�W���[�̏����i�[
        GameObject roadManagerObject = GameObject.Find("RoadManager");
        roadManager = roadManagerObject.GetComponent<RoadManager>();

        // ��]�̏����l���i�[
        _initialRotation = gameObject.transform.rotation;

        // RoadUI���\���ɂ���
        road.SetActive(false);

        //---------------------
        // �_�E�gUI���i�[����
        //---------------------

        for (int i = 0; i < 6; i++)
        {
            // �q�I�u�W�F�N�g�̐����擾
            int childCount = doubtImageParent[i].transform.childCount;

            // �q�I�u�W�F�N�g�����Ɏ擾����
            for (int n = 0; n < childCount; n++)
            {
                // �q�I�u�W�F�N�g���擾����
                Transform childTransform = doubtImageParent[i].transform.GetChild(n);
                GameObject childObject = childTransform.gameObject;

                // �i�[����
                doubtImageUiList[i, n] = childObject;

                // ��A�N�e�B�u�ɂ���
                childObject.SetActive(false);
            }
        }

        if (EditorManager.Instance.useServer == true)
        {
            //-------------------------------------------
            // ���݂̃��E���h���Ǝc��̃^�[�������X�V
            //-------------------------------------------
            roundCounterText.GetComponent<Text>().text = "" + ClientManager.Instance.roundNum;
            remainingTurnsText.GetComponent<Text>().text = "" + ClientManager.Instance.turnMaxNum;

            //-----------------------------
            // �X�R�A�̃e�L�X�g���X�V����
            //-----------------------------
            for (int i = 0; i < ClientManager.Instance.scoreList.Count; i++)
            {
                // �e�X�R�A�̃e�L�X�g���e���X�V����
                UdScoreText(i, ClientManager.Instance.scoreList[i]);
            }

            //------------------------------
            // �v���C���[�̖��O���X�V
            //------------------------------
            UdPlayerName(ClientManager.Instance.playerNameList);

            //-----------------------------
            // ��s�̃v���C���[��\������
            //-----------------------------
            int indexNum = ClientManager.Instance.turnPlayerID;
            UdTurnPlayerUI(ClientManager.Instance.playerNameList[indexNum], indexNum);

            //-----------------------------
            // �v���C���[UI�𓮂���
            //-----------------------------
            playerUIList[ClientManager.Instance.turnPlayerID].GetComponent<MovePlayerUI>().MoveOrReturn(true);
        }

        selectRoadNum = -1;
    }

    /// <summary>
    /// �O��I�����ꂽ��UI�̔�\������
    /// </summary>
    /// <param name="selectNum"></param>
    public void ShowRoad(int selectNum)
    {
        // RoadUI��\������
        road.SetActive(true);

        // �����ŗ����l��0�ȏ�Ȃ�
        if (selectNum >= 0)
        {
            // �O��I�񂾓�UI���\���ɂ���
            roadUIList[selectNum].SetActive(false);
        }
    }

    /// <summary>
    /// �O��I��������UI�̕\������
    /// </summary>
    /// <param name="selectNum"></param>
    public void HideRoad(int selectNum)
    {
        // �����ŗ����l��0�ȏ�Ȃ�
        if(selectNum >= 0)
        {
            // ��\���ɂ��Ă�����UI��\��
            roadUIList[selectNum].SetActive(true);
        }

        // ��UI���\��
        road.SetActive(false);

        // �v���C���[���[�h��MOVE�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    /// <summary>
    /// ��UI��true�Efalse����Ԃ�����
    /// </summary>
    /// <returns></returns>
    public bool ActiveRoad()
    {
        // true�Efalse��Ԃ�
        return road.activeSelf;
    }

    /// <summary>
    /// ��UI�̉�]����
    /// </summary>
    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// ���X�g�̒��g���J�E���g����

            // ���X�g�̑S�Ă���]����
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }

    /// <summary>
    /// ��UI�̉�]�����ɖ߂�����
    /// </summary>
    public void ResetRoadUI()
    {
        for (int i = 0; i < roadUIList.Count; i++)
        {// ���X�g�̒��g���J�E���g����

            // ���X�g�̑S�Ă���]�����ɖ߂�
            roadUIList[i].transform.rotation = _initialRotation;
        }
    }

    /// <summary>
    /// �v���C���[�̖��O���X�V && ���݂��Ȃ�����UI���폜
    /// </summary>
    private void UdPlayerName(List<string> nameList)
    {
        for (int i = 0; i < playerName.Count; i++)
        {
            if (i >= ClientManager.Instance.playerNameList.Count)
            {// ���݂��Ȃ��ꍇ

                // �j������
                Destroy(playerUIList[i]);
                Destroy(scorePlayerUIList[i]);
                Destroy(doubtButtonList[i]);
                Destroy(outImageList_DoubtUI[i]);

                Debug.Log("�j������" + i);

                continue;
            }

            // ��ɕ\���������̖��OUI
            playerName[i].GetComponent<Text>().text = nameList[i];

            // �X�R�A�\���̕��̖��OUI
            scorePlayerName[i].GetComponent<Text>().text = nameList[i];
        }

        Destroy(doubtButtonList[ClientManager.Instance.playerID]);
    }

    /// <summary>
    /// �c��^�[���e�L�X�g�X�V����
    /// </summary>
    /// <param name="turnNum"></param>
    public void UdRemainingTurns(int turnNum)
    {
        remainingTurnsText.GetComponent<Text>().text = "" + turnNum;
    }

    /// <summary>
    /// �X�R�A�̃e�L�X�g���X�V����
    /// </summary>
    /// <param name="indexNum"></param>
    /// <param name="score"></param>
    public void UdScoreText(int indexNum,int score)
    {
        scoreText[indexNum].text = "" + score;
    }

    /// <summary>
    /// �v���C���[UI�̈ʒu��߂�
    /// </summary>
    /// <param name="indexNum"></param>
    public void ReturnPlayerUI(int indexNum)
    {
        //���� [���W�𐳋K�����邽��]
        Canvas.ForceUpdateCanvases();

        // ���̈ʒu�֖߂�
        Vector3 pos = playerUIList[indexNum].transform.localPosition;
        playerUIList[indexNum].transform.localPosition = new Vector3(-34.3f, pos.y, pos.z);
    }

    /// <summary>
    /// �^�[���e�L�X�g�̍X�V(�{�v���C���[UI�𓮂���)
    /// </summary>
    /// <param name="indexNum"></param>
    public void UdTurnPlayerUI(string name, int indexNum)
    {
        // �A�N�e�B�u��
        turnText.SetActive(true);

        // �e�L�X�g�X�V
        Transform textTransform = turnText.transform.GetChild(0);   // �q�I�u�W�F�N�g���擾����
        GameObject textObject = textTransform.gameObject;
        textObject.GetComponent<Text>().text = name + "�̃^�[��";

        // �A�j���[�V����(�R���[�`��)
        turnText.GetComponent<TurnUI>().StartCoroutine("PanelAnim");

        // �v���C���[UI�𓮂���
        playerUIList[indexNum].GetComponent<MovePlayerUI>().MoveOrReturn(true);     // ������

        // �O�񓮂������v���C���[UI�����̈ʒu�֖߂�
        if (indexNum == 0)
        {
            // ���̈ʒu�֖߂�
            playerUIList[ClientManager.Instance.playerNameList.Count - 1].GetComponent<MovePlayerUI>().MoveOrReturn(false);
        }
        else
        {
            // ���̈ʒu�֖߂�
            playerUIList[indexNum - 1].GetComponent<MovePlayerUI>().MoveOrReturn(false);
        }
    }

    /// <summary>
    /// �r���ޏo�p��UI��\������
    /// </summary>
    /// <param name="indexNum"></param>
    public void UdOutUI(int indexNum)
    {
        // �A�N�e�B�u��
        outImageUI[indexNum].SetActive(true);
        outImageList_DoubtUI[indexNum].SetActive(true);
    }

    /// <summary>
    /// �_�E�gUI���X�V
    /// </summary>
    /// <param name="targetNum"></param>
    /// <param name="indexNum"></param>
    public void UdDoubt(int targetID,int playerID)
    {
        // �C���f�b�N�X�ԍ��𒲐�
        playerID = (playerID - 1 <= 0) ? 0 : playerID--;    // �摜�̎�ނ͂T��ނ̂��ߒ���

        // �A�N�e�B�u������
        doubtImageUiList[targetID, playerID].SetActive(true);
    }

    /// <summary>
    /// UI���X�g����v�f���폜����
    /// </summary>
    /// <param name="indexNum"></param>
    public void RemoveElement(int indexNum)
    {
        playerUIList.RemoveAt(indexNum);
        playerName.RemoveAt(indexNum);
        outImageUI.RemoveAt(indexNum);
        doubtButtonList.RemoveAt(indexNum);

        // �g�p�ł��Ȃ��C���f�b�N�X�ԍ���ǉ�
        disabledIndexNumList.Add(indexNum);
    }
}
