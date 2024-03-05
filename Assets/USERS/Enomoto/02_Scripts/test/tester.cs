using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    public List<GameObject> aaas;

    // Start is called before the first frame update
    void Start()
    {
        int cnt = 0;

        foreach(GameObject gameObject in aaas)
        {
            if(gameObject.activeSelf == true)
            {
                cnt++;
            }
        }

        Debug.Log(cnt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
