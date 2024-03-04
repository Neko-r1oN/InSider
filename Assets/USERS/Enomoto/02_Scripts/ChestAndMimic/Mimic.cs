using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    void Start()
    {
        Invoke("SceneChange", 5f);
    }

    private void SceneChange()
    {
        // フェード＆シーン遷移
        //Initiate.DoneFading();
        //SceneManager.LoadScene("TitleKawaguchi");

        ClientManager.Instance.DisconnectButton();
    }
}
