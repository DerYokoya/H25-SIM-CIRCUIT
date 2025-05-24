using UnityEngine;




/**
 * Classe qui gêre l'aniamtion de l'interrupteur, so levier.
 * 
 * 
 */
public class TournerLevierInterrupteur : MonoBehaviour
{
    public Interrupteur interrupteur;



    /**
     * L'interrupteur lorsqu'on le crée dans le simulateur, elle est par défaut 
     * ouvert donc sa résistance sera infini.
     */
    private void Start()
    {
        interrupteur.valeurResistance = float.MaxValue;
    }

    void OnMouseDown()
    {
        tournerInterrupteur();
    }


    /**
     * gestion animation, changement de valeur et booléén.
     */
    private void tournerInterrupteur()
    {

        // Si l'interrupteur était ouvert, on mets la résistance a 0 ohm pour faire passé le courant comme un fil,
        // on deplace le model 3d du levier vers le coter <<on>> et on met vrai son booléen <<estOuvert>>.
        if (!interrupteur.getEstOuvert())
        {
            // Change position et rotation du levier (valeurs d�exemple)
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            interrupteur.OuvrirOuFermer();
            interrupteur.valeurResistance = 0;
        }

        //Si il était fermé, on remet la résistance a l'infini pour ne pas faire passé le courant,
        //on redéplace le modèle 3D du levier vers <<off>>.
        else
        {
            transform.localPosition = new Vector3(-21.8f, 51.95175f, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            interrupteur.OuvrirOuFermer();
            interrupteur.valeurResistance = float.MaxValue;
        }
    }
}
