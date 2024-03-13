using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���
    //public int doubtNum = 0;
    Slider timeSlider;

    bool isMyTurn;

    /// <summary>
    /// ���̐�������
    /// </summary>
    public float nowTime { get; set; }

    // �V���O���g���p
    public static TimeUI Instance;

    const float timeMax = 20f;

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
        isMyTurn = false;
        timeSlider = GetComponent<Slider>();
        float maxTime = 0;

        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            if(ClientManager.Instance.playerID == ClientManager.Instance.turnPlayerID)
            {// �����̃^�[���̏ꍇ
                maxTime = timeMax;
                nowTime = maxTime;
            }
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            maxTime = 100000000f;
            nowTime = 100000000f;
        }

        //�X���C�_�[�̍ő�l�̐ݒ�
        timeSlider.maxValue = maxTime;

        //�X���C�_�[�̌��ݒl�̐ݒ�
        timeSlider.value = nowTime;

        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 1.00f;
        _timeElapsed = 0;
    }

    /// <summary>
    /// ���Ԑ����𐶐�
    /// </summary>
    /// <param name="doubtNum"></param>
    public void GenerateTimer(int doubtNum)
    {
        isMyTurn = true;

        float maxTime = timeMax;
        nowTime = 20f - 3 * doubtNum;

        //�X���C�_�[�̍ő�l�̐ݒ�
        timeSlider.maxValue = maxTime;

        //�X���C�_�[�̌��ݒl�̐ݒ�
        timeSlider.value = nowTime;
    }

    /// <summary>
    /// �^�C�}�[��0�ɂ���
    /// </summary>
    public void FinishTimer()
    {
        timeSlider.maxValue = 0;
        timeSlider.value = 0;
        nowTime = 0;
    }

    // Update is called once per frame
    async Task Update()
    {
        if (EditorManager.Instance.useServer)
        {
            if (ClientManager.Instance.isGetNotice == false)
            {// �Q�[���J�n�ʒm����M���Ă��Ȃ��ꍇ
                return;
            }
        }

        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

        if (_timeElapsed >= _repeatSpan)
        {//���Ԍo�߂Ŏ��Ԍ���
            //�X���C�_�[�̌��ݒl�̐ݒ�
            timeSlider.value -= 1;
            nowTime = timeSlider.value;
            _timeElapsed = 0;   //�o�ߎ��Ԃ����Z�b�g����

            if(nowTime <= 0)
            {// �������Ԃ�0�ȉ��̏ꍇ

                if (ClientManager.Instance.playerID == ClientManager.Instance.turnPlayerID)
                {// ���ݍs���ł���v���C���[��ID���������g�̏ꍇ
                    UpdateTurnsData updateTurnsData = new UpdateTurnsData();

                    // �^�[�����X�V���邽�߂ɑ��M����
                    await ClientManager.Instance.Send(updateTurnsData, 10);
                }

                // �U
                isMyTurn = false;
            }
        }
    }
}
