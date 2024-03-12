using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    /// <summary>
    /// �_�ł̃R���[�`�����J�n
    /// </summary>
    public IEnumerator StartBlink(List<GameObject> objList)
    {
        // �G�l�~�[�I�u�W�F�N�g���擾
        enemyList = objList;

        bool isActive = false;

        int max = 20;

        for (int i = 0; i < max; i++)
        {
            yield return new WaitForSeconds(0.25f);

            for (int num = 0; num < enemyList.Count; num++)
            {
                enemyList[num].SetActive(isActive);

                // �t���O��؂�ւ�
                isActive = !isActive;

                if(i == max - 1)
                {
                    // �j��
                    Destroy(enemyList[num].gameObject);
                }
            }
        }

        // ���X�g������������
        enemyList = new List<GameObject>();
    }
}