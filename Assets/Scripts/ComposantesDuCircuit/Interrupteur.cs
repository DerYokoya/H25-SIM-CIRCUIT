using UnityEngine;

public class Interrupteur : Resistance
{
    private bool estOuvert = false;

    private void Start()
    {
    }

    public void OuvrirOuFermer() // Quand l'utilisateur va appuyer sur le levier, cette méthode sera appelée pour l'ouvrir ou le fermer
    {
        estOuvert = !estOuvert;
        Debug.Log(estOuvert);

    }
    public bool getEstOuvert()
    {
        return estOuvert;
    }
}