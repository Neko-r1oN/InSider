using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAcyive_False : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("ActiveFalse",10f);
    }

    private void ActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
