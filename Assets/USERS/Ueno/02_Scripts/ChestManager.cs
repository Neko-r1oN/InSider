using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    GameObject textUI;

    private void Start()
    {
        textUI = GameObject.Find("TextUIManager");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                textUI.GetComponent<TextUIManager>().OnMouseEnter(10);
            }
            if(hit.transform.gameObject != this.gameObject)
            {
                textUI.GetComponent<TextUIManager>().OnMouseExit(10);
            }
        }
    }
}
