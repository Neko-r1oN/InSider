using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject Tutorial1;
    [SerializeField] GameObject StartButton;

    [SerializeField] GameObject NextButton;
    [SerializeField] GameObject BackButton;

    public void Start()
    {
        StartButton.SetActive(true);
        Tutorial1.SetActive(true);
        NextButton.SetActive(false);
        BackButton.SetActive(false);

    }
    public void StartTutorial()
    {
        StartButton.SetActive(false);
        Tutorial1.SetActive(true);
        NextButton.SetActive(true);
        BackButton.SetActive(true);
    }
    public void StoptTutorial()
    {
        StartButton.SetActive(true);
        Tutorial1.SetActive(true);
        NextButton.SetActive(true);
        BackButton.SetActive(true);
    }
}
