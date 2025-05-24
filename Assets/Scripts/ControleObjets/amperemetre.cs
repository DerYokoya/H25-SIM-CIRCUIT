using TMPro;
using UnityEngine;
using System.Globalization;
using UnityEditor;

public class Amperemetre : MonoBehaviour
{
    public GameObject outil; // Outil qui va afficher le nombre d'ampères
    public TextMeshPro nombreAmperes;
    public Camera cameraJoueur;
    public float distanceDetectionMax = 5f;

    void Update()
    {
        // Tenir/déséquiper l'outil avec la touche 7
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            outil.SetActive(!outil.activeSelf);
        }

        // Détection du survol
        if (outil.activeSelf)
        {
            Ray rayon = cameraJoueur.ScreenPointToRay(Input.mousePosition);
            RaycastHit impact;

            if (Physics.Raycast(rayon, out impact, distanceDetectionMax))
            {
                ComposanteDuCircuit composante = impact.collider.GetComponentInParent<ComposanteDuCircuit>();
                if (composante != null)
                {
                    float courant = GraphManager.Instance.GetCurrentForComponent(composante);
                    // Utiliser CultureInfo.InvariantCulture pour forcer le point décimal (0.0001 au lieu de 0,001)
                    nombreAmperes.text = string.Format(CultureInfo.InvariantCulture, "{0:F3} A", courant);
                    return;
                }
            }

            nombreAmperes.text = "0.000 A"; // Aucune composante détectée
        }
    }
}