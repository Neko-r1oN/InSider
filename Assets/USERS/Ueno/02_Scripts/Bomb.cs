using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject explosion;

    public GameObject roadPanel;

    CameraManager camera;

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cameraManager = GameObject.Find("CameraManager");
        camera = cameraManager.GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AtackbombPrefab();
        }
    }

    public void AtackbombPrefab()
    {
        animator.SetBool("attack01", true);

        Invoke("Explosion", 0.6f);
    }

    public void Explosion()
    {
        GameObject childObjct = Instantiate(explosion, new Vector3(0,0,0)
            ,Quaternion.identity,this.gameObject.transform);

        childObjct.transform.localPosition = new Vector3(0,1,0);

        camera.ShakeCamera();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            roadPanel.tag = "RoadPanel";

            Destroy(this.gameObject);
        }
    }
}
