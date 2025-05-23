using System;
using UnityEngine;

public class Ampoule : Resistance
{
    public Light lumiere;


    protected override void Update()
    {
    }

    public void ChangementLuminosite(float courant)
    {
        float efficaciteLum = 15f; // en lumens/watt (incandescence typique)
        float puissance = valeurResistance * courant * courant; // P = R * I^2 formule de la puissance (en lumens)
        float lumens = efficaciteLum * puissance;


        lumens = Mathf.Clamp(lumens, 0f, 1600f);


        /* Si le courant est suffisant (> 0.001 A), on allume l'ampoule avec une intensité lumineuse interpolée entre 3 et 5,
           en fonction du rapport lumens/800. Sinon, l'ampoule reste éteinte (intensité lumineuse = 0). */
        lumiere.intensity = Mathf.Abs(base.courant) > 0.001f ? Mathf.Lerp(3f, 5f, lumens / 800f) : 0f;

        /* La portée de la lumière est interpolée entre 2 et 50 unités selon la puissance lumineuse relative (lumens / 800). */
        lumiere.range = Mathf.Lerp(2, 50f, lumens / 800f);
    }
}
