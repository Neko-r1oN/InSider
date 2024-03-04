using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNamber : MonoBehaviour
{
    // �V���O���g���p
    public static PlayerNamber Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // �V�[���J�ڂ��Ă��j�����Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);

            Debug.Log("OK");
        }
        else
        {
            Destroy(gameObject);

            Debug.Log("NO");
        }
    }

    private void Start()
    {
        transform.GetComponent<Text>().text = "" + ClientManager.Instance.advancePlayerID;
    }

    public void UpdateText(int n)
    {
        transform.GetComponent<Text>().text = "" + n;
    }

}
