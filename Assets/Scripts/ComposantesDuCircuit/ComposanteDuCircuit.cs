using UnityEngine;

    public abstract class ComposanteDuCircuit : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    public abstract void Augmentation(); // Augmenter une valeur (volts chez la pile, résistance chez la résistance, etc.)

    public abstract void Diminution(); // Diminuer une valeur

    public abstract string TexteValeur(); // Retourner un string qui va dire le nombre plus l'unité (3 ohms, 4 volts, etc.)
}