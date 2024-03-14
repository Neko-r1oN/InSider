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

public class JobSceneManager : MonoBehaviour
{
    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���

    private bool InSiderJodge = true;  //���ʎҔ���(����m�F�p)

    //�e�L�X�g
    [SerializeField] GameObject InSider;
    [SerializeField] GameObject Excavator;
    [SerializeField] Text YourText;
    [SerializeField] Text InSiderText;
    [SerializeField] Text ExcavatorText;

    //���߂̐F
    [SerializeField] Color32 startColor = new Color32(255, 255, 255, 0);

    //�v���C���[���f���̃��X�g
    [SerializeField] List<GameObject> playerModelList = new List<GameObject>();

    // ��E
    static public string job;

    // ��s�̃v���C���[ID
    static public int advancePlayerID;

    private void Start()
    {
        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 0.5f;  
        _timeElapsed = 0;

        // ��E����
        JobJadge();

        // ����̃v���C���[���f����\������
        playerModelList[ClientManager.Instance.playerID].SetActive(true);    // �X�N���v�g����ID���擾

        //�e�L�X�g�J���[�𓧖��ɂ���
        YourText.color = startColor;
        InSiderText.color = startColor;
        ExcavatorText.color = startColor;

        Invoke("SceneChange", 3.0f);
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

        //�o�ߎ��Ԃ��J��Ԃ��Ԋu���o�߂�����
        if (_timeElapsed >= _repeatSpan)
        {//���Ԍo�߂Ńe�L�X�g�\��
            YourText.color = Color.Lerp(YourText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }    
        if (_timeElapsed >= _repeatSpan+1.0f)
        {//���Ԍo�߂Ńe�L�X�g�\��(��E)
            InSiderText.color = Color.Lerp(InSiderText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
            ExcavatorText.color = Color.Lerp(ExcavatorText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }
    }

    //���ʎҔ���֐�(�T�[�o�[��������P��)
    private void JobJadge()
    {
        if (ClientManager.Instance.isInsider == false)
        {// ���@�Ƃ̏ꍇ
            InSider.SetActive(false);
            Excavator.SetActive(true);
        }
        else
        {// �����҂̏ꍇ
            InSider.SetActive(true);
            Excavator.SetActive(false);
        }
    }

    //�V�[���؂�ւ�
    public void SceneChange()
    {
        // �t�F�[�h���V�[���J��
        Initiate.DoneFading();

        Debug.Log("���݂̃��E���h���F" + ClientManager.Instance.roundNum);

        if (ClientManager.Instance.roundNum == 1)
        {
            Initiate.Fade("GameStage1", Color.black, 1f);
        }
        else if (ClientManager.Instance.roundNum == 2)
        {
            Initiate.Fade("GameStage2", Color.black, 1f);
        }
        else if (ClientManager.Instance.roundNum == 3)
        {
            Initiate.Fade("GameStage3", Color.black, 1f);
        }
    }
}
