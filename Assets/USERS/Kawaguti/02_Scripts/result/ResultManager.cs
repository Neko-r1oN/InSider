/////////////////////////////////////////////
//
//リザルトシーンスクリプト
//Auther : Kawaguchi Kyousuke
//Date 2024.02/27
//
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    public static bool isResult;

    void Start()
    {
        isResult = false;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            audio.PlayOneShot(ClickSound);
            if (isResult == false)
            {
                
                Invoke("StartResult", 1.5f);
            }
        }
    }
    void StartResult()
    {
        isResult = true;
    }
}
