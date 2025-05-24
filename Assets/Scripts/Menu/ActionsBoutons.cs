using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsBtn : MonoBehaviour
{
    public void OuvrirScene(String nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}
