using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class StageManager : MonoBehaviour
{
    void Start()
    {
        // 取得する
        GetComponent<NavMeshSurface>().BuildNavMesh();

        // ClientManagerに他のマネージャーを取得させる
        //ClientManager.Instance.GetManagers();
    }

    public void StartBake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
