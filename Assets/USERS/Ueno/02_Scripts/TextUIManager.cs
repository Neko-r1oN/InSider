using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextUIManager : MonoBehaviour
{
    [SerializeField] GameObject textBack;       // �e�L�X�g�w�i�̃I�u�W�F�N�g
    [SerializeField] List<GameObject> textList; // �s���e�L�X�g���X�g

    /// <summary>
    /// �J�[�\���������Ă鎞�ɕ\������
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseEnter(int num)
    {
        Debug.Log("aaaas");

        // �e�L�X�g�̔w�i��\��
        textBack.SetActive(true);
        // �\������Ă������������\��
        textList[num].SetActive(true);
    }


    /// <summary>
    /// �J�[�\�����O�ꂽ�Ƃ��ɔ�\���ɂ���
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseExit(int num)
    {
        // �e�L�X�g�̔w�i���\��
        textBack.SetActive(false);
        // �\������Ă������������\��
        textList[num].SetActive(false);
    }


    /// <summary>
    /// �{�^���������ꂽ�Ƃ��ɔ�\���ɂ���
    /// </summary>
    public void HideText()
    {
        // �e�L�X�g�̔w�i���\��
        textBack.SetActive(false);

        // ���������\��
        for (int i = 0;i < textList.Count; i++)
        {
            textList[i].SetActive(false);
        }
    }
}
