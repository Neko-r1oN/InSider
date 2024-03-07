using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandBy : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    [SerializeField] Text StandText;
    [SerializeField] GameObject OKbutton;
    [SerializeField] GameObject Cancelbutton;
    [SerializeField] GameObject OKIcon;

    [SerializeField] Fade fade;

    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    private void Start()
    {

        fade.FadeOut(1f);

        audio = GetComponent<AudioSource>();
        OKIcon.SetActive(false);
        OKbutton.SetActive(true);
        Cancelbutton.SetActive(false);

    }
    private void Update()
    {
       

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {

            audio.PlayOneShot(ClickSound);

        }

       
    }
    public void OKButton()
    {
        OKIcon.SetActive(true);
        OKbutton.SetActive(false);
        Cancelbutton.SetActive(true);
    }
    public void CancelButton()
    {
        OKIcon.SetActive(false);
        Cancelbutton.SetActive(false);
        OKbutton.SetActive(true);
    }
}
