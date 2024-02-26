using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandBy : MonoBehaviour
{
    private float _repeatSpan;    //ŒJ‚è•Ô‚·ŠÔŠu
    private float _timeElapsed;   //Œo‰ßŽžŠÔ

    [SerializeField] Text StandText;
    [SerializeField] GameObject OKbutton;
    [SerializeField] GameObject Cancelbutton;
    [SerializeField] GameObject OKIcon;
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    private void Start()
    {
      
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
