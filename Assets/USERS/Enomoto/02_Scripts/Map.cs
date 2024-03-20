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

        if(mapManager == null)
        {
            Debug.Log("宝の地図のManagerを取得できない");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// プレイヤーの場合

            if (EditorManager.Instance.useServer)
            {
                if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
                {// プレイヤーのオブジェクトが自分自身の場合
                    mapManager.GetComponent<MapManager>().SetActiveTreasureUI();
                }
            }
            else
            {
                mapManager.GetComponent<MapManager>().SetActiveTreasureUI();
            }

            Destroy(this.gameObject);
        }
    }
}
