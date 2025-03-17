using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    public Camera camera;
    Vector3 positionSouris;
    float yConstant; // Positon de y fixe est initialisee, mais pas encore declaree

    private void Start()
    {
        yConstant = transform.position.y; // Position de y sera fixe
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private Vector3 GetPositionSouris()
    {
        return camera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        positionSouris = Input.mousePosition - GetPositionSouris();
    }

    private void OnMouseDrag()
    {
        Vector3 posSouris = Input.mousePosition;

        float depth = GetPositionSouris().z;

        Vector3 positionMonde = camera.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, depth));

        Vector3 nouvellePosition = new Vector3(positionMonde.x, yConstant, positionMonde.z);

        transform.position = nouvellePosition;
    }
}