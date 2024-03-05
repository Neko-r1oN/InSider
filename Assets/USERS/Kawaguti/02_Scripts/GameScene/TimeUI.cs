using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���
    public int doubtNum = 0;
    Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {

        timeSlider = GetComponent<Slider>();
        float maxHp = 30;
        float nowHp = 30f - 5 * doubtNum;


        //�X���C�_�[�̍ő�l�̐ݒ�
        timeSlider.maxValue = maxHp;

        //�X���C�_�[�̌��ݒl�̐ݒ�
        timeSlider.value = nowHp;


        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 1.00f;
        _timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

        if (_timeElapsed >= _repeatSpan)
        {//���Ԍo�߂ŃX�^�~�i����
         //�X���C�_�[�̌��ݒl�̐ݒ�
            timeSlider.value -= 1;
            _timeElapsed = 0;   //�o�ߎ��Ԃ����Z�b�g����
        }
    }
}
