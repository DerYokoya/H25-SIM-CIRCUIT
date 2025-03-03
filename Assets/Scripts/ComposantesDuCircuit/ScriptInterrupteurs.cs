using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interrupteur : ComposanteDuCircuit
{
    public bool EstOuvert { get; private set; } = true;

    void Update()
    {
        {
            if (Input.GetMouseButtonDown(0))  // 0 pour clic gauche 
            {

                // Le raycast vérifie si la souris est sur cet objet
                Ray souris = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit interrupteurTouche;

                if (Physics.Raycast(souris, out interrupteurTouche))
                {
                    if (interrupteurTouche.collider.gameObject == gameObject)
                    {
                        OuvrirOuFermer(); //Ouvrir ou fermer s'il y a un clic gauche dessus
                    }
                }
            }
        }
    }

    public void OuvrirOuFermer()
    {
        if (EstOuvert)
        {
            fonctionne = true;
        }

        else {
            fonctionne = false;
        }
    }

}
