using UnityEngine;

public class AttacheSnapping : MonoBehaviour
{
    [Tooltip("Root of the object this Attache belongs to.")]
    public Transform parentObject;

    [Tooltip("Tag used for other Attache objects.")]
    public string otherAttachTag = "Attache";

    private bool hasSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        // Prevent multiple snaps if only one is allowed
        if (hasSnapped) return;

        // Check tag
        if (!other.CompareTag(otherAttachTag)) return;

        // Get the other attache's root
        Transform otherAttach = other.transform;
        Transform otherParent = otherAttach.root;

        // --- SNAP LOGIC ---
        // 1. Compute offset between this attache and its parent
        Vector3 offset = parentObject.position - transform.position;

        // 2. Snap parent to make this attache match the other attache
        Vector3 targetPosition = otherAttach.position + offset;
        parentObject.position = targetPosition;

        // Optional: Align rotation
        // parentObject.rotation = otherAttach.rotation;

        hasSnapped = true;
        Debug.Log("cooler");
    }
}