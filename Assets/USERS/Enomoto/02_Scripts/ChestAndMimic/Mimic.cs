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
        // �t�F�[�h���V�[���J��
        //Initiate.DoneFading();
        //SceneManager.LoadScene("TitleKawaguchi");

        ClientManager.Instance.DisconnectButton();
    }
}
