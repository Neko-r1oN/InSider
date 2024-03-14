using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Bomb : MonoBehaviour
{
    // アニメーター
    Animator animator;

    // 爆発エフェクトプレファブを格納
    [SerializeField] GameObject explosion;

    // ボム解除のエフェクト
    [SerializeField] GameObject effectBomb;

    // パネル情報を他のスクリプトから取得
    public GameObject roadPanel;

    GameObject obj;

    // カメラマネージャー
    CameraManager camera;

    // オーディオソース系
    AudioSource audio;
    [SerializeField] AudioClip explosionSE;

    public int bombID;

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cameraManager = GameObject.Find("CameraManager");
        camera = cameraManager.GetComponent<CameraManager>();

        obj = GameObject.Find("Object001");

        // 徐々に大きくする
        transform.DOScale(new Vector3(4f, 4f, 4f), 15f);

        audio = GetComponent<AudioSource>();
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

        audio.PlayOneShot(explosionSE);
    }

    /// <summary>
    /// ボムを消すためだけの処理
    /// </summary>
    public void DestroyBomb()
    {
        // ロードパネルのタグをRoadPanel戻す
        roadPanel.tag = "RoadPanel";

        // 解除エフェクトを生成
        GameObject childEffectObj = Instantiate(effectBomb,
            new Vector3(this.gameObject.transform.position.x, 0.9f, this.transform.position.z), Quaternion.identity);

        // ボムを消す
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ボムを消す処理
    /// </summary>
    /// <param name="other"></param>
    private async Task OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// 当たったレイヤーが3(Player)なら

            Debug.Log("aaaaaaaa");

            if (EditorManager.Instance.useServer == true)
            {// サーバーを使用する場合

                Debug.Log("iiiiiiiiiiiiiii");

                if (other.GetComponent<Player>() != null)
                {// Playerスクリプトがない場合
                    return;
                }

                Debug.Log("uuuuuu");

                //int A = other.GetComponent<Player>().playerObjID;
                //int B = ClientManager.Instance.playerID;

                //Debug.Log(A);
                //Debug.Log(B);

                //if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
                //{// プレイヤーのオブジェクトが自分自身の場合

                //    Debug.Log("eeeeee");

                    // クラス変数を作成
                    Sabotage_Bomb_CancellData cancellData = new Sabotage_Bomb_CancellData();
                    cancellData.playerID = ClientManager.Instance.playerID;
                    cancellData.bombID = bombID;

                    await ClientManager.Instance.Send(cancellData, 201);
                //}
            }
            else
            {// サーバーを使用しない
                // ロードパネルのタグをRoadPanel戻す
                roadPanel.tag = "RoadPanel";

                // 解除エフェクトを生成
                GameObject childEffectObj = Instantiate(effectBomb,
                    new Vector3(this.gameObject.transform.position.x, 0.9f, this.transform.position.z), Quaternion.identity);

                // ボムを消す
                Destroy(this.gameObject);
            }
        }
    }
}
