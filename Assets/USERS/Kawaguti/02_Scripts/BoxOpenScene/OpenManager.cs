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
    bool Mimic;

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

        Mimic = true;
        Once = false;
    }

    // Update is called once per frame
    void Update()
    {

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
            if(!Mimic)
            {
                Default.Stop();
                Music1.PlayOneShot(WinBGM);
            }
            else if(Mimic)
            {
                Default.Stop();
                Music1.PlayOneShot(LoseBGM);
            }
            Once = true;
        }


    }
   
}
