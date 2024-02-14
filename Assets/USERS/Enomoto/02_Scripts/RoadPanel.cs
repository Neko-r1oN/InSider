using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPanel : MonoBehaviour
{
    // �u���b�N�̃v���t�@�u
    [SerializeField] GameObject blockPrefab;

    // �X�e�[�W�̊Ǘ�
    GameObject startPanel;

    // �v���C���[
    GameObject player;

    // �f�t�H���g�J���[
    Color defaultMaterial;

    // �u���߂�v�̑ΏۂɂȂ��Ă��邩�ǂ���
    public bool isFill = false;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        startPanel = GameObject.Find("StageBaker");
        player = GameObject.Find("Player");
        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().mode != Player.PLAYER_MODE.FILL)
        {// ���߂郂�[�h�ȊO�̏ꍇ
            isFill = false;    // �U
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {// Ray�����������I�u�W�F�N�g�̏���hit�ɓn��

            //**********************
            //  �ړ����[�h�̏ꍇ
            //**********************
            if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.MOVE)
            {// ���[�h�FMOVE

                if (hit.transform.gameObject == this.gameObject)
                {// �����ɃJ�[�\������������
                    gameObject.GetComponent<Renderer>().material.color = Color.blue; // �F
                }
                else
                {
                    // �f�t�H���g�J���[�ɖ߂�
                    gameObject.GetComponent<Renderer>().material.color = defaultMaterial;
                }
            }

            //*************************************************************
            //  �u���߂�v�̑ΏۂɂȂ��Ă���ꍇ�i���߂郂�[�h�̏ꍇ�j
            //*************************************************************
            else if (isFill == true)
            {// ���[�h�FMOVE

                if (hit.transform.gameObject == this.gameObject)
                {// �����ɃJ�[�\������������
                    gameObject.GetComponent<Renderer>().material.color = Color.green; // �ΐF

                    // ���N���b�N����
                    if (Input.GetMouseButtonDown(0))
                    {
                        // ���� �� �j�� �� �x�C�N
                        Bake(blockPrefab, new Vector3(transform.position.x, 1.3f, transform.position.z), 0, this.gameObject);
                    }
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.yellow; // ���F
                }
            }

            //************************************
            //  ���̑�
            //************************************
            else
            {
                // �f�t�H���g�J���[�ɖ߂�
                gameObject.GetComponent<Renderer>().material.color = defaultMaterial;
            }
        }
    }

    /// <summary>
    /// �����A�j���A�x�C�N����
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="pos">����������W</param>
    /// <param name="rotY">��������Ƃ��̉�]</param>
    /// <param name="desObject">�j������I�u�W�F�N�g</param>
    private void Bake(GameObject prefab, Vector3 pos, int rotY, GameObject dieObject)
    {
        // �I�u�W�F�N�g�𐶐�����
        GameObject block = Instantiate(prefab, pos, Quaternion.identity);

        // �j������
        Destroy(dieObject);

        // �x�C�N���J�n
        startPanel.GetComponent<StageBake>().StartBake();
    }
}
