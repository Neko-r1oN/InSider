using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> roadUIList;

    // �����擾
    RoadManager roadManager;
    GameObject player;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����擾
        road = GameObject.Find("RoadUI");

        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }

        GameObject roadManagerObject = GameObject.Find("RoadManager");
        roadManager = roadManagerObject.GetComponent<RoadManager>();

        // RoadUI���\���ɂ���
        road.SetActive(false);
    }

    public void ShowRoad(int selectNum)
    {// RoadUI��\������
        road.SetActive(true);

        if (selectNum >= 0)
        {
            // �O��I�񂾓�UI���\���ɂ���
            roadUIList[selectNum].SetActive(false);
        }
    }

    public void HideRoad(int selectNum)
    {
        if(selectNum >= 0)
        {
            // ��\���ɂ��Ă�����UI��\��
            roadUIList[selectNum].SetActive(true);
        }

        // ��UI���\��
        road.SetActive(false);

        // �v���C���[���[�h��MOVE�ɕύX
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public bool ActiveRoad()
    {
        // true�Efalse��Ԃ�
        return road.activeSelf;
    }

    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// ���X�g�̒��g���J�E���g����

            // ���X�g�̑S�Ă���]����
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }
}
