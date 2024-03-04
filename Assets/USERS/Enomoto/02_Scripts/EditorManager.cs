using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public bool useServer;

    //===========================
    //  [静的]フィールド
    //===========================

    // シングルトン用
    public static EditorManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Debug.Log("OK");
        }
        else
        {
            Destroy(gameObject);

            Debug.Log("NO");
        }
    }

    /// <summary>
    /// exeを終了したときの処理
    /// </summary>
    private void OnDestroy()
    {
        Instance = null;
    }


}
