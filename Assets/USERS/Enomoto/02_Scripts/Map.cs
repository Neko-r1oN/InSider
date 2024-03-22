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
            Debug.Log("��̒n�}��Manager���擾�ł��Ȃ�");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// �v���C���[�̏ꍇ

            if (EditorManager.Instance.useServer)
            {
                if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
                {// �v���C���[�̃I�u�W�F�N�g���������g�̏ꍇ
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
