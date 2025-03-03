using UnityEngine;

public class SupprimerObjet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // 1 pour clic droit 
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
    }
}