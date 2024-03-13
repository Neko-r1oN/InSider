using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundResultManager : MonoBehaviour
{
    [SerializeField] List<GameObject> playerUIList;
    [SerializeField] List<Text> playerNameList;
    [SerializeField] List<Text> totalScoreList;
    [SerializeField] List<Text> allieScoreList;
    [SerializeField] List<GameObject> insiderLogoList;

    // �ʐM�ҋ@���pUI
    [SerializeField] GameObject loadingPrefab;
    [SerializeField] GameObject canvasObj;

    bool isClick;

    // �V���O���g���p
    public static RoundResultManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isClick = false;
    }

    private void Update()
    {
        if(isClick == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            isClick = true;

            // �ʐM���̃e�L�X�g�𐶐�
            GameObject text = Instantiate(loadingPrefab, canvasObj.transform);
            text.transform.localPosition = new Vector3(508, -507, 0);

            Invoke("SendNotification", 5f);
        }
    }

    private async void SendNotification()
    {
        Debug.Log("���݂̃��E���h���F" + ClientManager.Instance.roundNum);

        // �K���ȃN���X�ϐ����쐬
        ReadyData readyData = new ReadyData();

        // ���̃��E���h�V�[���ɑJ�ڂ��鏀�����ł������Ƃ�ʒm
        await ClientManager.Instance.Send(readyData, 15);
    }

    public void SetUI(List<int> totalScore, List<int> allieScore, List<int> insiderID)
    {
        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i >= totalScore.Count)
            {// ���݂��Ȃ��v���C���[��j������
                Destroy(playerUIList[i]);
                continue;
            }

            totalScoreList[i].text = "" + totalScore[i];
            allieScoreList[i].text = "(+" + allieScore[i] + ")";
        }

        for (int i = 0; i < insiderID.Count; i++)
        {
            insiderLogoList[insiderID[i]].SetActive(true);    // Insider���S�}�[�N
        }
    }
}
