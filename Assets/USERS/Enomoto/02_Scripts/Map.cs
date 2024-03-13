using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    GameObject mapManager;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// プレイヤーの場合
            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// 自分自身の場合
                mapManager.GetComponent<MapManager>().SetActiveTreasureUI();
            }

            Destroy(this.gameObject);
        }
    }
}
