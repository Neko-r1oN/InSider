using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

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

    // �G
    GameObject enemy;

    // �{�^���}�l�[�W���[���擾
    ButtonManager buttonManager;

    // �����_���֐�
    System.Random rnd = new System.Random();

    public GameObject targetBlock;
    public int rotY;

    // �����_���̐��l������ϐ�
    int rand;

    private int roadNum; 

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
        if (EditorManager.Instance.useServer)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }

        //enemy = GameObject.Find("enemy");

        //enemy.SetActive(false);

        // Button
        GameObject buttonManagerObject = GameObject.Find("ButtonManager");

        buttonManager = buttonManagerObject.GetComponent<ButtonManager>();

        rand = rnd.Next(1, 20);
    }

    private void Update()
    {
        rand = rnd.Next(1, 20);
    }

    public async void Road(GameObject roadPrefab)
    {
        if (targetBlock == null)
        {// �^�[�Q�b�g�̃u���b�N�����݂��Ȃ�
            return;
        }

        // ���[�h�v���n�u�̊p�x��ς���
        roadPrefab.transform.Rotate(0f, rotY, 0f);

        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            // �f�[�^�ϐ���ݒ�
            Action_MiningData mineData = new Action_MiningData();
            mineData.playerID = ClientManager.Instance.playerID;
            mineData.objeID = targetBlock.GetComponent<Block>().objeID;
            mineData.prefabID = roadNum;
            mineData.rotY = rotY;

            // ���M����
            await ClientManager.Instance.Send(mineData, 7);
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            if(rand <= 15)
            {
                roadPrefab.tag = "RoadPanel";

                // ���� �� �j�� �� �x�C�N
                Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);
            }
            else if(rand > 15)
            {
                roadPrefab.tag = "EventPanel";

                // ���� �� �j�� �� �x�C�N
                Bake(roadPrefab, new Vector3(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z), targetBlock);

                //enemy.GetComponent<EnemyManager>().CreateEnemy(targetBlock.transform.position.x, 0f, targetBlock.transform.position.z);
            }
        }

        // ���I��UI�����
        uiMnager.GetComponent<UIManager>().HideRoad(player.GetComponent<Player>().selectRoadNum);

        // �����Ă���{�^����\������
        buttonManager.DisplayButton();

        // ������
        targetBlock = null;
        rotY = 0;
    }

   //====================
   // ����I��
   //====================
   public void Road(int num)
    {
        Player script = player.GetComponent<Player>();

        if(num == 0 && script.stamina >= 20)
        {// I��
            player.GetComponent<Player>().SubStamina(20);
        }
        else if(num == 1 && script.stamina >= 15)
        {// L��
            player.GetComponent<Player>().SubStamina(15);
        }
        else if (num == 2 && script.stamina >= 30)
        {// T��
            player.GetComponent<Player>().SubStamina(30);
        }
        else if (num == 3 && script.stamina >= 40)
        {// �\��
            player.GetComponent<Player>().SubStamina(40);
        }
        else if (num == 4 && script.stamina >= 10)
        {// �S�~�݂����ȓ�
            player.GetComponent<Player>().SubStamina(10);
        }
        else
        {
            Debug.Log("�X�^�~�i�s���̂��ߐ؂�J���Ȃ�");

            return;
        }

        roadNum = num;

        Road(RoadPrefab[num]);

        player.GetComponent<Player>().selectRoadNum = num;
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
