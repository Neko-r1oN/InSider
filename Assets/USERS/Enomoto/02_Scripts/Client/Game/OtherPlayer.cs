using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI�p
using Unity.AI.Navigation;
using DG.Tweening;

public class OtherPlayer : MonoBehaviour
{
    // �������g
    NavMeshAgent agent;

    // �A�j���[�^�[
    Animator animator;

    // ���g�̃v���C���[ID
    public int id;

    // �v���C���[��Y���W���Œ�
    const float pos_Y = 0.9f;

    // ���S�Ɉړ��������������ǂ���
    public bool isEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgent���擾����
        agent = GetComponent<NavMeshAgent>();

        // �A�j���[�^�[�����擾
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Y���W���Œ� �� �ړI�n�ɓ��B�������ǂ����̔��肪����Ȃ邽��
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance <= 0)
        {// �ړ��ʂ�0�ȉ�
            // ���炩�ɉ�]
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // ��������
        }

        if (agent.remainingDistance > 0)
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

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="targetPos"></param>
    public void MoveAgent(Vector3 targetPos)
    {
        // �ړI�n�ֈړ�
        agent.destination = targetPos;
    }
}
