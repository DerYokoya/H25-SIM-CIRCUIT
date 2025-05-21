using System;
using UnityEngine;

public class Pile : ComposanteDuCircuit
{
    public float Tension = 9;
    private bool Surchauffee = false;

    public ParticleSystem effetSurchauffe; // Le feu
    private bool aJoueSonSurchauffe = false;

    public AudioSource sourceAudio;        // Source audio à laquelle on joue le son
    public AudioClip sonSurchauffe;

    public Attache attachePlus; // Assigné dans l'éditeur
    public Attache attacheMinus; // Assigné dans l'éditeur

    public bool IsPositiveNode(ConnectionNode node)
        => attachePlus.currentConnectionNode == node;

public void Start()
    {
        sourceAudio = gameObject.AddComponent<AudioSource>();

    }
    /* Pour débugger

public void Update()
    {
        PileSurchauffee();
    }

    public bool EstSurchauffee()
    {
        return Surchauffee;
    }

    public void PileSurchauffeeMiseAJour()
    {
        if (Surchauffee)
        {
            // Rejouer si le son est terminé
            if (sourceAudio != null && sonSurchauffe != null)
            {
                if (!sourceAudio.isPlaying)
                {
                    sourceAudio.PlayOneShot(sonSurchauffe);
                    aJoueSonSurchauffe = true;
                }
            }

            if (effetSurchauffe != null && !effetSurchauffe.isPlaying)
                effetSurchauffe.Play();
        }
        else
        {

            // Arrêter la boucle du son qui joue
            aJoueSonSurchauffe = false;

            if (sourceAudio != null && sourceAudio.isPlaying)
            {
                sourceAudio.Stop();
            }

            if (effetSurchauffe != null) { 
                effetSurchauffe.Clear();
            }

        }
    }

    public override void Augmentation() => AjusterTension(5);

    public override void Diminution() => AjusterTension(-5);

    private void AjusterTension(int quantite)
    {
        Tension = Math.Clamp(Tension + quantite, 1, 120); // Minimum 1, maximum 120
    }

    public float GetVoltage() => Tension;

    public override string TexteValeur()
    {
        return Tension + " V";
    }
}
