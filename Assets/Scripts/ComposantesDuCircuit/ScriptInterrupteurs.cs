using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : Fil
{
    public bool EstOuvert { get; private set; } = true;
    
    private float seuilDoubleClic = 0.3f; // Temps max entre deux clics pour que ça compte comme un double clic
    private float dernierMomentDeClic = 0f; // Moment où le dernier clic à eu lieu

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 0 pour clic gauche 
        {
            Ray souris = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interrupteurTouche;

            if (Physics.Raycast(souris, out interrupteurTouche))
            {
                if (interrupteurTouche.collider.gameObject == gameObject)
                {
                    if (Time.time - dernierMomentDeClic < seuilDoubleClic)
                    {
                        OuvrirOuFermer(); // Ouvrir ou fermer s'il y a un double-clic gauche dessus
                        tournerInterrupteur();
                    }
                    dernierMomentDeClic = Time.time;
                }
            }
        }
    }

    public void OuvrirOuFermer()
    {
        EstOuvert = !EstOuvert;
        fonctionne = EstOuvert;
    }

    private void tournerInterrupteur()
    {
        float angleRotation = EstOuvert ? 45f : 0f; // Interrupteur ouvert? Vrai : 45 degrés. Faux : 0 degré.
        transform.rotation = Quaternion.Euler(0f, angleRotation, 0f);
    }
}