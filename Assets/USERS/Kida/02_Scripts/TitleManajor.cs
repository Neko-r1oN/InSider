using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleManajor : MonoBehaviour
{
    public GameObject titleMenu;
    public GameObject sendBotton;
    [SerializeField] InputField nameField;
    [SerializeField] Text playerName;
    static private string userName = "";

    public static string UserName
    {
        get { return userName; }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            titleMenu.SetActive(true);
        }

    }

    public void OnNameClick()
    {
        userName = nameField.text;
        playerName.text = "P1: "+userName;

    }

}
