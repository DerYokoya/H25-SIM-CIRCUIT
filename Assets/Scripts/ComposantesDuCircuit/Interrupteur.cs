using UnityEngine;

public class Interrupteur : Resistance
{
    private bool estOuvert = false;

    public void OuvrirOuFermer()
    {
        estOuvert = !estOuvert;
        Debug.Log(estOuvert);

    }
    public bool getEstOuvert()
    {
        return estOuvert;
    }
}