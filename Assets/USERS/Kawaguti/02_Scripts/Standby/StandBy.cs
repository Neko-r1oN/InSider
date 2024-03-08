using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandBy : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

  
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
