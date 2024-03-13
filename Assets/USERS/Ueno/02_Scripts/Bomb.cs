using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // �A�j���[�^�[
    Animator animator;

    // �����G�t�F�N�g�v���t�@�u���i�[
    [SerializeField] GameObject explosion;

    // �p�l�����𑼂̃X�N���v�g����擾
    public GameObject roadPanel;

    GameObject obj;

    // �J�����}�l�[�W���[
    CameraManager camera;

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cameraManager = GameObject.Find("CameraManager");
        camera = cameraManager.GetComponent<CameraManager>();

        obj = GameObject.Find("Object001");
    }

    private void Update()
    {
        //obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_SpecColor", new Color(255,0,0));

        if (Input.GetKeyDown(KeyCode.A))
        {// A�L�[���������甚�����������s
            AtackbombPrefab();
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void AtackbombPrefab()
    {
        // �����̃A�j���[�V������true�ɂ���
        animator.SetBool("attack01", true);

        // 0.6�b��ɔ����̃G�t�F�N�g���������s
        Invoke("Explosion", 0.6f);

        Invoke("DestroyBomb", 3f);
    }

    /// <summary>
    /// �����G�t�F�N�g�̏���
    /// </summary>
    public void Explosion()
    {
        // �{���̎q�I�u�W�F�N�g�ɔ����̃G�t�F�N�g�𐶐�����
        GameObject childObjct = Instantiate(explosion, new Vector3(0,0,0)
            ,Quaternion.identity,this.gameObject.transform);

        // �����G�t�F�N�g�̈ʒu��ݒ�
        childObjct.transform.localPosition = new Vector3(0,1,0);

        // �J������h�炷����
        camera.ShakeCamera();
    }

    /// <summary>
    /// �{�����������߂����̏���
    /// </summary>
    public void DestroyBomb()
    {
        roadPanel.tag = "RoadPanel";

        Destroy(this.gameObject);
    }

    /// <summary>
    /// �{������������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// �����������C���[��3(Player)�Ȃ�
            // ���[�h�p�l���̃^�O��RoadPanel�߂�
            roadPanel.tag = "RoadPanel";

            // �{��������
            Destroy(this.gameObject);
        }
    }
}
