using UnityEngine;

public class TournerLevierInterrupteur : MonoBehaviour
{
    public Interrupteur interrupteur;

    void OnMouseDown()
    {
        tournerInterrupteur();
    }

    private void tournerInterrupteur()
    {

        if (!interrupteur.getEstOuvert())
        {
            // Change position et rotation du levier (valeurs d’exemple)
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            interrupteur.OuvrirOuFermer();
            interrupteur.valeurResistance = float.MaxValue;
        }
        else
        {
            transform.localPosition = new Vector3(-21.8f, 51.95175f, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            interrupteur.OuvrirOuFermer();
            interrupteur.valeurResistance = 0;
        }
    }
}
