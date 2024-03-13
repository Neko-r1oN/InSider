using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalBlockDestroy : MonoBehaviour
{
    // 破棄するオブジェクトへの参照。
    public GameObject gameObject;

    void Start()
    {
        Invoke("DestroyObj", 5.0f);
    }

    private void DestroyObj()
    {
        // 引数のGameObjectを破棄
        Destroy(gameObject);
    }
}
