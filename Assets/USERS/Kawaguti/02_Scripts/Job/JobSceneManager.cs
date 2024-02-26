using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //�v���C���[
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Player3;
    [SerializeField] GameObject Player4;
    [SerializeField] GameObject Player5;
    [SerializeField] GameObject Player6;

    private void Start()
    {
        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 0.5f;  
        _timeElapsed = 0;

        JobJadge();

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
        if (InSiderJodge == false)
        {
            InSider.SetActive(false);
            Excavator.SetActive(true);
        }
        if (InSiderJodge == true)
        {
            InSider.SetActive(true);
            Excavator.SetActive(false);
        }
    }
    //�V�[���؂�ւ�
    public void SceneChange()
    {
        //SceneManager.LoadScene("");
    }
}
