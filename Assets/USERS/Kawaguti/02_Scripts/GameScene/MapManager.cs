using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject TreasureUI;

    public void SetActiveTreasureUI()
    {
        TreasureUI.SetActive(true);
    }

    public async void SendLie(bool isFlag)
    {
        Debug.Log("Map�Ŗ{���̂��Ƃ��������ǂ���:" + isFlag);

        if (EditorManager.Instance.useServer)
        {
            // �N���X�ϐ����쐬
            MapData mapData = new MapData();
            mapData.playerID = ClientManager.Instance.playerID;
            mapData.isLie = isFlag; // �E�\�����ꍇ��false

            Debug.Log(mapData.playerID);

            await ClientManager.Instance.Send(mapData, 16);
        }

        TreasureUI.SetActive(false);
    }
}
