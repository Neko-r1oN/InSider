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

    public AudioSource Default;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource Music1;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
  

    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

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

        //表示切り替え時間を指定
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

        Mimic = true;
        Once = false;
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //時間をカウントする


        if (_timeElapsed  >= _repeatSpan)
        {//時間経過でカメラ変更
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
