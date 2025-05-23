using UnityEngine;

public class DeplacerFil : MonoBehaviour
{
    public Camera camera;
    Vector3 positionSouris;
    public float yConstant; // Positon de y fixe est initialisee, mais pas encore declaree

    private void Start()
    {
        yConstant = transform.position.y; // Position de y sera fixe
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }


    private void Update()
    {
        
    }

    public Vector3 GetPositionSouris()
    {
        return camera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseEnter() // Si la souris est sur l'objet, mettre les contours de l'objet en surbrillance
    {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit() // Si la souris n'est pas sur l'objet, ne pas mettre les contours de l'objet en surbrillance
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

                // Taille de l'objet qu'on déplace (pour éviter qu'il dépasse avec son bord)
                Renderer rend = GetComponent<Renderer>();
                Vector3 objetExtent = rend != null ? rend.bounds.extents : Vector3.zero;

                // Empêcher l'objet de dépasser la plateforme (en incluant les marges de l'objet)
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