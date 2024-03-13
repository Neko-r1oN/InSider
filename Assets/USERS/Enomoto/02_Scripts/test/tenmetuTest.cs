using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenmetuTest : MonoBehaviour
{
    [SerializeField] GameObject target;

    int cnt;
    bool isOK;

    private void Start()
    {
        isOK = false;
        cnt = 0;

        this.gameObject.SetActive(false);

        Debug.Log("aaaa");

        InvokeRepeating("IsOK", 0, 1f);
    }

    private void IsOK()
    {
        isOK = !isOK;
        target.SetActive(isOK);

        cnt++;

        if(cnt > 10)
        {
            isOK = true;
            target.SetActive(true);
            CancelInvoke("IsOK");
        }
    }
}
