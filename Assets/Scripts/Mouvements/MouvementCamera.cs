using System;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private GameObject personnage;

    void Start()
    {
        // Trouver le GameObject par son nom
        personnage = GameObject.Find("personnage");

        if (personnage != null)
        {
           
            Debug.Log("Personnage trouv� !");
            // Vous pouvez maintenant acc�der � ses composants, comme transform, renderer, etc.
            Vector3 position = personnage.transform.position;
            Debug.Log($"Position de personnage: {position}");
        }
        else
        {
            Debug.LogError("Personnage non trouv� !");
        }
    }
}