using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class StageManager : MonoBehaviour
{
    void Start()
    {
        // �擾����
        GetComponent<NavMeshSurface>().BuildNavMesh();

        // ClientManager�ɑ��̃}�l�[�W���[���擾������
        //ClientManager.Instance.GetManagers();
    }

    public void StartBake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
