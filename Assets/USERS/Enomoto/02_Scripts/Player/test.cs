using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI—p

public class test : MonoBehaviour
{
    [SerializeField] GameObject chest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = chest.transform.position;
            GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}