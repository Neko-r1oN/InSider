using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] GameObject tuto;

    [SerializeField] GameObject Tutorial1;
    [SerializeField] GameObject Tutorial2;
    [SerializeField] GameObject Tutorial3;
    [SerializeField] GameObject Tutorial4;
    [SerializeField] GameObject Tutorial5;

    [SerializeField] GameObject StartButton;

    [SerializeField] GameObject ButtonUI;

    private int num;

    public void Start()
    {
        num = 0;
        StartButton.SetActive(true);

        tuto.SetActive(false);

        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(false);
        ButtonUI.SetActive(false);

    }
    public void StartTutorial()
    {
        num = 1;

        tuto.SetActive(true);

        StartButton.SetActive(false);
        Tutorial1.SetActive(true);
        ButtonUI.SetActive(true);
       
    }
    public void NextButton()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(false);

        if (num >= 5)
        {
            num = 5;
        }

        if (num == 1)
        {
            Tutorial1.SetActive(true);
        }
        if (num == 2)
        {
            Tutorial2.SetActive(true);
        }
        if (num == 3)
        {
            Tutorial3.SetActive(true);
        }
        if (num ==4)
        {
            Tutorial4.SetActive(true);
        }
        if (num == 5)
        {
            Tutorial5.SetActive(true);
            StartButton.SetActive(false);

        }
        num += 1;
    }

    public void BackButton()
    {
        num -= 1;
        if (num >= 1)
        {
            num = 1;
        }

       
        if (num == 1)
        {
            Tutorial2.SetActive(false);
        }
        if (num ==2)
        {
            Tutorial3.SetActive(false);
        }
        if (num == 3)
        {
            Tutorial4.SetActive(false);
        }
        if (num == 4)
        {
            Tutorial5.SetActive(false);
        }
    }

    public void StoptTutorial()
    {
        tuto.SetActive(false);
        StartButton.SetActive(true);
        Tutorial1.SetActive(false);
        ButtonUI.SetActive(false);
       
    }
}
