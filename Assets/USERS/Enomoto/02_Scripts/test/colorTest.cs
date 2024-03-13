using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorTest : MonoBehaviour
{
    [SerializeField] Material mat = default;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", Color.red);
    }
}
