using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera1;
    [SerializeField]
    private GameObject Camera2;

    AudioSource audio;
    //[SerializeField] AudioClip ClickSound;


    [SerializeField] AudioClip WinBGM;
    [SerializeField] AudioClip LoseBGM;

    public AudioSource Default;//AudioSource�^�̕ϐ�A_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
    public AudioSource Music1;//AudioSource�^�̕ϐ�A_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
  

    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���

    bool Once;

    // Update�������J�n
    public bool isStart { get; set; } = false;

    // �󔠂ƃ~�~�b�N�̃I�u�W�F�N�g
    [SerializeField] GameObject Chest;
    [SerializeField] GameObject Mimic;

    // ���s�e�L�X�g
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;

    // �v���C���[UI�̃��X�g
    [SerializeField] List<GameObject> playerUIList;

    // �v���C���[�̖��O���X�g
    [SerializeField] List<Text> playerNameList;

    // �g�[�^���X�R�A�̃��X�g
    [SerializeField] List<Text> totarScoreList;

    // ��������X�R�A�̃��X�g
    [SerializeField] List<Text> allieScoreList;

    // ��E�̃��S�}�[�NUI�̃��X�g
    [SerializeField] List<GameObject> insiderLogoMarkUI;
    [SerializeField] List<GameObject> excavatorLogoMarkUI;

    // �C���T�C�_�[�̃I�u�W�F�N�g�̃��X�g
    List<GameObject>[] insiderListParent = new List<GameObject>[3];
    [SerializeField] List<GameObject> insiderList1;
    [SerializeField] List<GameObject> insiderList2;
    [SerializeField] List<GameObject> insiderList3;

    // �󔠂��J����v���C���[�̃��X�g
    [SerializeField] List<GameObject> openPlayerList;

    // �~�~�b�N���ǂ����̔���
    public bool isMimic { get; set; }

    // �V���O���g���p
    public static OpenManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        Default.Play();
        audio = GetComponent<AudioSource>();

        Music1 = GetComponent<AudioSource>();

        Camera1.SetActive(true);
        Camera2.SetActive(true);

        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

        isMimic = true;
        Once = false;

        // �z��Ɋi�[����
        insiderListParent[0] = insiderList1;
        insiderListParent[1] = insiderList2;
        insiderListParent[2] = insiderList3;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart == false)
        {
            return;
        }

        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����


        if (_timeElapsed  >= _repeatSpan)
        {//���Ԍo�߂ŃJ�����ύX
           Camera1.SetActive(false);
            //Camera1.SetActive(!Camera1.activeSelf);
           
        }
        if (_timeElapsed  >= _repeatSpan + 1.0f)
        {
            Camera2.SetActive(false);
            //Camera2.SetActive(!Camera2.activeSelf);
        }

        if (_timeElapsed  >= _repeatSpan + 2.5f  && !Once)
        {
            if(!isMimic)
            {
                Default.Stop();
                Music1.PlayOneShot(WinBGM);
            }
            else if(isMimic)
            {
                Default.Stop();
                Music1.PlayOneShot(LoseBGM);
            }
            Once = true;
        }
    }

    /// <summary>
    /// Insider�̃I�u�W�F�N�g��ݒ� & �󔠂��J����v���C���[�ݒ� & �~�~�b�N���ǂ���
    /// </summary>
    public void SetPlayerAndMimic(List<int> insiderID, int openPlayerID,bool ismimic,List<int> totalScore,List<int> allieScore)
    {
        for (int i = 0; i < insiderID.Count; i++)
        {
            List<GameObject> targetList = insiderListParent[i]; // ���X�g�����o��

            // �\���E��\��
            targetList[insiderID[i]].SetActive(true);           // �v���C���[
            insiderLogoMarkUI[insiderID[i]].SetActive(true);    // Insider���S�}�[�N
            excavatorLogoMarkUI[insiderID[i]].SetActive(false); // �̌@�҂̃��S�}�[�N
        }

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i >= ClientManager.Instance.playerNameList.Count)
            {// ���݂��Ȃ��v���C���[UI���폜����
                Destroy(playerUIList[i]);
            }
            else
            {// �v���C���[�����݂���ꍇ
                playerNameList[i].text = ClientManager.Instance.playerNameList[i];  // ���O�X�V
                totarScoreList[i].text = "" + totalScore[i];   // ���v�X�R�A�X�V

                if (allieScore[i] > 0)
                {// �{�̏ꍇ
                    allieScoreList[i].text = "(+" + allieScore[i] + ")";   // ���Z����X�R�A����������
                }
                else
                {// -�̏ꍇ
                    allieScoreList[i].text = "(" + allieScore[i] + ")";    // ���Z����X�R�A����������
                }
            }
        }

        // �󔠂��J����v���C���[��\������
        openPlayerList[openPlayerID].SetActive(true);

        // �������
        isMimic = ismimic;

        // �I�u�W�F�N�g��\������
        if (ismimic == true)
        {// �~�~�b�N�̏ꍇ
            Mimic.SetActive(true);

            // �n�Y��
            loseText.SetActive(true);
        }
        else
        {// �󔠂̏ꍇ
            Chest.SetActive(true);

            // ������
            winText.SetActive(true);
        }

        // Update�������J�n
        isStart = true;
    }
}
