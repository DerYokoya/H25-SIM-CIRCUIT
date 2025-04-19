using System;
using UnityEngine;

public abstract class ComposanteDuCircuit : MonoBehaviour
{
    public bool fonctionne { get; set; } = false; //Initialisé à faux
    public bool tournerOuPas { get; set; }
    public Camera camera;
    public bool doubleOuPas = false;
    public Ray souris;
    public RaycastHit interrupteurTouche;
    public float angleRotation;

    public void Start()
    {
        GetComponent<Outline>().enabled = false;
    }
    public void ChangerEtat()
    {

        fonctionne = !fonctionne; /* S'il y a une surchauffe par exemple, la batterie ne fonctionnera plus,
                                   donc nous changeront l'état. */
    }

    public void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        doubleOuPas = false;


        if (SourirEstDessu())
        {
            GetComponent<Outline>().enabled = true;

        } else
            {
                GetComponent<Outline>().enabled = false;
            }

    }
    //a regler etait la a cause de la methode rotation à utiliser probablement pour interrupteir
    public void horizotalOuVertical()
    {
        tournerOuPas = !tournerOuPas;
        fonctionne = !tournerOuPas;
    }

    public bool SourirEstDessu()
    {
        Ray souris = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit interrupteurTouche;
        if (Physics.Raycast(souris, out interrupteurTouche))
        {
            if (interrupteurTouche.collider.gameObject == gameObject)
            {
                return true;
            }

        }
        return false;
    }

    public abstract void Augmentation(); // Augmenter une valeur (volts chez la pile, résistance chez la résistance, etc.)

    public abstract void Diminution(); // Diminuer une valeur

    public abstract string TexteValeur(); // Retourner un string qui va dire le nombre plus l'unité (3 ohms, 4 volts, etc.)
}