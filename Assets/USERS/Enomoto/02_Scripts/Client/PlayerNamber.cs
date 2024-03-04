using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNamber : MonoBehaviour
{
    // シングルトン用
    public static PlayerNamber Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // シーン遷移しても破棄しないようにする
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
