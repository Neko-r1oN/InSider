using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] GameObject mimicPrefab;
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // ミミックかどうか

    bool isPlayer;  // プレイヤーを検知したかどうか

    private void Start()
    {
        isPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {// プレイヤーの場合
            Invoke("IsMimic", 2f);
        }
    }

    private void IsMimic()
    {
        if (isPlayer == true)
        {
            return;
        }

        isPlayer = true;

        if (isMimic == true)
        {// 自身がミミックの場合
            text[0].SetActive(true);

            // ミミックにすり替える
            Instantiate(mimicPrefab, transform.position, Quaternion.Euler(0, 180, 0), transform.parent);    // リストに格納
            Destroy(this.gameObject);
            
        }
        else
        {// 宝箱の場合
            text[1].SetActive(true);
        }

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
