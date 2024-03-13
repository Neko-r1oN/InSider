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
        // クラス変数を作成
        MapData mapData = new MapData();
        mapData.playerID = ClientManager.Instance.playerID;
        mapData.isLie = isFlag; // ウソをつく場合はfalse

        Debug.Log(mapData.playerID);

        await ClientManager.Instance.Send(mapData, 16);

        TreasureUI.SetActive(false);
    }
}
