using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManajor : MonoBehaviour
{
    public GameObject spaceText; //PushSpace
    public GameObject titleMenu; //�^�C�g�����j���[
    [SerializeField] InputField nameField; //���O����͂���Ƃ�
    [SerializeField] Text playerName; //�\���p�v���C���[�l�[��
    static private string userName = "";�@//�v���C���[�l�[���i�[�p

    public static string UserName
    {
        get { return userName; }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {//�X�y�[�X�������ƃ��j���[���o��
            spaceText.SetActive(false);
            titleMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {//ESC�L�[���������ꍇ
#if UNITY_EDITOR //Unity�G�f�B�^�̏ꍇ
            UnityEditor.EditorApplication.isPlaying = false;
#else //�r���h�̏ꍇ
                Application.Quit();
#endif
        }

    }
    /// <summary>
    /// ���O���͂��󂯕t���ĕ\��
    /// </summary>
    public void OnNameClick()
    {
        userName = nameField.text;
        playerName.text = "Player: "+userName;

    }

    /// <summary>
    /// �X�^�[�g�{�^���������Ǝ��̃V�[����
    /// </summary>
    public void OnStartClick()
    {
        Initiate.Fade("game", Color.black, 0.5f);
    }

    /// <summary>
    /// End�{�^���������ƃ\�t�g�I��
    /// </summary>
    public void OnEndClick()
    {
#if UNITY_EDITOR //Unity�G�f�B�^�̏ꍇ
        UnityEditor.EditorApplication.isPlaying = false;
#else //�r���h�̏ꍇ
                Application.Quit();
#endif
    }

    /// <summary>
    /// ���j���[����鏈��
    /// </summary>
    public void OnCloseClick()
    {
        titleMenu.SetActive(false);
        spaceText.SetActive(true);
    }


}
