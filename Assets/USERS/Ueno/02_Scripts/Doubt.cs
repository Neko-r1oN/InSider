using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubt : MonoBehaviour
{
    [SerializeField] GameObject doubt;

    // Start is called before the first frame update
    void Start()
    {
        doubt.SetActive(false);
    }

    public void DisplayDoubt()
    {
        doubt.SetActive(true);
    }
}
