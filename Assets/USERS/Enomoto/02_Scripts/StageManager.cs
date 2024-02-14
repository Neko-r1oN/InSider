using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageManager : MonoBehaviour
{
    void Awake()
    {
        // ベイクを開始
        StartBake();
    }

    /// <summary>
    /// ベイクを開始
    /// </summary>
    public void StartBake()
    {
        // ベイクする
        //this.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
