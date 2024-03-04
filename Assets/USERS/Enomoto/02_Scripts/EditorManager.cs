using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public bool useServer;

    //===========================
    //  [�ÓI]�t�B�[���h
    //===========================

    // �V���O���g���p
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
    /// exe���I�������Ƃ��̏���
    /// </summary>
    private void OnDestroy()
    {
        Instance = null;
    }


}
