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
            Vector3 targetPosition = GetMouseWorldPosition(attacheTiree.position.y) + offset;
            Vector3 pivot = attacheTiree == transform.Find("Attache1") ? transform.Find("Attache2").position : transform.Find("Attache1").position;

            Vector3 direction = targetPosition - pivot;
            transform.position = pivot + direction / 2f;
            transform.forward = direction.normalized;
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