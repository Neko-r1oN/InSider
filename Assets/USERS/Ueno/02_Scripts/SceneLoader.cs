using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Start�̑O�Ɏ��s�����
    /// </summary>
    private void Awake()
    {
        SceneManager.LoadScene("UenoUI", LoadSceneMode.Additive);    // ���݂̃V�[����UIManager�V�[����ǉ�����
    }
}
