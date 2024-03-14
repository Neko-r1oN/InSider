//************************************************************
//
//  �T�[�o�[�g�p���Ƀu���b�N�̐����A���p�l���̐���������
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;   // AI�p

public class BlockManager : MonoBehaviour
{
    // �i�[�p
    public GameObject[] blocks;
    public GameObject[] bombs;

    // ���p�l���̃v���t�@�u
    [SerializeField] List<GameObject> roadPrefab;

    // �u���b�N�̃v���t�@�u
    [SerializeField] GameObject blockPrefab;

    // �S�[���h�̃v���t�@�u
    [SerializeField] GameObject goldPrefab;

    // ���e�̃v���t�@�u
    [SerializeField] GameObject bombPrefab;

    // �g���b�v�̃v���t�@�u
    [SerializeField] GameObject trapPrefab;

    // ���߂�Ƃ��̃A�j���v���n�u
    [SerializeField] GameObject fillAnim;

    // �؂�J���Ƃ��̃A�j���v���n�u
    [SerializeField] GameObject mineAnim;

    // �i�[�p
    GameObject stageManager;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        stageManager = GameObject.Find("StageManager");

        // �e�I�u�W�F�N�g���擾
        GameObject parentObject = GameObject.Find("ParentObject");

        // �q�I�u�W�F�N�g�̐����̔z����쐬
        blocks = new GameObject[this.transform.childCount];

        // �q�I�u�W�F�N�g�����Ɏ擾����
        for (int i = 0; i < blocks.Length; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            childTransform.gameObject.GetComponent<Block>().objeID = i; // ID��ݒ肷��
            blocks[i] = childTransform.gameObject;  // �q�I�u�W�F�N�g���i�[����
        }

        Debug.Log("�u���b�N�̐�:"+blocks.Length);

        // ���e�̍ő吔
        bombs = new GameObject[2];
    }

    /// <summary>
    /// ���𖄂߂�
    /// </summary>
    /// <param name="objeID">�j������I�u�W�F�N�gID</param>
    /// <param name="fillPos">���W</param>
    public IEnumerator FillObject(int objeID)
    {
        // ���W��ݒ�
        Vector3 fillPos = blocks[objeID].gameObject.transform.position;
        fillPos.y = 1.47f;

        // �؂�J���Ƃ��̃A�j���v���t�@�u
        Instantiate(fillAnim, new Vector3(fillPos.x, 1f, fillPos.z), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);    // �����̃A�j���[�V�����̓s���Œx�点��

        // �I�u�W�F�N�g�𐶐�����
        GameObject block = Instantiate(blockPrefab, fillPos, Quaternion.identity);

        // �u���b�N�����X�V���j������
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = block;
        Destroy(dieObje);

        // ID��ݒ�
        block.GetComponent<Block>().objeID = objeID;

        Debug.Log("�u���b�NID�F" + blocks[objeID].GetComponent<Block>().objeID);

        // �x�C�N���J�n
        stageManager.GetComponent<StageManager>().StartBake();
    }

    /// <summary>
    /// �u���b�N��؂�J��
    /// </summary>
    /// <param name="objeID">�j������I�u�W�F�N�gID</param>
    /// <param name="prefabID">�v���t�@�u�̃i���o�[</param>
    /// <param name="rotY">��]�x</param>
    public void MineObject(int playerID,int objeID,int prefabID,int rotY,bool isGetGold)
    {
        // ���W��ݒ�
        Vector3 minePos = blocks[objeID].gameObject.transform.position;
        minePos.y = 0f;

        // �I�u�W�F�N�g�𐶐�����
        GameObject road = Instantiate(roadPrefab[prefabID], minePos, Quaternion.Euler(0, rotY, 0));

        // �؂�J���Ƃ��̃A�j���v���t�@�u
        Instantiate(mineAnim, new Vector3(road.transform.position.x,1f,road.transform.position.z),Quaternion.identity);

        // �u���b�N�����X�V���j������
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = road;
        Destroy(dieObje);

        // ID��ݒ�
        road.GetComponent<RoadPanel>().objeID = objeID;

        Debug.Log("��ID�F" + blocks[objeID].GetComponent<RoadPanel>().objeID);

        // �x�C�N���J�n
        stageManager.GetComponent<StageManager>().StartBake();

        if(isGetGold == true)
        {// �t���O���^�̏ꍇ

            Debug.Log("���̐���");
            Debug.Log(playerID);

            //�S�[���h�𐶐�
            GameObject gold = Instantiate(goldPrefab, minePos, Quaternion.identity);

            // Update�������J�n & �ǔ�������v���C���[ID��������
            gold.GetComponent<BlockGoldManager>().StartMove(playerID);
        }
    }

    /// <summary>
    /// ���e�𐶐�����
    /// </summary>
    public void SetSabotage_Bomb(int objID,int bombID)
    {
        Debug.Log("��������");

        // ���W��ݒ�
        Vector3 minePos = blocks[objID].gameObject.transform.position;
        minePos.y = 0.5f;

        // ���e�𐶐�����
        GameObject bomb = Instantiate(bombPrefab, minePos, Quaternion.identity);

        // ���e��ID��ݒ�
        bomb.GetComponent<Bomb>().bombID = bombID;

        // �I�u�W�F�N�g�̏����i�[����
        bombs[bombID] = bomb;

        // ���e�̉��ɂ���p�l���̏��𔚒e�ɓn��
        bomb.GetComponent<Bomb>().roadPanel = blocks[objID];

        // �p�l���̃^�O��AbnormalPanel�ɕύX
        blocks[objID].tag = "AbnormalPanel";    // ���e�����݂������A�u���߂�v�̑ΏۊO�ɂȂ�
    }

    /// <summary>
    /// �g���b�v��ݒ肷��
    /// </summary>
    public void SetSabotage_Trap(int objID)
    {
        // ���W��ݒ�
        Vector3 minePos = blocks[objID].gameObject.transform.position;
        minePos.y = 0f;

        // �g���b�v�𐶐�����
        GameObject bomb = Instantiate(trapPrefab, minePos, Quaternion.identity);
    }
}
