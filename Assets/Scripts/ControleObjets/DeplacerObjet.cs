using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    public Camera camera;
    public float yConstant;
    private bool peutDeplacer = false;

    void Start()
    {
        yConstant = transform.position.y;
        if (camera == null)
            camera = Camera.main;
    }

    void Update()
    {
        // Souris cliquée -> vérifie si on clique sur une hitbox enfant
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Composante") && hit.collider.transform.IsChildOf(transform))
                    peutDeplacer = true;
            }
        }

        // Si on garde le clic appuyé, on déplace
        if (peutDeplacer && Input.GetMouseButton(0))
        {
            Deplacer();
        }

        // Quand on relâche la souris, on arrête de déplacer
        if (Input.GetMouseButtonUp(0))
        {
            peutDeplacer = false;
        }
    }

    void Deplacer()
    {
        Vector3 posSouris = Input.mousePosition;
        float profondeur = camera.WorldToScreenPoint(transform.position).z;
        Vector3 positionMonde = camera.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, profondeur));

        GameObject sol = GameObject.FindGameObjectWithTag("Ground");
        if (sol != null)
        {
            BoxCollider solCollider = sol.GetComponent<BoxCollider>();
            if (solCollider != null)
            {
                Bounds limites = solCollider.bounds;

                Renderer rend = GetComponent<Renderer>();
                Vector3 objetExtent = rend != null ? rend.bounds.extents : Vector3.zero;

                float xLimite = Mathf.Clamp(positionMonde.x, limites.min.x + objetExtent.x, limites.max.x - objetExtent.x);
                float zLimite = Mathf.Clamp(positionMonde.z, limites.min.z + objetExtent.z, limites.max.z - objetExtent.z);

                transform.position = new Vector3(xLimite, yConstant, zLimite);
            }
        }
    }
}