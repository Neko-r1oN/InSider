using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // ���[�h�v���n�u���i�[
    [SerializeField] GameObject[] RoadPrefab = new GameObject[5];

    // �x�C�N�I�u�W�F�N�g���擾
    GameObject Baker;

    // UIManager
    GameObject uiMnager;

    // �v���C���[
    GameObject player;

    // �{�^���}�l�[�W���[���擾
    ButtonManager buttonManager;

    public GameObject targetBlock;
    public int rotY;


    public int roadNum; 

    // Start is called before the first frame update
    void Start()
    {
        rotY = 0;
        targetBlock = null;

        // Bake
        Baker = GameObject.Find("StageManager");

        // UIManager
        uiMnager = GameObject.Find("UIManager");

        // Player
        player = GameObject.Find("Player1");

        // Button
        GameObject buttonManagerObject = GameObject.Find("ButtonManager");

        buttonManager = buttonManagerObject.GetComponent<ButtonManager>();
    }

    public void Road(GameObject roadPrefab)
    {
        if (targetBlock == null)
        {// �^�[�Q�b�g�̃u���b�N�����݂��Ȃ�
            return;
        }

        // ���[�h�v���n�u�̊p�x��ς���
        roadPrefab.transform.Rotate(0f, rotY, 0f);

        // ���� �� �j�� �� �x�C�N
        Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);

        // ���I��UI�����
        uiMnager.GetComponent<UIManager>().HideRoad(player.GetComponent<Player>().selectRoadNum);

        // �����Ă���{�^����\������
        buttonManager.DisplayButton();
    }

   //====================
   // ����I��
   //====================
   public void Road(int num)
    {
        Road(RoadPrefab[num]);

        if(num == 0)
        {
            player.GetComponent<Player>().SubStamina(20);
        }
        else if(num == 1)
        {
            player.GetComponent<Player>().SubStamina(15);
        }
        else if (num == 2)
        {
            player.GetComponent<Player>().SubStamina(30);
        }
        else if (num == 3)
        {
            player.GetComponent<Player>().SubStamina(40);
        }
        else if (num == 4)
        {
            player.GetComponent<Player>().SubStamina(10);
        }

        player.GetComponent<Player>().selectRoadNum = num;

        //roadNum = num;
    }
   
    public void AddRotButton()
    { //���̉�]
        rotY += 90;

        if (rotY >= 360)
        {
            rotY = 0;
        }
    }

    /// <summary>
    /// �����A�j���A�x�C�N����
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="pos">����������W</param>
    /// <param name="desObject">�j������I�u�W�F�N�g</param>
    public void Bake(GameObject prefab, Vector3 pos, GameObject dieObject)
    {
        // �I�u�W�F�N�g�𐶐�����
        GameObject block = Instantiate(prefab, pos, Quaternion.Euler(0, rotY, 0));

        // �j������
        Destroy(dieObject);

        // �x�C�N���J�n
        Baker.GetComponent<StageBake>().StartBake();

        // ������
        targetBlock = null;
        rotY = 0;
    }
}
