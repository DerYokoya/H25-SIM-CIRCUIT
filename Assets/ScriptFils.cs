using UnityEngine;

public RectTransform rectTransform; // Reference to the RectTransform of the UI object

void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        startMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        startRectPos = rectTransform.position;
        isDrawing = true;
    }

    if (isDrawing && Input.GetMouseButton(0))
    {
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        // Set sizeDelta for UI elements (RectTransform)
        rectTransform.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
        rectTransform.position = new Vector3(startRectPos.x + Mathf.Min(currentMousePos.x, startMousePos.x),
                                             startRectPos.y + Mathf.Min(currentMousePos.y, startMousePos.y),
                                             startRectPos.z);
    }

    if (Input.GetMouseButtonUp(0))
    {
        isDrawing = false;

    }
}
