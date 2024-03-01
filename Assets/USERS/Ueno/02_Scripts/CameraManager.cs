using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    GameObject subCamera;

    public CinemachineVirtualCameraBase vcam1;
    public CinemachineVirtualCameraBase vcam2;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        mainCamera = GameObject.Find("Main Camera"); // メインカメラ
        subCamera = GameObject.Find("SubCamera");    // サブカメラ

        // サブカメラを非表示にする
        //subCamera.SetActive(false);
    }

    public void SwitchCamera()
    {// カメラの表示・非表示を切り替える
        if (vcam1.Priority == 0)
        {// メインカメラがtrueなら

            // メインを非表示・サブを表示する
            //mainCamera.SetActive(false);
            //subCamera.SetActive(true);

            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        else
        {// メインカメラがfalseなら

            // メインを表示・サブを非表示する
            //mainCamera.SetActive(true);
            //subCamera.SetActive(false);

            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
    }
}
