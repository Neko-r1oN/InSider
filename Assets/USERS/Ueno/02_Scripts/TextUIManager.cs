using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextUIManager : MonoBehaviour
{
    [SerializeField] GameObject textBack;       // テキスト背景のオブジェクト
    [SerializeField] List<GameObject> textList; // 行動テキストリスト

    /// <summary>
    /// カーソルがあってる時に表示する
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseEnter(int num)
    {
        Debug.Log("aaaas");

        // テキストの背景を表示
        textBack.SetActive(true);
        // 表示されていた説明文を非表示
        textList[num].SetActive(true);
    }


    /// <summary>
    /// カーソルが外れたときに非表示にする
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseExit(int num)
    {
        // テキストの背景を非表示
        textBack.SetActive(false);
        // 表示されていた説明文を非表示
        textList[num].SetActive(false);
    }


    /// <summary>
    /// ボタンが押されたときに非表示にする
    /// </summary>
    public void HideText()
    {
        // テキストの背景を非表示
        textBack.SetActive(false);

        // 説明文を非表示
        for (int i = 0;i < textList.Count; i++)
        {
            textList[i].SetActive(false);
        }
    }
}
