using UnityEngine;

public class AfficherHitboxSurvol : MonoBehaviour
{
    public Outline outline;  // Drag ton Outline ici si pas auto

    public Camera cam;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (outline == null) outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    void Update()
    {
        if (cam == null || outline == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.IsChildOf(transform)) // souris sur pile ou un de ses enfants
            {
                outline.enabled = true;
                return;
            }
        }

        outline.enabled = false;
    }
}