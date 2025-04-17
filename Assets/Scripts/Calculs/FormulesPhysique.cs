using System;
using System.Collections.Generic;
using UnityEngine;

public class Calculs : MonoBehaviour
{
    // Calculer la Résistance (R) avec Tension (U) et Intensité (I)
    // R = U / I
    public static float CalculerResistance(float tension, float intensite)
    {
        if (intensite == 0)
        {
            throw new ArgumentException("L'intensité ne peut pas être zéro.");
        }
        return tension / intensite;
    }

    // Calculer la Tension (U) avec Résistance (R) et Intensité (I)
    // U = R * I
    public static float CalculerTension(float resistance, float intensite)
    {
        return resistance * intensite;
    }

    // Calculer l'Intensité (I) avec Tension (U) et Résistance (R)
    // I = U / R
    public static float CalculerIntensite(float tension, float resistance)
    {
        if (resistance == 0)
        {
            throw new ArgumentException("La résistance ne peut pas être zéro.");
        }
        return tension / resistance;
    }

    // Calculer la Résistance Équivalente pour un Circuit en Série
    public static float CalculerResistanceSerie(List<float> resistances)
    {
        float resistanceTotale = 0;
        foreach (float resistance in resistances)
        {
            resistanceTotale += resistance;
        }
        return resistanceTotale;
    }

    // Calculer la Résistance Équivalente pour un Circuit en Parallèle
    public static float CalculerResistanceParallele(List<float> resistances)
    {
        float sommeReciproque = 0;
        foreach (float resistance in resistances)
        {
            sommeReciproque += 1 / resistance;
        }
        if (sommeReciproque == 0)
        {
            throw new ArgumentException("La somme réciproque totale ne peut pas être zéro.");
        }
        return 1 / sommeReciproque;
    }
}