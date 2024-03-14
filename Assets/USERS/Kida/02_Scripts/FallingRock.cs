using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
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
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RoadPanel")
        {
            //Invoke("")
            Instantiate(smok); //スモーク発動
            Destroy(gameObject); //ブロックを破壊
        }
    }
    private void DestroyStone()
    {
        Destroy(gameObject); //ブロックを破壊
    }
}
