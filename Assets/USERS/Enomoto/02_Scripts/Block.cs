using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // ���p�l���̃v���t�@�u
    [SerializeField] GameObject roadPrefab;

    // �X�e�[�W�̊Ǘ�
    GameObject startPanel;

    // �v���C���[
    GameObject player;

    // �f�t�H���g�J���[
    Color defaultMaterial;

    // �̌@�̑ΏۂɂȂ��Ă���ꍇ
    public bool isMining = false;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        startPanel = GameObject.Find("StageManager");
        player = GameObject.Find("Player");
        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().mode != Player.PLAYER_MODE.MINING)
        {// �̌@���[�h�ȊO�̏ꍇ
            isMining = false;    // �U
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {// Ray�����������I�u�W�F�N�g�̏���hit�ɓn��

            //**********************
            //  �u�̌@�v�̑ΏۂɂȂ��Ă���ꍇ�i�̌@���[�h�̏ꍇ�j
            //**********************
            if (isMining == true)
            {
                if (hit.transform.gameObject == this.gameObject)
                {// �����ɃJ�[�\������������
                    gameObject.GetComponent<Renderer>().material.color = Color.green; // �ΐF

                    // ���N���b�N����
                    if(Input.GetMouseButtonDown(0))
                    {
                        // ���� �� �j�� �� �x�C�N
                        Bake(roadPrefab, new Vector3(transform.position.x, 0f, transform.position.z), 0, this.gameObject);
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
