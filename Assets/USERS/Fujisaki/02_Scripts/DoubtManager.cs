using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubtManager : MonoBehaviour
{
    [SerializeField] GameObject DoubtUI;

    void Start()
    {
        DoubtUI.SetActive(false);
    }

    public void OffDisplayDoubtUI()
    {
        DoubtUI.SetActive(false);
    }

    public void DisplayDoubtUI()
    {
        DoubtUI.SetActive(true);
    }
}
