using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using System.Threading.Tasks;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject canvasObj;
    [SerializeField] GameObject loadingPrefab;

    async Task Start()
    {
        // 取得する
        GetComponent<NavMeshSurface>().BuildNavMesh();

        if (EditorManager.Instance.useServer == true)
        {
            // ClientManagerに他のマネージャーを取得させる
            ClientManager.Instance.GetManagers();

            // 通信中のテキストを生成
            GameObject text = Instantiate(loadingPrefab, canvasObj.transform);
            text.transform.localPosition = Vector3.zero;

            // オブジェクトを持たせる
            ClientManager.Instance.loadingObj = text;

            // ゲームシーンに遷移できたことをサーバーに送信
            RoundStartData roundStartData = new RoundStartData();
            await ClientManager.Instance.Send(roundStartData, 13);
        }
    }

    public void StartBake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
