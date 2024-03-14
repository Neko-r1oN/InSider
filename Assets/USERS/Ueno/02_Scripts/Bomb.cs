using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Bomb : MonoBehaviour
{
    // �A�j���[�^�[
    Animator animator;

    // �����G�t�F�N�g�v���t�@�u���i�[
    [SerializeField] GameObject explosion;

    // �{�������̃G�t�F�N�g
    [SerializeField] GameObject effectBomb;

    // �p�l�����𑼂̃X�N���v�g����擾
    public GameObject roadPanel;

    GameObject obj;

    // �J�����}�l�[�W���[
    CameraManager camera;

    // �I�[�f�B�I�\�[�X�n
    AudioSource audio;
    [SerializeField] AudioClip explosionSE;

    public int bombID;

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cameraManager = GameObject.Find("CameraManager");
        camera = cameraManager.GetComponent<CameraManager>();

        obj = GameObject.Find("Object001");

        // ���X�ɑ傫������
        transform.DOScale(new Vector3(4f, 4f, 4f), 15f);

        audio = GetComponent<AudioSource>();
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

        audio.PlayOneShot(explosionSE);
    }

    /// <summary>
    /// �{�����������߂����̏���
    /// </summary>
    public void DestroyBomb()
    {
        // ���[�h�p�l���̃^�O��RoadPanel�߂�
        roadPanel.tag = "RoadPanel";

        // �����G�t�F�N�g�𐶐�
        GameObject childEffectObj = Instantiate(effectBomb,
            new Vector3(this.gameObject.transform.position.x, 0.9f, this.transform.position.z), Quaternion.identity);

        // �{��������
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �{������������
    /// </summary>
    /// <param name="other"></param>
    private async Task OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {// �����������C���[��3(Player)�Ȃ�

            Debug.Log("aaaaaaaa");

            if (EditorManager.Instance.useServer == true)
            {// �T�[�o�[���g�p����ꍇ

                Debug.Log("iiiiiiiiiiiiiii");

                if (other.GetComponent<Player>() != null)
                {// Player�X�N���v�g���Ȃ��ꍇ
                    return;
                }

                Debug.Log("uuuuuu");

                //int A = other.GetComponent<Player>().playerObjID;
                //int B = ClientManager.Instance.playerID;

                //Debug.Log(A);
                //Debug.Log(B);

                //if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
                //{// �v���C���[�̃I�u�W�F�N�g���������g�̏ꍇ

                //    Debug.Log("eeeeee");

                    // �N���X�ϐ����쐬
                    Sabotage_Bomb_CancellData cancellData = new Sabotage_Bomb_CancellData();
                    cancellData.playerID = ClientManager.Instance.playerID;
                    cancellData.bombID = bombID;

                    await ClientManager.Instance.Send(cancellData, 201);
                //}
            }
            else
            {// �T�[�o�[���g�p���Ȃ�
                // ���[�h�p�l���̃^�O��RoadPanel�߂�
                roadPanel.tag = "RoadPanel";

                // �����G�t�F�N�g�𐶐�
                GameObject childEffectObj = Instantiate(effectBomb,
                    new Vector3(this.gameObject.transform.position.x, 0.9f, this.transform.position.z), Quaternion.identity);

                // �{��������
                Destroy(this.gameObject);
            }
        }
    }
}
