using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoadPanel : MonoBehaviour
{
    // �u���b�N�̃v���t�@�u
    [SerializeField] GameObject blockPrefab;

    // �X�e�[�W�̊Ǘ�
    GameObject stageManager;

    // �v���C���[
    GameObject player;

    RoadManager roadManager;

    // �f�t�H���g�J���[
    Color defaultMaterial;

    // �u���߂�v�̑ΏۂɂȂ��Ă��邩�ǂ���
    public bool isFill = false;

    // �I�u�W�F�N�gID
    public int objeID;

    public bool isSelect;

    // �}�l�[�W���[���擾����
    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        manager = GameObject.Find("BlockList");
        stageManager = GameObject.Find("StageManager");

        GameObject roadManagerobj = GameObject.Find("RoadManager");
        roadManager = roadManagerobj.GetComponent<RoadManager>();
        
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            player = GameObject.Find("Player1");
        }

        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;

        isSelect = false;
    }

    // Update is called once per frame
    async Task Update()
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

            //**********************
            //  ���߂�(�T�{�^�[�W��)���[�h�̏ꍇ
            //**********************
            else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
            {// ���[�h�FMOVE

                if (hit.transform.gameObject == this.gameObject)
                {// �����ɃJ�[�\������������

                    if(hit.transform.gameObject.tag != "StartPanel")
                    {
                        gameObject.GetComponent<Renderer>().material.color = Color.yellow; // �F
                    }
                    
                    // ���N���b�N����
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (isSelect == false)
                        {
                            // ���߂�ꏊ�I�𐔂��J�E���g
                            roadManager.fillCount++;

                            // ���X�g�ɃI�u�W�F�N�g�����i�[
                            roadManager.blokObjList.Add(this.gameObject);
                            Debug.Log("�ǉ����܂����B");

                            isSelect = true;
                        }
                    }
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
                        if (EditorManager.Instance.useServer == true)
                        {// �T�[�o�[���g�p����ꍇ
                            // �f�[�^�ϐ���ݒ�
                            Action_FillData fillData = new Action_FillData();
                            fillData.playerID = ClientManager.Instance.playerID;
                            fillData.objeID = objeID;

                            Debug.Log("���߂�I�u�W�F�N�gID : " + fillData.objeID);

                            // ���M����
                            await ClientManager.Instance.Send(fillData, 6);
                        }
                        else
                        {// �T�[�o�[���g�p���Ȃ�
                            // �I�u�W�F�N�g�𐶐�����
                            GameObject block = Instantiate(blockPrefab, new Vector3(transform.position.x, 1.47f, transform.position.z), Quaternion.identity);



                            // �j������
                            Destroy(this.gameObject);

                            // �x�C�N���J�n
                            stageManager.GetComponent<StageManager>().StartBake();
                        }

                        // �X�^�~�i�����炷
                        player.GetComponent<Player>().SubStamina(20);
                        Debug.Log("�c��X�^�~�i" + player.GetComponent<Player>().stamina);
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
