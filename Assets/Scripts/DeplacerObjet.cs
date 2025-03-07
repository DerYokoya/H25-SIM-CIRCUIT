using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    Vector3 positionSouris;
    float yConstant; // Positon de y fixe est initialisée, mais pas encore déclarée

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
        Vector3 nouvellePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - positionSouris);
        nouvellePosition.y = yConstant; // Garder la position de y constante
        transform.position = nouvellePosition;
    }
}