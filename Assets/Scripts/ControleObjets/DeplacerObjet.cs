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

    private void OnMouseDown()
    {
        positionSouris = Input.mousePosition - GetPositionSouris();

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
                Bounds bounds = solCollider.bounds;

                // Taille de l'objet qu'on déplace (pour éviter qu'il dépasse avec son bord)
                Renderer rend = GetComponent<Renderer>();
                Vector3 objetExtent = rend != null ? rend.bounds.extents : Vector3.zero;

                // Clamp avec marges
                float clampX = Mathf.Clamp(positionMonde.x, bounds.min.x + objetExtent.x, bounds.max.x - objetExtent.x);
                float clampZ = Mathf.Clamp(positionMonde.z, bounds.min.z + objetExtent.z, bounds.max.z - objetExtent.z);

                Vector3 nouvellePosition = new Vector3(clampX, yConstant, clampZ);
                transform.position = nouvellePosition;

                Rotation();
            }
        }
    }

    public void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
        }
    }
}