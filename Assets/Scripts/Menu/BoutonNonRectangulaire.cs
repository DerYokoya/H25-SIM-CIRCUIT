using UnityEngine;
using UnityEngine.UI;


/*
 *  Petite classe qui gêre l'espace invisible d'un bouton. Rendre la zone <<cliquable>> seulement la zone qu'on 
 *  peut voir a travers le <<sprite>> (la texture du boutton) du bouton example un bouton circulaire ne sera pas 
 *  consideéer pas unity comme un carré.
 */
public class Clickable : MonoBehaviour
{
    public float alphaThreshold = 0.1f;
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

}