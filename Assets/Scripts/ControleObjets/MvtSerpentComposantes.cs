using UnityEngine;

public class PileDrag : MonoBehaviour
{
    public Camera cam;
    private Transform attacheTiree = null;
    private Vector3 offset;
    private float yConstant;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        yConstant = transform.position.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Attache"))
                {
                    attacheTiree = hit.collider.transform;
                    offset = attacheTiree.position - GetMouseWorldPosition(attacheTiree.position.y);
                }
            }
        }

        if (Input.GetMouseButton(0) && attacheTiree != null)
        {
            Vector3 targetPosition = GetMouseWorldPosition(yConstant) + offset;
            Transform otherAttache = attacheTiree.name == "Attache1" ? transform.Find("Attache2") : transform.Find("Attache1");
            Vector3 pivot = otherAttache.position;

            // Calcul de la nouvelle position et rotation
            Vector3 newDirection = (targetPosition - pivot).normalized;
            float length = Vector3.Distance(pivot, targetPosition);
            
            // Position au milieu entre le pivot et la target
            transform.position = pivot + newDirection * (length / 2f);
            
            // Rotation correcte selon l'attache tirée
            if (attacheTiree.name == "Attache1")
            {
                transform.forward = newDirection;
            }
            else
            {
                transform.forward = -newDirection;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            attacheTiree = null;
        }
    }

    Vector3 GetMouseWorldPosition(float y)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}