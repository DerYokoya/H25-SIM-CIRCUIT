using UnityEngine;

public class SupprimerObjet : MonoBehaviour
{
    public Camera camera;

    private static AudioSource sourceAudio;
    private static AudioClip suppressionAudio;

    void Start()
    {
        if (suppressionAudio == null)
        {
            suppressionAudio = Resources.Load<AudioClip>("Sons/EffetsSonores/EffetSuppression");
            if (suppressionAudio == null)
                Debug.LogWarning("Le son de suppression est introuvable.");
        }

        if (camera == null)
        {
            camera = GameObject.Find("Camera").GetComponent<Camera>();
        }

        if (sourceAudio == null)
        {
            sourceAudio = camera.GetComponent<AudioSource>();
            if (sourceAudio == null)
                sourceAudio = camera.gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.X))
        {
            // Le raycast vérifie si la souris est sur cet objet
            Ray souris = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit elementTouche;

            if (Physics.Raycast(souris, out elementTouche))
            {
                if (elementTouche.collider.gameObject == gameObject)
                {
                    sourceAudio.PlayOneShot(suppressionAudio);

                    // Retirer du graphe si c'est un composant
                    ComposanteDuCircuit composant = GetComponent<ComposanteDuCircuit>();
                    if (composant != null && GrapheManager.Instance != null)
                    {
                        GrapheManager.Instance.Graphe.SupprimerComposant(composant);
                    }

                    Destroy(gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            sourceAudio.PlayOneShot(suppressionAudio);

            GameObject changeurExiste = GameObject.Find("BlocInfos(Clone)");
            if (changeurExiste != null)
            {
                Destroy(changeurExiste.gameObject);
            }

            // Trouver tous les objets dans la scène qui ont la composante «SupprimerObjet»
            SupprimerObjet[] objetsASupprimer = FindObjectsOfType<SupprimerObjet>();

            foreach (SupprimerObjet objet in objetsASupprimer)
            {
                // Vérifier si l'objet est dans la scène (comme ça les péfabs ne seront pas supprimés)
                if (objet.gameObject.scene.IsValid())
                {
                    ComposanteDuCircuit composant = objet.GetComponent<ComposanteDuCircuit>();
                    if (composant != null && GrapheManager.Instance != null)
                    {
                        GrapheManager.Instance.Graphe.SupprimerComposant(composant);
                    }

                    Destroy(objet.gameObject);
                }
            }
        }
    }
}