using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class StageBake : MonoBehaviour
{
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void StartBake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
