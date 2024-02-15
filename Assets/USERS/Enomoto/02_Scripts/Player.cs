using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI�p
using UnityEngine.AI;   // AI�p
using Unity.AI.Navigation;
using DG.Tweening;


public class Player : MonoBehaviour
{
    // �������g
    NavMeshAgent agent;

    // �N���b�N�����p�l���̍��W���i�[
    Vector3 clickedTarget;

    // �A�j���[�^�[
    Animator animator;

    // �X�^�~�i�Q�[�W
    GameObject staminaGauge;

    // �ړI�n��ݒ肵�����ǂ���
    bool isSetTarget = false;

    // �v���C���[��Y���W���Œ�
    const float pos_Y = 0.9f;

    // ���S�Ɉړ��������������ǂ���
    public bool isEnd = false;

    // �X�^�~�i
    int stamina = 100;

    // �X�^�~�i�Q�[�W�̐��l
    GameObject staminaNum;

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

        // �X�^�~�i�Q�[�W�̃I�u�W�F�N�g�����擾
        staminaGauge = GameObject.Find("staminaGauge");

        // StaminaNum�����擾
        staminaNum = GameObject.Find("StaminaNum");

        // �A�j���[�^�[�����擾
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Y���W���Œ� �� �ړI�n�ɓ��B�������ǂ����̔��肪����Ȃ邽��
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);

        // ���݂̃X�^�~�i��\��
        staminaNum.GetComponent<Text>().text = "" + stamina;

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

                    // �X�^�~�i�����炷
                    SubStamina(10);
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

        if (agent.velocity.magnitude > 0)
        {// �ړ����͋U
            // �C�ӂ̃A�j���[�V������true�ɕύX
            animator.SetBool("Run", true);

            isEnd = false;
        }
        else if (Mathf.Abs(transform.localEulerAngles.y) >= 179f && isEnd == false) // �������Βl�ɂ���
        {// ��]���I�������

            // ��]�𒲐�
            transform.localEulerAngles = new Vector3(0, 180, 0);

            isEnd = true;

            // �C�ӂ̃A�j���[�V������false�ɕύX
            animator.SetBool("Run", false);
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
    }

    public void SubStamina(int num)
    {
        // �X�^�~�i�����炷
        stamina -= num;

        if(stamina <= 0)
        {// �X�^�~�i��0�ȉ��ɂȂ�����
            // 0�ɌŒ肷��
            stamina = 0;
        }

        // �X���C�_�[�����炷�A�j���[�V����(DOTween)
        staminaGauge.GetComponent<Slider>().DOValue(stamina,1.5f);

        // �c��X�^�~�i��\��(�f�o�b�N�̂�)
        Debug.Log("�c��X�^�~�i" + stamina);
    }
}
