using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag3DRectangle : MonoBehaviour
{
    private Vector3 decalage;  // Distance souris-rectangle
    private bool enEtirement = false;
    private Vector3 pointDepart;  // Point de départ de l'étirement
    private Vector3 tailleInitiale;  // Taille initiale du rectangle

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

                // Ajuster la taille du rectangle en fonction du déplacement
                transform.localScale = new Vector3(
                    tailleInitiale.x + deplacement.x,
                    tailleInitiale.y,
                    tailleInitiale.z
                );
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            enEtirement = false;
        }
    }
}