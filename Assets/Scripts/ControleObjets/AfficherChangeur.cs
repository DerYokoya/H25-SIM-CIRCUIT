using Unity.VisualScripting;
using UnityEngine;

public class AfficherChangeur : MonoBehaviour
{
    public Camera camera;
    GameObject changeur;

    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        changeur = Resources.Load<GameObject>("Prefabs/BlocInfos");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject changeurExiste = GameObject.Find("BlocInfos(Clone)"); // Recherche si un bloc existe déja

            // Le raycast vérifie si la souris est sur cet objet
            Ray souris = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit elementTouche;

            if (Physics.Raycast(souris, out elementTouche))
            {
                if (elementTouche.collider.gameObject == gameObject)
                {

                    if (changeurExiste != null) {
                        Destroy(changeurExiste);
                    }
                    // transform.position prend la position de l'objet auquel le script est attaché
                    // on ajoute 15 unités en y à cet objet
                    Vector3 positionDapparition = transform.position + new Vector3(0, 1f, 0);

                    // créer le changeur
                    Instantiate(changeur, positionDapparition, Quaternion.identity);
                }
            }
        }
    }
}