using UnityEngine;

public class AttacheSnapping : MonoBehaviour
{
    [Tooltip("Root of the object this Attache belongs to.")]
    public Transform parentObject;

    [Tooltip("Tag used for other Attache objects.")]
    public string otherAttachTag = "Attache";

    public void OnTriggerStay(Collider other)
    {
        // Check tag
        if (!other.CompareTag(otherAttachTag)) return;

        // Get the other attache's root
        Transform otherAttach = other.transform;
        Transform otherParent = otherAttach.root;

        // --- SNAP LOGIC ---
        Vector3 offset = parentObject.position - transform.position;
        Vector3 targetPosition = otherAttach.position + offset;
        parentObject.position = targetPosition;

        ComposanteDuCircuit composanteA = parentObject.GetComponent<ComposanteDuCircuit>();
        ComposanteDuCircuit composanteB = otherParent.GetComponent<ComposanteDuCircuit>();
    }
}