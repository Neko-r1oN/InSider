using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubtManager : MonoBehaviour
{
    [SerializeField] GameObject DoubtUI;

    GameObject buttonManager;

    void Start()
    {
        DoubtUI.SetActive(false);
        buttonManager = GameObject.Find("ButtonManager");
    }

    public void OffDisplayDoubtUI()
    {
        DoubtUI.SetActive(false);

        buttonManager.GetComponent<ButtonManager>().DisplayButton();
    }

    public void DisplayDoubtUI()
    {
        DoubtUI.SetActive(true);

        buttonManager.GetComponent<ButtonManager>().HideButton();
    }
}
