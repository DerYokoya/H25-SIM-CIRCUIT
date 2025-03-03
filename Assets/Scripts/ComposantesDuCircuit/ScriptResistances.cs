using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistance : ComposanteDuCircuit
{
    public double ValeurResistance { get; private set; } /*Si nous appelons ceci Resistance, il y aura une erreur
                                                            car la classe s'appele déja Resistance*/

    private static readonly string[] Couleurs = //En mode lecture seule
        { "Noir", "Marron", "Rouge", "Orange", "Jaune", "Vert", "Bleu", "Violet", "Gris", "Blanc" };

    public double GetResistance()
    {
        return ValeurResistance;
    }

    public void SetResistance(double resistance)
    {
        ValeurResistance = resistance;
      //DeterminerCouleurs(resistance);
    }

  /*public string[] DeterminerCouleurs(double resistance) {

    }*/

}