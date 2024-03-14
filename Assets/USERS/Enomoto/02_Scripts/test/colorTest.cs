using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class colorTest : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(new Vector3(4f, 4f, 4f), 20f);
    }
}
