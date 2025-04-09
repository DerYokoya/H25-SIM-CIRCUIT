using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LierObjets : MonoBehaviour
{
    public float snapDistance = 0.05f;
    public bool autoSnap = true;

    private Collider myCollider;
    private Transform connectedObject;
    private Vector3 snapOffset;
    private bool isSnapped = false;
    private Transform parentToMove;
    private float longueurParent;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        parentToMove = transform.parent; // Move the whole component (e.g., "Pile")
        longueurParent = parentToMove.GetComponentInChildren<Renderer>().bounds.size.x;
    }

    void Update()
    {
        if (!isSnapped && autoSnap)
        {
            TrySnapToNearestComposante();
        }

        if (isSnapped && connectedObject != null)
        {
            parentToMove.position = connectedObject.position + snapOffset + new Vector3(longueurParent/2 , 0 , 0);
        }
    }

    void TrySnapToNearestComposante()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Composante");

        foreach (GameObject candidate in candidates)
        {
            if (candidate == gameObject) continue;

            Collider targetCol = candidate.GetComponent<Collider>();
            if (targetCol == null) continue;

            Vector3 myClosestPoint = myCollider.ClosestPoint(targetCol.transform.position);
            Vector3 targetClosestPoint = targetCol.ClosestPoint(myClosestPoint);
            Vector3 offset = targetClosestPoint - myClosestPoint;

            // We check if they already overlap (distance very small)
            if (offset.magnitude < snapDistance)
            {
                parentToMove.position += offset;
                connectedObject = candidate.transform;
                snapOffset = parentToMove.position - connectedObject.position;
                isSnapped = true;
                return; // break out after the first valid match
            }
        }
    }


    public void ResetSnap()
    {
        isSnapped = false;
        connectedObject = null;
    }
}