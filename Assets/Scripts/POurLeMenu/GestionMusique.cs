using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class GestionMusique : MonoBehaviour
{
    public AudioSource audioSource;

    private float timer = 0.0f;

    private ArrayList musiques = new ArrayList();
    private int index;


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
