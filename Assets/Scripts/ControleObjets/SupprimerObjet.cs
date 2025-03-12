using UnityEngine;

public class SupprimerObjet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))  // 1 pour clic droit 
        {
            // Le raycast vérifie si la souris est sur cet objet
            Ray souris = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit elementTouche;

            if (Physics.Raycast(souris, out elementTouche))
            {
                if (elementTouche.collider.gameObject == gameObject)
                {
                    Destroy(gameObject); //Détruire l'objet si la souris fait un clic droit dessus
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // Trouver tous les objets dans la scène qui ont la composante «SupprimerObjet»
            SupprimerObjet[] objetsASupprimer = FindObjectsOfType<SupprimerObjet>();

            foreach (SupprimerObjet objet in objetsASupprimer)
            {
                // Vérifier si l'objet est dans la scène (comme ça les péfabs ne seront pas supprimés)
                if (objet.gameObject.scene.IsValid())
                {
                    Destroy(objet.gameObject);
                }
            }
        }
    }
}