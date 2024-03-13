using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // アニメーター
    Animator animator;

    // 爆発エフェクトプレファブを格納
    [SerializeField] GameObject explosion;

    // パネル情報を他のスクリプトから取得
    public GameObject roadPanel;

    GameObject obj;

    // カメラマネージャー
    CameraManager camera;

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cameraManager = GameObject.Find("CameraManager");
        camera = cameraManager.GetComponent<CameraManager>();

        obj = GameObject.Find("Object001");
    }

    private void Update()
    {
        //obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_SpecColor", new Color(255,0,0));

        if (Input.GetKeyDown(KeyCode.A))
        {// Aキーを押したら爆発処理を実行
            AtackbombPrefab();
        }
    }

    /// <summary>
    /// 爆発処理
    /// </summary>
    public void AtackbombPrefab()
    {
        // 爆発のアニメーションをtrueにする
        animator.SetBool("attack01", true);

        // 0.6秒後に爆発のエフェクト処理を実行
        Invoke("Explosion", 0.6f);

        Invoke("DestroyBomb", 3f);
    }

    /// <summary>
    /// 爆発エフェクトの処理
    /// </summary>
    public void Explosion()
    {
        // ボムの子オブジェクトに爆発のエフェクトを生成する
        GameObject childObjct = Instantiate(explosion, new Vector3(0,0,0)
            ,Quaternion.identity,this.gameObject.transform);

        // 爆発エフェクトの位置を設定
        childObjct.transform.localPosition = new Vector3(0,1,0);

        // カメラを揺らす処理
        camera.ShakeCamera();
    }

    /// <summary>
    /// ボムを消すためだけの処理
    /// </summary>
    public void DestroyBomb()
    {
        roadPanel.tag = "RoadPanel";

        Destroy(this.gameObject);
    }

    /// <summary>
    /// ボムを消す処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// 当たったレイヤーが3(Player)なら
            // ロードパネルのタグをRoadPanel戻す
            roadPanel.tag = "RoadPanel";

            // ボムを消す
            Destroy(this.gameObject);
        }
    }
}
