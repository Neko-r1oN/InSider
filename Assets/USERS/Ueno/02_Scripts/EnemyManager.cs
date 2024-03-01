using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player")]
    private GameObject player;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgent��ێ����Ă���
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[��ڎw���Đi��
        navMeshAgent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pioneer")
        {
            Debug.Log("�^�b�`���ꂽ");
        }
    }
}
