using UnityEngine;

public class SupprimerObjet : MonoBehaviour
{
    public Camera camera;
    void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Le raycast vérifie si la souris est sur cet objet
            Ray souris = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit elementTouche;

            if (Physics.Raycast(souris, out elementTouche))
            {
                if (elementTouche.collider.gameObject == gameObject && (Input.GetKeyDown(KeyCode.Q)))
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