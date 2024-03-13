using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextUIManager : MonoBehaviour
{
    [SerializeField] GameObject textBack;       // �e�L�X�g�w�i�̃I�u�W�F�N�g
    [SerializeField] List<GameObject> textList; // �s���e�L�X�g���X�g

    // �T�{�^�[�W���ł̂����u�������m�F����e�L�X�g
    [SerializeField] public GameObject saboText;

    // �T�{�^�[�W���ł̂����u���邩�̃e�L�X�g
    Text saboPosstext;

    // �T�{�^�[�W���ł̂����u���ꏊ��I���������̃e�L�X�g
    Text saboPlaceText;

    private void Awake()
    {
        // �e�L�X�g���\���ɂ���O�ɃT�{�^�[�W���񐔕\���e�L�X�g���擾
        saboPosstext = GameObject.Find("SaboPossNum").GetComponent<Text>();
        saboPlaceText = GameObject.Find("SaboPlaceNum").GetComponent<Text>();
    }

    private void Start()
    {
        // �e�L�X�g���\���ɂ���
        saboText.SetActive(false);
    }

    /// <summary>
    /// �J�[�\���������Ă鎞�ɕ\������
    /// </summary>
    /// <param name="num"></param>
    public void OnMouseEnter(int num)
    {
        Debug.Log("�������Ă��");

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
        Debug.Log("������");

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

        // �T�{�^�[�W���̃e�L�X�g���\���ɂ���
        saboText.SetActive(false);

        // ���������\��
        for (int i = 0;i < textList.Count; i++)
        {
            textList[i].SetActive(false);
        }
    }

    /// <summary>
    /// ���߂铹�E���e�̐ݒu�ʒu��I���ł��鐔�̕\������
    /// �����ɂ����u���邩�̍ő吔��n��
    /// </summary>
    public void PossibleNum(int num)
    {
        saboPosstext.text = "/ " + num;
    }

    /// <summary>
    /// ���߂铹�E���e�̐ݒu�ʒu��I���������̕\������
    /// �����ɂ����u��������n��
    /// </summary>
    /// <param name="num"></param>
    public void PutNum(int num)
    {
        saboPlaceText.text = "" + num;
    }
}
