using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionsBtn : MonoBehaviour
{
    public void FaireQQchose()
    {
        Console.WriteLine("TEST");
        Debug.Log("TEST............");
    }

    public void OuvrirScene(String nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }

}
