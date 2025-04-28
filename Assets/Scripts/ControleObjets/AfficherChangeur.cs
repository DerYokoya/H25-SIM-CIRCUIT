using TMPro;
using UnityEngine;

public class AfficherChangeur : MonoBehaviour
{
    public Transform personnage;
    public Camera camera;
    GameObject changeur;
    GameObject changeurActuel; // Plus statique, chaque instance a son propre changeur

    ComposanteDuCircuit composanteActuelle;
    GameObject boutonPositif;
    GameObject boutonNegatif;

    void Start()
    {
        changeur = Resources.Load<GameObject>("Prefabs/BlocInfos");

        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("Collider manquant pour l'objet: " + gameObject.name);
        }

    }

    void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        personnage = GameObject.Find("Personnage").GetComponent<Transform>();
        Ray souris = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit elementTouche;

        // Mettre à jour le texte du changeur s'il existe et appartient à cet objet
        if (changeurActuel != null)
        {
            Vector3 vecteurLien = personnage.position - changeurActuel.transform.position;
            Quaternion rotation = Quaternion.LookRotation(vecteurLien);
            changeurActuel.transform.rotation = rotation  ;


            TextMeshPro texte = changeurActuel.GetComponentInChildren<TextMeshPro>();
            if (composanteActuelle != null)
            {
                texte.text = composanteActuelle.TexteValeur();
            }

            //Suivre la composante du circuit
            changeurActuel.transform.position = transform.position + new Vector3(0, composanteActuelle.GetComponent<BoxCollider>().size.y + 0.2f, 0);
        }


        // Gestion des clics sur les boutons
        if (Physics.Raycast(souris, out elementTouche))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Vérifier si le clic est sur nos boutons et que le changeur actuel appartient à cet objet
                if (changeurActuel != null)
                {
                    if (boutonNegatif != null && elementTouche.collider.gameObject == boutonNegatif)
                    {
                        composanteActuelle?.Diminution();
                        Debug.Log("Diminution appliquée.");
                    }

                    if (boutonPositif != null && elementTouche.collider.gameObject == boutonPositif)
                    {
                        composanteActuelle?.Augmentation();
                        Debug.Log("Augmentation appliquée.");
                    }

                    TextMeshPro texte = changeurActuel.transform.Find("Texte")?.GetComponent<TextMeshPro>();
                    if (composanteActuelle != null)
                    {
                        texte.text = composanteActuelle.TexteValeur();
                    }
                }
            }
        }

        // Instantiation du changeur lorsqu'on appuie sur la touche I
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Physics.Raycast(souris, out elementTouche))
            {
                Debug.Log("Raycast touche: " + elementTouche.collider.gameObject.name);

                // Si le raycast touche le GameObject actuel
                if (elementTouche.collider.gameObject == gameObject)
                {
                    // Détruire l'ancien changeur s'il existe pour cet objet
                    if (changeurActuel != null)
                    {
                        Destroy(changeurActuel);
                    }

                    // Récupérer la composante du circuit
                    composanteActuelle = GetComponent<ComposanteDuCircuit>();
                    if (composanteActuelle == null)
                    {
                        Debug.LogError("ComposanteDuCircuit manquante sur l'objet: " + gameObject.name);
                        return;
                    }

                    // Définir la position d'apparition du changeur
                    Vector3 positionDapparition = transform.position + new Vector3(0, 0, 0);

                    // Créer et instancier un nouveau changeur
                    changeurActuel = Instantiate(changeur, positionDapparition, Quaternion.identity);

                    // Mettre à jour les références des boutons
                    boutonPositif = changeurActuel.transform.Find("BoutonPositif")?.gameObject;
                    boutonNegatif = changeurActuel.transform.Find("BoutonNegatif")?.gameObject;

                    if (boutonPositif == null || boutonNegatif == null)
                    {
                        Debug.LogError("Boutons manquants dans le prefab BlocInfos");
                    }

                    // Mettre à jour le texte immédiatement
                    TextMeshPro texte = changeurActuel.GetComponentInChildren<TextMeshPro>();
                    texte.text = composanteActuelle.TexteValeur();

                    Debug.Log("Changeur créé pour: " + gameObject.name);
                }
            }
        }
    }

    void OnDestroy()
    {
        // Détruire le changeur associé quand cet objet est détruit+
        if (changeurActuel != null)
        {
            Destroy(changeurActuel);
        }
    }
}