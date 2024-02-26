using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] GameObject[] goldPrefabs;       //�|�[�V�����̃v���n�u
    [SerializeField] bool slowFlag;                     //�|�[�V�����̃t���O����
    int rand;                                           //�|�[�V���������������_���ɂ��邽�߂̕ϐ�
    int randAngle;                                      //�|�[�V�����̊p�x�̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        //1.5�b�Ԋu�Ŋ֐������s
        InvokeRepeating("SlowGold", 1.0f, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {
    }
    //�|�[�V�����ˏo
    public void SlowGold()
    {
        rand = Random.Range(0, 5);
        randAngle = Random.Range(-180, 180);
        //�|�[�V�����𐶐����ăR���|�[�l���g���擾
        GameObject portion = Instantiate(goldPrefabs[rand], transform.position, Quaternion.Euler(-90 + randAngle, 0, 0));
        if (slowFlag)
        {
            portion.GetComponent<Gold>().SlowLeft();
        }
        else
        {
            portion.GetComponent<Gold>().SlowRight();
        }
    }
}