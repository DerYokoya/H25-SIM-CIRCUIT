using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInfo : MonoBehaviour
{
    public GameObject Info;
    public GameObject bouton;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Si l'utilisateur appuie sur «Tab», le menu va s'afficher
        {
            bool estActiver = Info.activeSelf;
            Info.SetActive(!estActiver);
        }
    }
}


  
