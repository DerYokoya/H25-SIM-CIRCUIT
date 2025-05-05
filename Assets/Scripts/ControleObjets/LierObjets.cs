using Unity.VisualScripting;
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
        // 1. Compute offset between this attache and its parent
        Vector3 offset = parentObject.position - transform.position;

        // 2. Snap parent to make this attache match the other attache
        Vector3 targetPosition = otherAttach.position + offset;
        parentObject.position = targetPosition;

        // Optional: Align rotation
        // parentObject.rotation = otherAttach.rotation;
        //Debug.Log("cooler");

        ComposanteDuCircuit composanteA = parentObject.GetComponent<ComposanteDuCircuit>();
        ComposanteDuCircuit composanteB = otherParent.GetComponent<ComposanteDuCircuit>();

        if (composanteA != null && composanteB != null)
        {
            composanteA.connecte = true;
            composanteB.connecte = true;

            composanteA.Connecter(composanteB); // Ajoute dans les voisins
            //Debug.Log($"Connecté {composanteA.name} avec {composanteB.name}");

            // Recalculer le circuit circuit
            ResoudreCircuit gestionnaire = FindObjectOfType<ResoudreCircuit>();
            if (gestionnaire != null)
                gestionnaire.ForcerRecalcul();

        }

    }
}