using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBy : MonoBehaviour
{
    [SerializeField] GameObject OKbutton;
    [SerializeField] GameObject Cancelbutton;
    [SerializeField] GameObject OKIcon;

    private void Start()
    {
        OKIcon.SetActive(false);
        OKbutton.SetActive(true);
        Cancelbutton.SetActive(false);

    }
    public void OKButton()
    {
        OKIcon.SetActive(true);
        OKbutton.SetActive(false);
        Cancelbutton.SetActive(true);
    }
    public void CancelButton()
    {
        OKIcon.SetActive(false);
        Cancelbutton.SetActive(false);
        OKbutton.SetActive(true);
    }
}
