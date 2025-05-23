using UnityEngine;

public class PileRigideDrag : MonoBehaviour
{
    public Transform attachePlus;    // Extrémité +
    public Transform attacheMoins;   // Extrémité -
    public Camera cam;

    private Transform attacheActive = null;
    private Transform attacheFixe = null;
    private float longueurInitiale;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        longueurInitiale = Vector3.Distance(attachePlus.position, attacheMoins.position);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Attache"))
                {
                    attacheActive = hit.transform;
                    attacheFixe = (attacheActive == attachePlus) ? attacheMoins : attachePlus;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            attacheActive = null;
            attacheFixe = null;
        }

        if (attacheActive != null && attacheFixe != null)
        {
            Plane plan = new Plane(Vector3.up, attacheFixe.position);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (plan.Raycast(ray, out float distance))
            {
                Vector3 positionSouris = ray.GetPoint(distance);
                Vector3 direction = (positionSouris - attacheFixe.position).normalized;

                // Position du centre de la pile (entre les deux extrémités)
                Vector3 centre = attacheFixe.position + direction * (longueurInitiale / 2f);
                transform.position = centre;

                // Aligner la rotation avec la direction
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}