using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmanager : MonoBehaviour
{
    int rotY;

    GameObject parentObj;

    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rotY += 1;

        parentObj.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

        if (rotY >= 360)
        {
            rotY = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(parentObj);
        Debug.Log("“–‚½‚Á‚½");
    }
}
