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
        // Déplacer en diagonale
        rectTransform.anchoredPosition += new Vector2(speedX, speedY) * Time.deltaTime;

        // Vérifier si l'image dépasse les limites de l'écran
        Vector2 pos = rectTransform.anchoredPosition;
        if (pos.x > screenSize.x || pos.y < -screenSize.y)
        {
            // Remettre à la position initiale
            rectTransform.anchoredPosition = debutPosition;
        }
    }
}