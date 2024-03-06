using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextUIManager : MonoBehaviour
{
    [SerializeField] GameObject textBack;       // �e�L�X�g�w�i�̃I�u�W�F�N�g
    [SerializeField] List<GameObject> textList; // �s���e�L�X�g���X�g

    public void OnMouseEnter(int num)
    {
        textBack.SetActive(true);
        textList[num].SetActive(true);
    }

    public void OnMouseExit(int num)
    {
        textBack.SetActive(false);
        textList[num].SetActive(false);
    }

    public void HideText()
    {
        textBack.SetActive(false);

        for(int i = 0;i < textList.Count; i++)
        {
            textList[i].SetActive(false);
        }
    }
}
