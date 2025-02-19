using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform human;
    public Vector3 decalage = new Vector3(0f, 2f, -5f); // Décalage de la caméra

    void LateUpdate()
    {
        transform.position = human.position;
        transform.LookAt(human);
    }
}