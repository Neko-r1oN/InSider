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
        startPanel = GameObject.Find("StageManager");
        player = GameObject.Find("Player1");
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
            else if (isFill == true && this.gameObject.tag == "RoadPanel")
            {// ���[�h�FFILL

                if (hit.transform.gameObject == this.gameObject)
                {// �����ɃJ�[�\������������
                    gameObject.GetComponent<Renderer>().material.color = Color.green; // �ΐF

                    // ���N���b�N����
                    if (Input.GetMouseButtonDown(0))
                    {
                        // �I�u�W�F�N�g�𐶐�����
                        GameObject block = Instantiate(blockPrefab, new Vector3(transform.position.x, 1.47f, transform.position.z), Quaternion.identity);

                        // �j������
                        Destroy(this.gameObject);

                        // �x�C�N���J�n
                        startPanel.GetComponent<StageBake>().StartBake();
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
}
