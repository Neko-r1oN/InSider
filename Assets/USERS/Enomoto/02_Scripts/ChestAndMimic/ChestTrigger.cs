using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] GameObject mimicPrefab;
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // �~�~�b�N���ǂ���

    bool isPlayer;  // �v���C���[�����m�������ǂ���

    private void Start()
    {
        isPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {// �v���C���[�̏ꍇ
            Invoke("IsMimic", 2f);
        }
    }

    private void IsMimic()
    {
        if (isPlayer == true)
        {
            return;
        }

        isPlayer = true;

        if (isMimic == true)
        {// ���g���~�~�b�N�̏ꍇ
            text[0].SetActive(true);

            // �~�~�b�N�ɂ���ւ���
            Instantiate(mimicPrefab, transform.position, Quaternion.Euler(0, 180, 0), transform.parent);    // ���X�g�Ɋi�[
            Destroy(this.gameObject);
            
        }
        else
        {// �󔠂̏ꍇ
            text[1].SetActive(true);
        }

        Invoke("SceneChange", 5f);
    }

    private void SceneChange()
    {
        // �t�F�[�h���V�[���J��
        //Initiate.DoneFading();
        //SceneManager.LoadScene("TitleKawaguchi");

        ClientManager.Instance.DisconnectButton();
    }
}
