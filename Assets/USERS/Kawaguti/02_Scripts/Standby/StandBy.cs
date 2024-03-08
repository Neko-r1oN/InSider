using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandBy : MonoBehaviour
{
    private float _repeatSpan;    //ŒJ‚è•Ô‚·ŠÔŠu
    private float _timeElapsed;   //Œo‰ßŽžŠÔ

  
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    private void Start()
    {

        audio = GetComponent<AudioSource>();

    }
    private void Update()
    {
       

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {

            audio.PlayOneShot(ClickSound);

        }

       
    }
  
}
