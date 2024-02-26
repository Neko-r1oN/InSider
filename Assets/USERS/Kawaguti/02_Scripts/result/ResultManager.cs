using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    //��������I�u�W�F�N�g��Inspector����w�肷��p
    public GameObject spawnObject;
    //�����Ԋu�p
    public float interval = 3.0f;


    void Start()
    {
        //�R���[�`���̊J�n
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        //�������[�v�̊J�n
        while (true)
        {
            //�����������I�u�W�F�N�g�̈ʒu�ɁA��������I�u�W�F�N�g���C���X�^���X�����Đ�������
            Instantiate(spawnObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}
