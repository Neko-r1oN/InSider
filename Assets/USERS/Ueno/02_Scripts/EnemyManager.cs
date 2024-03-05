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

    public bool touch;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを保持しておく
        navMeshAgent = GetComponent<NavMeshAgent>();

        touch = false;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーを目指して進む
        navMeshAgent.destination = player.transform.position;
    }

    public void CreateEnemy(float posX, float posY, float posZ)
    {
        this.gameObject.SetActive(true);

        this.gameObject.transform.position = new Vector3(posX,posY,posZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pioneer")
        {
            Debug.Log("タッチされた");

            player.GetComponent<Player>().mode = Player.PLAYER_MODE.DOWN;

            touch = true;

            //player.GetComponent<Player>().TouchPlayer();

            Debug.Log(player.GetComponent<Player>().mode);
        }
    }
}
