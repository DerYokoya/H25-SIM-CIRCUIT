using UnityEngine;

public class Attache : MonoBehaviour
{
    public ComposanteDuCircuit composantParent;

    public string otherAttachTag = "Attache";


    void Start()
    {
        // Initialisation de la couleur pour le debug
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Snap des objets parents
        Vector3 offset = composantParent.transform.position - transform.position;
        composantParent.transform.position = other.transform.position + offset;
    }
}