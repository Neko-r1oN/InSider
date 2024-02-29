using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var test = transform.GetComponent<BoxCollider>();
        test.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
