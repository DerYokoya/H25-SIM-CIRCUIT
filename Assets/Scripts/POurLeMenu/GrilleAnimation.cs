using UnityEngine;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    public Image image;
    public float speedX;
    public float speedY;
    public Vector2 debutPosition;

    private RectTransform rectTransform;
    private Vector2 screenSize;

    void Start()
    {
        rectTransform = image.GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // D�placer en diagonale
        rectTransform.anchoredPosition += new Vector2(speedX, speedY) * Time.deltaTime;

        // V�rifier si l'image d�passe les limites de l'�cran
        Vector2 pos = rectTransform.anchoredPosition;
        if (pos.x > screenSize.x || pos.y < -screenSize.y)
        {
            // Remettre � la position initiale
            rectTransform.anchoredPosition = debutPosition;
        }
    }
}