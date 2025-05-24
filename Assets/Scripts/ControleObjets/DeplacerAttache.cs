using UnityEngine;

/**
 * Classe non inclut dans la présentation en classe qui améliore le deplacement des composants du circuit. Memes approches que PHET.
 * 
 */


public class DeplacerObjetSerpentin : MonoBehaviour
{
    public Camera cam;
    private Transform attacheTiree = null;
    private Vector3 offset;
    private float yConstant;
    private bool estEnTrainDeplacer = false;
    private Bounds limitesSol;



    /**
     * Récupération de préalables, Camera, terrain, constantes ect.
     */
    void Start()
    {
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

    /**
     * 
     * Gestion de la souris, utilisation brute des booléean Input.GetMouseButtonDown et ButtonUp,
     * car elle sont meilleur que les méthode d'Unity OnMouseDown(), OnMouseDrag() et OnMouseUp().
     * Ces trois fonction créer des bug de précisions lors du déplacmeent des composantes.
     */
    void GestionDrag()
    {
        if (Input.GetMouseButtonDown(0) && !estEnTrainDeplacer)
            detecterAttache();

        if (estEnTrainDeplacer && Input.GetMouseButton(0) && attacheTiree != null)
            deplacerComposant();

        if (Input.GetMouseButtonUp(0))
        {
            attacheTiree = null;
            estEnTrainDeplacer = false;
        }
    }

    /**
     * Détection et récupération de l'attache si on le selectione bien comme il faut.
     */
    void detecterAttache()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray))
        {
            if (hit.collider.CompareTag("Attache") && hit.collider.transform.IsChildOf(this.transform))
            {
                attacheTiree = hit.collider.transform;
                offset = attacheTiree.position - GetMouseWorldPosition(yConstant);
                estEnTrainDeplacer = true;
                break;
            }
        }
    }

    /**
     * Déplacement type serpent comme dans PHET.
     * 
     */
    void deplacerComposant()
    {
        Vector3 sourisMonde = GetMouseWorldPosition(yConstant);
        Vector3 targetPos = AppliquerLimites(sourisMonde + offset);

        var filParent = attacheTiree.GetComponentInParent<Fil>();
        if (filParent != null)
        {
            // Déplacer uniquement l'attache selon la souris, borné aux limites
            Vector3 finalPos = targetPos;
            finalPos.y = yConstant;
            attacheTiree.position = finalPos;
        }
        else
        {
            // Déplacer et orienter tout l'objet selon la souris, borné aux limites
            Transform other = attacheTiree.name == "Attache1" ? transform.Find("Attache2") : transform.Find("Attache1");
            Vector3 pivot = other.position;
            Vector3 dirLimitee = targetPos - pivot;

            // Positionner au milieu du segment pivot-target
            Vector3 newPos = pivot + dirLimitee / 2f;
            newPos.y = yConstant;
            transform.position = newPos;

            // Orientation vers la direction de la souris
            Vector3 forward = (attacheTiree.name == "Attache1") ? dirLimitee.normalized : -dirLimitee.normalized;
            if (forward != Vector3.zero)
                transform.forward = forward;
        }
    }

    /**
     * récuperation de la position que la souris vise à travers l'écran.
     */
    Vector3 GetMouseWorldPosition(float y)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        plane.Raycast(ray, out float dist);
        return ray.GetPoint(dist);
    }

    /**
     * Limites déplacement des composants pour pas dépasser la platine de prototypage, le terrain gris.
     * 
     */
    Vector3 AppliquerLimites(Vector3 pos)
    {
        Renderer rend = GetComponent<Renderer>();
        Vector3 ext = rend != null ? rend.bounds.extents : Vector3.zero;
        return new Vector3(
            Mathf.Clamp(pos.x, limitesSol.min.x + ext.x, limitesSol.max.x - ext.x),
            pos.y,
            Mathf.Clamp(pos.z, limitesSol.min.z + ext.z, limitesSol.max.z - ext.z)
        );
    }
}
