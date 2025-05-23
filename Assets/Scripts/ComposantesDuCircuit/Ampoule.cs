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
        float puissance = valeurResistance * courant * courant; // P = R * I*I formule lumens
        float lumens = efficaciteLum * puissance;


        lumens = Mathf.Clamp(lumens, 0f, 1600f);


        lumiere.intensity = Mathf.Abs(base.courant) > 0.001f ? Mathf.Lerp(3f, 5f, lumens / 800f) : 0f;
        lumiere.range = Mathf.Lerp(2, 50f, lumens / 800f);
    }
}
