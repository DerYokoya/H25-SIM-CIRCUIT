using UnityEngine;



/**
 * En classe durant la présentation, vous aviez mentionné qu'une classe abstraite ne peut pas extend la classe MonoBehaviour mais non.
 * 
 */
    public abstract class ComposanteDuCircuit : MonoBehaviour
{
    public float courant { get; set; }
    public void Start()
    {
        GetComponent<Outline>().enabled = false; // Désactiver le contour de couleur autour de la composante
    }

    public abstract void Augmentation(); // Augmenter une valeur (volts chez la pile, résistance chez la résistance, etc.)

    public abstract void Diminution(); // Diminuer une valeur

    public abstract string TexteValeur(); // Retourner un string qui va dire le nombre plus l'unité (3 ohms, 4 volts, etc.)
}