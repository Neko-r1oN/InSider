using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // �i�[�p
    public GameObject[] blocks;

    // ���p�l���̃v���t�@�u
    [SerializeField] List<GameObject> roadPrefab;

    // �u���b�N�̃v���t�@�u
    [SerializeField] GameObject blockPrefab;

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
    }

    /// <summary>
    /// ���𖄂߂�
    /// </summary>
    /// <param name="objeID">�j������I�u�W�F�N�gID</param>
    /// <param name="fillPos">���W</param>
    public void FillObject(int objeID)
    {
        // ���W��ݒ�
        Vector3 fillPos = blocks[objeID].gameObject.transform.position;
        fillPos.y = 1.47f;

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
    public void MineObject(int objeID,int prefabID,int rotY)
    {
        // ���W��ݒ�
        Vector3 minePos = blocks[objeID].gameObject.transform.position;
        minePos.y = 0f;

        // �I�u�W�F�N�g�𐶐�����
        GameObject road = Instantiate(roadPrefab[prefabID], minePos, Quaternion.Euler(0, rotY, 0));

        // �u���b�N�����X�V���j������
        GameObject dieObje = blocks[objeID];
        blocks[objeID] = road;
        Destroy(dieObje);

        // ID��ݒ�
        road.GetComponent<RoadPanel>().objeID = objeID;

        Debug.Log("��ID�F" + blocks[objeID].GetComponent<RoadPanel>().objeID);

        // �x�C�N���J�n
        stageManager.GetComponent<StageManager>().StartBake();
    }
}
