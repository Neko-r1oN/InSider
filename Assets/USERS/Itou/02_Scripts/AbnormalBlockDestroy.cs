using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalBlockDestroy : MonoBehaviour
{
    // �j������I�u�W�F�N�g�ւ̎Q�ƁB
    public GameObject gameObject;

    void Start()
    {
        Invoke("DestroyObj", 5.0f);
    }

    private void DestroyObj()
    {
        // ������GameObject��j��
        Destroy(gameObject);
    }
}
