using UnityEngine;
using UnityEngine.UI;

/**
 * Petite classe qui gêre l'animation de la grille au menu.
 */
public class Animation : MonoBehaviour
{
    public Image image;
    public float speedX;
    public float speedY;
    public Vector2 debutPosition;

    private RectTransform rectTransform;
    private Vector2 screenSize;


    /*
     * récupération de la résolution de l'écran du client.
     */
    void Start()
    {
        rectTransform = image.GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    /**
     * Effet <<infini>> de l'animation en déplacant une des trois grilles vers la zone morte de l'écran en haut vers la gauche.
     */
    void Update()
    {
        // Déplacement en diagonale
        rectTransform.anchoredPosition += new Vector2(speedX, speedY) * Time.deltaTime;

        // Vérifier si le segment de la grille dépasse les limites de l'écran vers le bas à droite
        Vector2 pos = rectTransform.anchoredPosition;
        if (pos.x > screenSize.x || pos.y < -screenSize.y)
        {
            //Remettre à la position initiale en haut à gauche
            rectTransform.anchoredPosition = debutPosition;
        }
    }
}