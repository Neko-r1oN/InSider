using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextUIManager : MonoBehaviour
{
    [SerializeField] GameObject textBack;       // テキスト背景のオブジェクト
    [SerializeField] List<GameObject> textList; // 行動テキストリスト

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
