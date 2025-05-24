using UnityEngine;

public class PileDrag : MonoBehaviour
{
    public Camera cam;
    private Transform attacheTiree = null;
    private Vector3 offset;
    private float yConstant;
    private bool estEnTrainDrag = false;
    private Bounds limitesSol;
    public float distanceMax = 5f;

    void Start()
    {
        Debug.Log($"Attache1 trouvée: {transform.Find("Attache1") != null}");
    Debug.Log($"Attache2 trouvée: {transform.Find("Attache2") != null}");
        if (cam == null) cam = Camera.main;
        yConstant = transform.position.y;
        
        GameObject sol = GameObject.FindGameObjectWithTag("Ground");
        if (sol != null)
        {
            BoxCollider solCollider = sol.GetComponent<BoxCollider>();
            if (solCollider != null) limitesSol = solCollider.bounds;
        }
    }

    void Update()
    {
        GestionDrag();
    }

    void GestionDrag()
    {
        if (Input.GetMouseButtonDown(0) && !estEnTrainDrag)
        {
            DetecterAttache();
        }

        if (estEnTrainDrag && Input.GetMouseButton(0) && attacheTiree != null)
        {
            DeplacerPile();
        }

        if (Input.GetMouseButtonUp(0))
        {
            attacheTiree = null;
            estEnTrainDrag = false;
        }
    }

    void DetecterAttache()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray))
        {
            if (hit.collider.CompareTag("Attache") && hit.collider.transform.IsChildOf(this.transform))
            {
                attacheTiree = hit.collider.transform;
                offset = attacheTiree.position - GetMouseWorldPosition(yConstant);
                estEnTrainDrag = true;
                break;
            }
        }
    }

    void DeplacerPile()
    {
        Vector3 targetPosition = GetMouseWorldPosition(yConstant) + offset;
        Transform otherAttache = attacheTiree.name == "Attache1" ? transform.Find("Attache2") : transform.Find("Attache1");
        Vector3 pivot = otherAttache.position;

        // Nouveau: Calcul direction avant application des limites
        Vector3 rawDirection = targetPosition - pivot;
        Vector3 limitedDirection = rawDirection;

        // Application des limites du sol
        Vector3 limitedPosition = AppliquerLimites(targetPosition);
        limitedDirection = limitedPosition - pivot;

        // Limiter la distance maximale (en maintenant la proportion X/Z)
        if (limitedDirection.magnitude > distanceMax)
        {
            // Conservation du ratio directionnel original
            Vector3 normalizedRawDirection = rawDirection.normalized;
            limitedDirection = normalizedRawDirection * distanceMax;
            limitedPosition = pivot + limitedDirection;
            
            // Réappliquer les limites après ajustement
            limitedPosition = AppliquerLimites(limitedPosition);
            limitedDirection = limitedPosition - pivot;
        }

        // Calcul position/rotation finale
        Vector3 newPosition = pivot + limitedDirection / 2f;
        newPosition.y = yConstant;
        
        transform.position = newPosition;
        transform.forward = (attacheTiree.name == "Attache1") ? limitedDirection.normalized : -limitedDirection.normalized;
    }

    Vector3 GetMouseWorldPosition(float y)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }

    Vector3 AppliquerLimites(Vector3 position)
    {
        Renderer rend = GetComponent<Renderer>();
        Vector3 objetExtent = rend != null ? rend.bounds.extents : Vector3.zero;

        return new Vector3(
            Mathf.Clamp(position.x, limitesSol.min.x + objetExtent.x, limitesSol.max.x - objetExtent.x),
            position.y,
            Mathf.Clamp(position.z, limitesSol.min.z + objetExtent.z, limitesSol.max.z - objetExtent.z)
        );
    }
}