using System;
using UnityEngine;

public abstract class ComposanteDuCircuit : MonoBehaviour
{
    public bool fonctionne { get; set; } = false; //Initialisé à faux
    public bool tournerOuPas { get; set; }
    public Camera camera;
    private float seuilDoubleClic = 0.3f; // Temps max entre deux clics pour que ça compte comme un double clic
    private float dernierMomentDeClic = 0f;
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

            if (Input.GetMouseButtonDown(0))  // 0 pour clic gauche 
            {

                doubleOuPas = true;
                if (Time.time - dernierMomentDeClic < seuilDoubleClic)
                {
                    horizotalOuVertical(); // Ouvrir ou fermer s'il y a un double-clic gauche dessus
                    Rotation();

                }
                dernierMomentDeClic = Time.time;
            }
           

        } else
            {
                GetComponent<Outline>().enabled = false;
            }

    }
    public void horizotalOuVertical()
    {
        tournerOuPas = !tournerOuPas;
        fonctionne = !tournerOuPas;
    }
    public void Rotation()
    {
        float angleRotation = tournerOuPas ? 90f : 0f; // Interrupteur ouvert? Vrai : 45 degrés. Faux : 0 degré.
        transform.rotation = Quaternion.Euler(0f, angleRotation, 0f);

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