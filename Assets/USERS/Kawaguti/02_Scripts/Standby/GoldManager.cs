using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] GameObject[] goldPrefabs;       //ポーションのプレハブ
    [SerializeField] bool slowFlag;                     //ポーションのフラグ判定
    int rand;                                           //ポーション生成をランダムにするための変数
    int randAngle;                                      //ポーションの角度の変数
    // Start is called before the first frame update
    void Start()
    {
        //1.5秒間隔で関数を実行
        InvokeRepeating("SlowGold", 1.0f, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {
    }
    //ポーション射出
    public void SlowGold()
    {
        rand = Random.Range(0, 5);
        randAngle = Random.Range(-180, 180);
        //ポーションを生成してコンポーネントを取得
        GameObject portion = Instantiate(goldPrefabs[rand], transform.position, Quaternion.Euler(-90 + randAngle, 0, 0));
        if (slowFlag)
        {
            portion.GetComponent<Gold>().SlowLeft();
        }
        else
        {
            portion.GetComponent<Gold>().SlowRight();
        }
    }
}