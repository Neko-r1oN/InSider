using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumber : MonoBehaviour
{
    // シングルトン用
    public static PlayerNumber Instance;

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


    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Text>().text = "" + ClientManager.Instance.advancePlayerID;
    }

    public void UpdateText(int n)
    {
        transform.GetComponent<Text>().text = "" + n;

    }
}
