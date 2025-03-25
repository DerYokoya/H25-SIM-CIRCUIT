using System;
using UnityEngine;

public class Resistance : ComposanteDuCircuit
{
    public double ValeurResistance { get; private set; } /*Si nous appelons ceci Resistance, il y aura une erreur
                                                            car la classe s'appele déja Resistance*/

    private static readonly string[] Couleurs = //En mode lecture seule
        { "Noir", "Marron", "Rouge", "Orange", "Jaune", "Vert", "Bleu", "Violet", "Gris", "Blanc" };

    public override void Augmentation() => AjusterIntensiteMax(3);

    public override void Diminution() => AjusterIntensiteMax(-3);

    public void AjusterIntensiteMax(int quantite)
    {
        ValeurResistance = Math.Clamp(ValeurResistance + quantite, 1, 100); // Minimum 1, maximum 100
    }

    public double GetResistance()
    {
        return ValeurResistance;
    }

    public void SetResistance(double resistance)
    {
        ValeurResistance = resistance;
      //DeterminerCouleurs(resistance);
    }

    public override string TexteValeur()
    {
        return ValeurResistance + "'\u03A9'"; // '\u03A9' est le symbole des ohms
    }

    /*public string[] DeterminerCouleurs(double resistance) {

      }*/

}