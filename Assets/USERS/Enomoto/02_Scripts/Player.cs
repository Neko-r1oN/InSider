using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI�p
using UnityEngine.AI;   // AI�p
using Unity.AI.Navigation;


public class Player : MonoBehaviour
{
    [SerializeField] Text modeText; // ���[�h�\��

    // �������g
    NavMeshAgent agent;

    // �N���b�N�����p�l���̍��W���i�[
    Vector3 clickedTarget;

    // �A�j���[�^�[
    Animator animator;

    // �ړI�n��ݒ肵�����ǂ���
    bool isSetTarget = false;

    // �v���C���[��Y���W���Œ�
    const float pos_Y = 0.9f;

    int stamina = 100;

    public enum PLAYER_MODE
    {
        MOVE,   // �ړ�
        MINING, // �̌@
        FILL,   // ���߂�
        NOTHING // �������Ȃ�
    }

    // �v���C���[�̃��[�h
    public PLAYER_MODE mode = PLAYER_MODE.MOVE;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgent���擾����
        agent = GetComponent<NavMeshAgent>();

        // ������
        clickedTarget = transform.position;

        modeText.text = "���݂̃��[�h�F�ړ�";
    }

    // Update is called once per frame
    void Update()
    {
        // ���[�h�ύX
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            modeText.text = "  ���݂̃��[�h�F�ړ�";
            mode = PLAYER_MODE.MOVE;
            stamina -= 10;
            Debug.Log("�c��X�^�~�i" + stamina);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            modeText.text = "  ���݂̃��[�h�F�̌@";
            mode = PLAYER_MODE.MINING;
            stamina -= 10;
            Debug.Log("�c��X�^�~�i" + stamina);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            modeText.text = "  ���݂̃��[�h�F���߂�";
            mode = PLAYER_MODE.FILL;
            stamina -= 10;
            Debug.Log("�c��X�^�~�i+ stamina");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            modeText.text = "  ���݂̃��[�h�F�������Ȃ�";
            mode = PLAYER_MODE.NOTHING;
            stamina -= 10;
            Debug.Log("�c��X�^�~�i" + stamina);
        }

        // Y���W���Œ� �� �ړI�n�ɓ��B�������ǂ����̔��肪����Ȃ邽��
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);

        // �N���b�N���� && ���[�h�FMOVE
        if (Input.GetMouseButtonDown(0) && mode == PLAYER_MODE.MOVE)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {// Ray�����������I�u�W�F�N�g�̏���hit�ɓn��

                if (hit.transform.tag == "RoadPanel")
                {// ���p�l���̏ꍇ
                    // �擾����
                    clickedTarget = hit.collider.transform.position;

                    // ����
                    clickedTarget = new Vector3(clickedTarget.x, pos_Y, clickedTarget.z);

                    // �^
                    isSetTarget = true;

                    // �ړI�n�ֈړ�
                    agent.destination = clickedTarget;
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if(agent.velocity.magnitude <= 0)
        {// �ړ��ʂ�0�ȉ�
            // ���炩�ɉ�]
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // ��������
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //******************************
        //  �̌@���[�h
        //******************************
        if(other.gameObject.tag == "Block")
        {// �̌@�\�ȃu���b�N
            if (mode == PLAYER_MODE.MINING)
            {// �̌@���[�h�̏ꍇ
                // ���̃u���b�N���Ώۂɂ���
                other.GetComponent<Block>().isMining = true;
            }
            else
            {
                // �Ώۂ���O��
                other.GetComponent<Block>().isMining = false;
            }
        }

        //******************************
        //  ���߂郂�[�h
        //******************************
        if(other.gameObject.tag == "RoadPanel")
        {// ���߂邱�Ƃ��\�ȓ��p�l��
            if (mode == PLAYER_MODE.FILL)
            {// ���߂郂�[�h�̏ꍇ
                // ���̓��p�l�����Ώۂɂ���
                other.GetComponent<RoadPanel>().isFill = true;
            }
            else
            {
                // �Ώۂ���O��
                other.GetComponent<RoadPanel>().isFill = false;
            }
        }

        //// �N���b�N����
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (mode == PLAYER_MODE.MINING || mode == PLAYER_MODE.FILL)
        //    {// ���[�h�FMINING || ���[�h�FFILL

        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit = new RaycastHit();

        //        if (Physics.Raycast(ray, out hit))
        //        {// Ray�����������I�u�W�F�N�g�̏���hit�ɓn��

        //            //******************************
        //            //  �̌@���[�h
        //            //******************************
        //            if (other.gameObject.tag == "Block" && hit.transform.gameObject == other.gameObject
        //                && mode == PLAYER_MODE.MINING)
        //            {// �u���b�N�̏ꍇ

        //                // ���� �� �j�� �� �x�C�N
        //                Bake(roadPrefab, new Vector3(other.transform.position.x, 0f, other.transform.position.z), 0, other.gameObject);
        //            }

        //            //******************************
        //            //  ���߂郂�[�h
        //            //******************************
        //            else if (other.gameObject.tag == "RoadPanel" && hit.transform.gameObject == other.gameObject
        //                && mode == PLAYER_MODE.FILL)
        //            {// ���p�l���̏ꍇ

        //                // ���� �� �j�� �� �x�C�N
        //                Bake(blockPrefab, new Vector3(other.transform.position.x, 1.3f, other.transform.position.z), 0, other.gameObject);
        //            }
        //        }
        //    }
        //}
    }

    void PushButton()
    {
        
    }
}
