using UnityEngine;

public class SupprimerObjet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // 1 pour clic droit 
        {
            // Raycast to check if the mouse is over this object
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