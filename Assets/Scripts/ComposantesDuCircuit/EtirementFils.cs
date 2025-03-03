using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtirementRectangle : MonoBehaviour
{
    private Vector3 decalage;  // Distance souris-rectangle
    private bool enEtirement = false;
    private Vector3 pointDepart;  // Point de départ de l'étirement
    private Vector3 tailleInitiale;
    private Vector3 positionInitiale;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray laser = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit laserTouche;

            if (Physics.Raycast(laser, out laserTouche))
            {
                if (laserTouche.collider.gameObject == gameObject)
                {
                    decalage = gameObject.transform.position - laserTouche.point;
                    pointDepart = laserTouche.point;
                    tailleInitiale = transform.localScale;
                    positionInitiale = transform.position;
                    enEtirement = true;
                }
            }
        }

        if (enEtirement && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit laserTouche;

            if (Physics.Raycast(ray, out laserTouche))
            {
                // Calculer le déplacement de la souris par rapport au point de départ
                Vector3 deplacement = laserTouche.point - pointDepart;

                // Déterminer la direction du déplacement (gauche ou droite)
                float nouvelleLargeur = tailleInitiale.x + Mathf.Abs(deplacement.x); // Taille toujours positive
                float direction = Mathf.Sign(deplacement.x); // -1 pour gauche, 1 pour droite

                // Limiter la largeur pour éviter des valeurs négatives ou trop petites
                if (nouvelleLargeur > 0)
                {
                    // Ajuster la taille du rectangle
                    transform.localScale = new Vector3(
                        nouvelleLargeur,
                        tailleInitiale.y,
                        tailleInitiale.z
                    );

                    // Ajuster la position en fonction de la direction
                    if (direction > 0) // Tirer vers la droite
                    {
                        transform.position = new Vector3(
                            positionInitiale.x + deplacement.x / 2,
                            positionInitiale.y,
                            positionInitiale.z
                        );
                    }
                    else // Tirer vers la gauche
                    {
                        transform.position = new Vector3(
                            positionInitiale.x + deplacement.x / 2,
                            positionInitiale.y,
                            positionInitiale.z
                        );
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            enEtirement = false;
        }
    }
}