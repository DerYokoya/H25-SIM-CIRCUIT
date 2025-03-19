using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class Interrupteur : Fil
{
    public bool EstOuvert { get; private set; } = false;
    

    private Renderer interrupteurRenderer;
    public Camera camera;


    void Start()
    {
        // Obtenir le composant Renderer de l'objet
        interrupteurRenderer = GetComponent<Renderer>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();

    }
    private void Update()
    {
        base.Update();

        if (sourirEstDessu()) { 
            if (Input.GetMouseButtonDown(1))
            { 
                OuvrirOuFermer();
                tournerInterrupteur();
            }
        }
        
    }


    public void OuvrirOuFermer()
    {
        EstOuvert = !EstOuvert;
        fonctionne = !EstOuvert;
    }

    private void tournerInterrupteur()
    { 

            if (EstOuvert)
        {
            interrupteurRenderer.material.color = Color.white;
        }
        else
        {
            interrupteurRenderer.material.color = Color.black;
        }
    }
}