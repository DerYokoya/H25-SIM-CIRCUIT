using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleExtension : MonoBehaviour
{
    public GameObject rectangle; // The brown rectangle
    private Vector3 startMousePos; // The position where the mouse first clicked
    private Vector3 startRectPos;  // The position of the rectangle at the start
    private bool isDrawing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rectangle == null)
        {
            Debug.LogError("No rectangle GameObject assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Record the initial mouse position and rectangle position when mouse is clicked
            startMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            startRectPos = rectangle.transform.position;
            isDrawing = true;
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            // Calculate the current mouse position
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            // Calculate the new size based on the mouse distance
            Vector3 size = currentMousePos - startMousePos;

            // Update the scale of the rectangle based on mouse movement
            rectangle.transform.localScale = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), 1);

            // Move the rectangle to its starting position
            rectangle.transform.position = new Vector3(startRectPos.x + Mathf.Min(currentMousePos.x, startMousePos.x),
                                                       startRectPos.y + Mathf.Min(currentMousePos.y, startMousePos.y),
                                                       startRectPos.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Finish drawing the rectangle when the mouse is released
            isDrawing = true;
        }
    }
}
