using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * petite classe qui gêre la musique calme du menu.
 * 
 */
public class GestionMusique : MonoBehaviour
{
    public AudioSource audioSource;

    private float timer = 0.0f;

    private ArrayList musiques = new ArrayList();
    private int index;



    /**
     * Initialisation. Création de la compilation des musiques plus premier choix aléatoire de la musique
     * 
     */
    void Start()
    {
        index = (new System.Random()).Next(0, 5);

        musiques = new ArrayList()
                    {
                        Resources.Load<AudioClip>("Sons/Musique/Azure1"),
                        Resources.Load<AudioClip>("Sons/Musique/Azure2"),
                        Resources.Load<AudioClip>("Sons/Musique/Azure3"),
                        Resources.Load<AudioClip>("Sons/Musique/Azure4"),
                        Resources.Load<AudioClip>("Sons/Musique/Azure5"),
                    };

        audioSource.clip = (AudioClip) musiques[index];
        audioSource.Play();

    }

    /*
     * La liste de la musique joue en boucle. Intervalle de 20 secondes entre les morceaux. Choix aléatoire.
     * 
     */
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= audioSource.clip.length + 20)
        {
            audioSource.clip = (AudioClip)musiques[(new System.Random()).Next(0,5)];
            audioSource.Play();
            timer = 0;
        }
    }
}
