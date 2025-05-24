using System;
using UnityEngine;

public class Pile : ComposanteDuCircuit
{
    public float Tension = 9;
    private bool Surchauffee = false;

    public ParticleSystem effetSurchauffe; // Le feu
    private bool aJoueSonSurchauffe = false;

    public AudioSource sourceAudio1;        // Source audio à laquelle on joue le son
    public AudioSource sourceAudio2;
    public AudioClip sonSurchauffe;
    public AudioClip sonCourtCircuit;

    public Attache attachePlus; // Assigné dans l'éditeur
    public Attache attacheMinus; // Assigné dans l'éditeur

    // Retourne true si le nœud passé correspond au nœud de connexion positif actuel
    public bool IsPositiveNode(ConnectionNode node)
        => attachePlus.currentConnectionNode == node;

public void Start()
    {
        sourceAudio1 = gameObject.AddComponent<AudioSource>();
        sourceAudio2 = gameObject.AddComponent<AudioSource>();

    }

public void Update()
    {
        PileSurchauffee();
    }

    public bool EstSurchauffee()
    {
        return Surchauffee;
    }

    public void setEstSurchauffee(bool surchauffe)
    {
        Surchauffee = surchauffe;
    }


    public void PileSurchauffee()
    {
        if (Surchauffee)
        {
            // Rejouer si le son est terminé
            if (sourceAudio1 != null && sonSurchauffe != null)
            {
                if (!sourceAudio1.isPlaying && !sourceAudio2.isPlaying)
                {
                    sourceAudio1.PlayOneShot(sonSurchauffe);
                    sourceAudio2.PlayOneShot(sonCourtCircuit);
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

            if (sourceAudio1 != null && sourceAudio1.isPlaying && sourceAudio2 != null && sourceAudio2.isPlaying)
            {
                sourceAudio1.Stop();
                sourceAudio2.Stop();
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
        Tension = Math.Clamp(Tension + quantite, 1, 120); // Minimum 1V, maximum 120V
    }

    public float GetVoltage() => Tension;

    public override string TexteValeur()
    {
        return Tension + " V";
    }
}
