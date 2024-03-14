using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
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
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RoadPanel")
        {
            //Invoke("")
            Instantiate(smok); //�X���[�N����
            Destroy(gameObject); //�u���b�N��j��
        }
    }
    private void DestroyStone()
    {
        Destroy(gameObject); //�u���b�N��j��
    }
}
