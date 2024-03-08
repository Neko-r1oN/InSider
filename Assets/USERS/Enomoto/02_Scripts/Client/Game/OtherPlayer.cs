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

    // �p�X
    NavMeshPath path = null;

    // �v���C���[��Y���W���Œ�
    const float pos_Y = 0.9f;

    // ���S�Ɉړ��������������ǂ���
    public bool isEnd = false;

    // ���̃X�N���v�g�𑕔����Ă���I�u�W�F�N�g��ID
    public int playerObjID;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgent���擾����
        agent = GetComponent<NavMeshAgent>();

        // �A�j���[�^�[�����擾
        animator = GetComponent<Animator>();

        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        // Y���W���Œ� �� �ړI�n�ɓ��B�������ǂ����̔��肪����Ȃ邽��
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance <= 0.1f)
        {// �ړ��ʂ�0�ȉ�
            // ���炩�ɉ�]
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // ��������
        }

        if (agent.remainingDistance > 0.1f)
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
    public async void MoveAgent(Vector3 targetPos)
    {
        //// NavMesh�̃p�X���擾
        NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);

        // �N���X�ϐ����쐬
        RevisionPosData revisionPosData = new RevisionPosData();

        if (path.corners.Length > 0)
        {// �p�X����ꂽ�ꍇ
            var length = path.corners[path.corners.Length - 1] - targetPos;

            if (length.magnitude < 1.0f)
            {// �ړI�n�ɂ��ǂ蒅�����Ƃ��ł���ꍇ

                // �ړI�n�ֈړ�
                agent.destination = targetPos;
            }
            else
            {
                Debug.Log("���W���C������");

                // �{���̍��W�ɏC������ʒm�𑗐M
                revisionPosData.playerID = ClientManager.Instance.playerID;
                revisionPosData.targetID = playerObjID;
                revisionPosData.isBuried = false;

                // [revisionPosData]�T�[�o�[�ɑ��M
                await ClientManager.Instance.Send(revisionPosData, 12);
            }
        }
        else
        {
            Debug.Log("���W���C������");

            // �{���̍��W�ɏC������ʒm�𑗐M
            revisionPosData.playerID = ClientManager.Instance.playerID;
            revisionPosData.targetID = playerObjID;
            revisionPosData.isBuried = false;

            // [revisionPosData]�T�[�o�[�ɑ��M
            await ClientManager.Instance.Send(revisionPosData, 12);
        }
    }

    /// <summary>
    /// ���W���C������
    /// </summary>
    /// <param name="pos"></param>
    public void RevisionPos(Vector3 pos,bool isBruried)
    {
        if (isBruried == true)
        {// �u���b�N�ɖ��܂������̏ꍇ

            // ���W������������
            pos = new Vector3(0f, 0.9f, -5f);

            // �����p�[�e�B�N�����o��
            //Instantiate();

            Debug.Log("pos���C��");
        }

        // Agent�̖ړI�n��ݒ�
        agent.destination = pos;

        // �R���|�[�l���g�𖳌��ɂ���
        agent.enabled = false;

        // �J�n�ʒu�ֈړ�
        transform.position = pos;

        // �R���|�[�l���g��L���ɂ���
        agent.enabled = true;

        Debug.Log(pos);
    }
}
