using UnityEngine;

public class CylinderBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float rayon;
    public Material cylinderMaterial;

    private GameObject cylinder;

    private BoxCollider regionDeplacementComplet;

    void Start()
    {
        regionDeplacementComplet = this.GetComponent<BoxCollider>();
        CreateCylinder();
    }

    void Update()
    {
        UpdateCylinder();
    }

    void CreateCylinder()
    {
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.SetParent(this.transform);
        cylinder.GetComponent<Renderer>().material = cylinderMaterial;
        cylinder.AddComponent<Outline>();
        UpdateCylinder();
    }

    void UpdateCylinder()
    {
        if (pointA == null || pointB == null) return;

        Vector3 middlePosition = (pointA.position + pointB.position) / 2f;
        cylinder.transform.position = middlePosition;

        Vector3 direction = pointB.position - pointA.position;
        float distance = direction.magnitude;

        // Cylinder height is distance between points
        cylinder.transform.localScale = new Vector3(rayon, distance / 2, rayon);

        // Rotate to point from A to B
        cylinder.transform.rotation = Quaternion.LookRotation(direction);
        cylinder.transform.Rotate(Vector3.right, 90f);
    }
}