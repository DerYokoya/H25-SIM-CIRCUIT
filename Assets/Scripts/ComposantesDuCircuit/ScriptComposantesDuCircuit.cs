using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComposanteDuCircuit : MonoBehaviour
{
    public bool fonctionne { get; set; } = true; //Initialisé à vrai

    public void ChangerEtat()
    {
        fonctionne = !fonctionne; /* S'il y a une surchauffe par exemple, la batterie ne fonctionnera plus,
                                   donc nous changeront l'état. */
    }
}