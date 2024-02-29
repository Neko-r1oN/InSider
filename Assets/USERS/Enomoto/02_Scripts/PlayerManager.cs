using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < players.Count;i++)
        {
            if(i == ClientManager.Instance.advancePlayerID)
            {

            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
