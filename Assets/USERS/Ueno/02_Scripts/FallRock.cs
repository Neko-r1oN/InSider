using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    [SerializeField] GameObject smok;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyStone", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ブロックが床に当たったら処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoadPanel" && other.gameObject.tag == "Block")
        {
            //Invoke("")
            Instantiate(smok); //スモーク発動
            Destroy(gameObject); //ブロックを破壊
            Debug.Log("あたった？");
        }
    }

    private void DestroyStone()
    {
        Destroy(gameObject); //ブロックを破壊
    }
}
