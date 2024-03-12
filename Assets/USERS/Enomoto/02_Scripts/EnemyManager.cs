using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    /// <summary>
    /// 点滅のコルーチンを開始
    /// </summary>
    public IEnumerator StartBlink(List<GameObject> objList)
    {
        // エネミーオブジェクトを取得
        enemyList = objList;

        bool isActive = false;

        int max = 20;

        for (int i = 0; i < max; i++)
        {
            yield return new WaitForSeconds(0.25f);

            for (int num = 0; num < enemyList.Count; num++)
            {
                enemyList[num].SetActive(isActive);

                // フラグを切り替え
                isActive = !isActive;

                if(i == max - 1)
                {
                    // 破棄
                    Destroy(enemyList[num].gameObject);
                }
            }
        }

        // リストを初期化する
        enemyList = new List<GameObject>();
    }
}