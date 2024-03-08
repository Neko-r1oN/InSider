using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    [SerializeField] GameObject smok;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyStone", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �u���b�N�����ɓ��������珈��
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoadPanel" && other.gameObject.tag == "Block")
        {
            //Invoke("")
            Instantiate(smok); //�X���[�N����
            Destroy(gameObject); //�u���b�N��j��
            Debug.Log("���������H");
        }
    }

    private void DestroyStone()
    {
        Destroy(gameObject); //�u���b�N��j��
    }
}
