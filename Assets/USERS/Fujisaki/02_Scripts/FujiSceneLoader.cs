using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FujiSceneLoader : MonoBehaviour
{
    /// <summary>
    /// Start�̑O�Ɏ��s�����
    /// </summary>
    private void Awake()
    {
        SceneManager.LoadScene("fujiUI", LoadSceneMode.Additive);    // ���݂̃V�[����UIManager�V�[����ǉ�����
    }
}
