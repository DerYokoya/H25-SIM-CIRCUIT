using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    Vector3 positionSouris;
    float yConstant; // Positon de y fixe est initialisee, mais pas encore declaree

    private void Start()
    {
        yConstant = transform.position.y; // Position de y sera fixe
    }

    private Vector3 GetPositionSouris()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        positionSouris = Input.mousePosition - GetPositionSouris();
    }

    private void OnMouseDrag()
    {
        Vector3 posSouris = Input.mousePosition;

        Vector3 positionMonde = Camera.main.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, 
            transform.position.z - Camera.main.transform.position.z));

        float deltaY = posSouris.y - (positionSouris.y + GetPositionSouris().y);

        float nouveauZ = transform.position.z + deltaY * 0.01f; // On peut ajuster la sensibilité en mettant une autre valeur que 0.01

        Vector3 nouvellePosition = new Vector3(positionMonde.x, yConstant, nouveauZ);

        transform.position = nouvellePosition;

        positionSouris = posSouris - GetPositionSouris();
    }
}