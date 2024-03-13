/////////////////////////////////////////////
//
//���U���g�V�[���X�N���v�g
//Auther : Kawaguchi Kyousuke
//Date 2024.02/27
//
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioClip ClickSound;

    public static bool isResult;

    [SerializeField] Text winnerName;
    [SerializeField] List<GameObject> playerObjList;

    [SerializeField] List<GameObject> playerUIList;
    [SerializeField] List<Text> playerNameList;
    [SerializeField] List<Text> totalScoreList;

    void Start()
    {
        isResult = false;
        audio = GetComponent<AudioSource>();

        int maxScore = 0;
        int indexNum = 0;

        List<int> scoreList = ClientManager.Instance.scoreList;
        List<string> nameList = ClientManager.Instance.playerNameList;

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if(i >= scoreList.Count)
            {// ���݂��Ȃ��v���C���[��UI��j������
                Destroy(playerUIList[i]);
                continue;
            }
            
            if (maxScore < scoreList[i])
            {// �X�R�A����ԍ����v���C���[������
                maxScore = scoreList[i];
                indexNum = i;
            }

            playerNameList[i].text = nameList[i];
            totalScoreList[i].text = "" + scoreList[i];
        }

        // 1�ʂ̃v���C���[������\������
        winnerName.text = nameList[indexNum];
        playerObjList[indexNum].SetActive(true);
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
