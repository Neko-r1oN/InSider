using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FujiSceneLoader : MonoBehaviour
{
    /// <summary>
    /// Startの前に実行される
    /// </summary>
    private void Awake()
    {
        SceneManager.LoadScene("fujiUI", LoadSceneMode.Additive);    // 現在のシーンにUIManagerシーンを追加する
    }
}
