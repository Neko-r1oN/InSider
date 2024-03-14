using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject Tutorial1;
    [SerializeField] GameObject StartButton;

    [SerializeField] GameObject ButtonUI;


    public void Start()
    {
        StartButton.SetActive(true);
        Tutorial1.SetActive(false);
        ButtonUI.SetActive(false);

    }
    public void StartTutorial()
    {
        StartButton.SetActive(false);
        Tutorial1.SetActive(true);
        ButtonUI.SetActive(true);
       
    }
    public void NextButton()
    {
       
    }

    public void BackButton()
    {
       
    }

    public void StoptTutorial()
    {
        StartButton.SetActive(true);
        Tutorial1.SetActive(false);
        ButtonUI.SetActive(false);
       
    }
}
