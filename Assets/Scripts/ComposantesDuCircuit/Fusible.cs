using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Fusible : ComposanteDuCircuit
{
    public GameObject groupeFusible;


    private Material couleurNormale;
    private Material couleurBrule;

    private bool estBrule = false;

    public float IntensiteMax = 4f;

    public GameObject fil1;                // Première moitié du fil
    public GameObject fil2;                // Deuxième moitié du fil

    public AudioSource sourceAudio;        // Source audio à laquelle on joue le son
    public AudioClip sonClac;              // Le son du "clac"

    public float deplacementFils = 0.09f;

    private void Start()
    {

        couleurNormale = Resources.Load<Material>("Couleurs/couleurGrisePale");
        couleurBrule = Resources.Load<Material>("Couleurs/couleurGrise");

        sourceAudio = gameObject.AddComponent<AudioSource>();

        // Valeur initiale si non définie ailleurs
        if (IntensiteMax <= 0)
            IntensiteMax = 10;
    }

    public override void Augmentation() => AjusterIntensiteMax(3);

    public override void Diminution() => AjusterIntensiteMax(-3);

    /* Juste pour débugger
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            BrulerFusible();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ReparerFusible();
        }
    }
    */

    private void AjusterIntensiteMax(int quantite)
    {
        IntensiteMax = Math.Clamp(IntensiteMax + quantite, 1, 20);
    }

    public double GetIntensiteCourantMax() => IntensiteMax;

    public void SetIntensiteCourantMax(float intensiteMax)
    {
        IntensiteMax = intensiteMax;
    }

    public void VerifierIntensite(float intensite)
    {
        if (intensite > IntensiteMax)
        {
            BrulerFusible();
        }
    }

    public void BrulerFusible()
    {
        if (estBrule) return;
        estBrule = true;

        foreach (Transform enfant in groupeFusible.transform)
        {
            if (enfant.name.Contains("Fil")) continue; // Ignore tout ce qui s'appelle "Fil1", "Fil2", etc.
            if (enfant.name.Equals("mmGroup11")) continue; /* Ignore la pièce centrale. Dans un code future, elle pourrait 
            changer de couleur */

            Renderer rend = enfant.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = couleurBrule;
            }
        }

        fil1.transform.position += new Vector3(0, deplacementFils, 0); // déplacement vers le haut
        fil2.transform.position += new Vector3(0, -deplacementFils, 0); // déplacement vers le bas

        if (sourceAudio != null && sonClac != null)
            sourceAudio.PlayOneShot(sonClac);
    }


    public void ReparerFusible()
    {
        if (!estBrule) return;
        estBrule = false;

        // Restaurer la couleur normale sur tous les enfants (sauf les fils)
        foreach (Transform enfant in groupeFusible.transform)
        {
            if (enfant.name.Contains("Fil")) continue; // Ne pas toucher aux fils
            if (enfant.name.Equals("mmGroup11")) continue; // Ignore la pièce centrale

            Renderer rend = enfant.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = couleurNormale;
            }
        }

        fil1.transform.position += new Vector3(0, -deplacementFils, 0); // déplacement vers le bas
        fil2.transform.position += new Vector3(0, deplacementFils, 0); // déplacement vers le haut

    }


    public override string TexteValeur()
    {
        return "Maximum : " + IntensiteMax + " A";
    }
}
