using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuInfo : MonoBehaviour
{
    public GameObject Info;
    public GameObject bouton;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool estActiver = Info.activeSelf;
            Info.SetActive(!estActiver);
        }
    }
}


  
