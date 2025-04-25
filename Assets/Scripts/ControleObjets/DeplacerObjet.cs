using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    public Camera camera;
    Vector3 positionSouris;
    public float yConstant; // Positon de y fixe est initialisee, mais pas encore declaree

    private void Start()
    {
        yConstant = transform.position.y; // Position de y sera fixe
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    public Vector3 GetPositionSouris()
    {
        return camera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void OnMouseDrag()
    {
        Vector3 posSouris = Input.mousePosition;
        float profondeur = GetPositionSouris().z;
        Vector3 positionMonde = camera.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, profondeur));

        GameObject sol = GameObject.FindGameObjectWithTag("Ground");
        if (sol != null)
        {
            BoxCollider solCollider = sol.GetComponent<BoxCollider>();
            if (solCollider != null)
            {
                Bounds limites = solCollider.bounds;

                // Taille de l'objet qu'on d�place (pour �viter qu'il d�passe avec son bord)
                Renderer rend = GetComponent<Renderer>();
                Vector3 objetExtent = rend != null ? rend.bounds.extents : Vector3.zero;

                // Clamp avec marges
                float xLimite = Mathf.Clamp(positionMonde.x, limites.min.x + objetExtent.x, limites.max.x - objetExtent.x);
                float zLimite = Mathf.Clamp(positionMonde.z, limites.min.z + objetExtent.z, limites.max.z - objetExtent.z);

                Vector3 nouvellePosition = new Vector3(xLimite, yConstant, zLimite);
                transform.position = nouvellePosition;

                Rotation();
            }
        }
    }
    private void OnMouseDown()
    {
        positionSouris = Input.mousePosition - GetPositionSouris();
        GetComponent<Outline>().enabled = false;

    }

    public void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.R) && transform.CompareTag("Composante"))
        {
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
        }
    }
}