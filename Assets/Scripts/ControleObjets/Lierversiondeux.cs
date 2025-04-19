using UnityEngine;

public class AttacheManager : MonoBehaviour
{
    public Transform attache1;
    public Transform attache2;

    private bool estAttache = false;
    private Transform prefabAttache;

    void Start()
    {
        if (attache1 == null) attache1 = transform.Find("attache1");
        if (attache2 == null) attache2 = transform.Find("attache2");

        if (attache1 != null && attache2 != null)
        {
            attache1.gameObject.tag = "Attache";
            attache2.gameObject.tag = "Attache";
            Debug.Log($"{name} : Attaches initialisées.");
        }
        else
        {
            Debug.LogWarning($"{name} : Les attaches ne sont pas correctement assignées !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estAttache) return;

        if (other.CompareTag("Attache"))
        {
            Transform autreAttache = other.transform;
            Transform autrePrefab = autreAttache.parent;

            Debug.Log($"{name} : Collision détectée avec {autreAttache.name} de {autrePrefab.name}");

            Vector3 decalage = attache1.position - transform.position;
            transform.position = autreAttache.position - decalage;

            // Alignement optionnel de la rotation
            transform.rotation = autrePrefab.rotation;

            prefabAttache = autrePrefab;
            estAttache = true;

            Debug.Log($"{name} : Attaché à {prefabAttache.name} via {autreAttache.name}");
        }
    }

    private void Update()
    {
        if (estAttache && Input.GetMouseButtonDown(0)) // Click gauche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == attache1 || hit.transform == attache2)
                {
                    Debug.Log($"{name} : Click sur la jonction ({hit.transform.name})");
                    Detacher();
                }
                else
                {
                    Debug.Log($"{name} : Click sur {hit.transform.name} (pas une jonction)");
                }
            }
            else
            {
                Debug.Log($"{name} : Click sans toucher d'objet.");
            }
        }
    }

    private void Detacher()
    {
        Debug.Log($"{name} : Détaché de {prefabAttache.name}");
        estAttache = false;
        prefabAttache = null;
    }
}