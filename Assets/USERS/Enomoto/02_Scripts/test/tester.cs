using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        child.SetActive(true);
        parent.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
