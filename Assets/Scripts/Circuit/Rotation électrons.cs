using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationélectrons : MonoBehaviour
{
    public Transform sphereTransform;     // Référence à la sphère
    public float sphereRadius = 0.1f;     // Rayon de la sphère (ex: 0.1 pour ton modèle)
    public Vector3 offset = new Vector3(0, 0.02f, 0); // Optionnel : pour éviter que le texte touche la sphère

    void Update()
    {
        if (sphereTransform == null || Camera.main == null)
            return;

        // Placer le texte juste au-dessus de la sphère (au bord)
        Vector3 targetPosition = sphereTransform.position + Vector3.up * sphereRadius + offset;
        transform.position = targetPosition;

        // Faire en sorte que le texte regarde la caméra
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
