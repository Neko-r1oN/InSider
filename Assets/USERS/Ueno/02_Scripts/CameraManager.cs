using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    GameObject subCamera;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        player = GameObject.Find("Player");          // プレイヤー
        mainCamera = GameObject.Find("Main Camera"); // メインカメラ
        subCamera = GameObject.Find("SubCamera");    // サブカメラ

        // サブカメラを非表示にする
        subCamera.SetActive(false);

        // カメラとプレイヤーの相対距離を求める
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Startで求めたプレイヤーとの位置関係を常にキープするようにカメラを動かす
        transform.position = player.transform.position + offset;
    }

    public void SwitchCamera()
    {// カメラの表示・非表示を切り替える
        if (mainCamera.activeSelf == true)
        {// メインカメラがtrueなら

            // メインを非表示・サブを表示する
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
        }
        else
        {// メインカメラがfalseなら

            // メインを表示・サブを非表示する
            mainCamera.SetActive(true);
            subCamera.SetActive(false);
        }
    }
}
