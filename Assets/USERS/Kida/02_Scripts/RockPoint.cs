using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPoint : MonoBehaviour
{
    [SerializeField] GameObject fallingLock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject fallLock = Instantiate(fallingLock);

        }
    }
}
