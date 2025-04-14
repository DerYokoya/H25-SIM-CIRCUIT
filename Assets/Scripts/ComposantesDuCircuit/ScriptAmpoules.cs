using System;
using UnityEngine;

public class Ampoule : Resistance


{
    public double Puissance { get; set; }
    public double Tension { get; set; }
    public double Luminosite { get; set; }

    public void ChangementLuminosite()
    {
    }

    public void CalculPuissance()
    {
        Puissance = Math.Pow(Tension, 2) / ValeurResistance; // Calcul de la puissance, qui va déterminer la luminosité
    }

}
