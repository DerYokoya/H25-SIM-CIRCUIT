using System;
using UnityEngine;

public static class CouleursResistance
{
    public static Material Noir;
    public static Material Brun;
    public static Material Rouge;
    public static Material Orange;
    public static Material Jaune;
    public static Material Vert;
    public static Material Bleu;
    public static Material Mauve;
    public static Material Gris;
    public static Material Blanc;
    public static Material Or;
    public static Material Argent;
    public static Material Erreur;

    private static Material[] CodeMateriaux => new Material[]
    {
        Noir, Brun, Rouge, Orange, Jaune,
        Vert, Bleu, Mauve, Gris, Blanc
    };

    public static Material[] GetBandesCouleurs(double valeurResistance)
    {
        if (valeurResistance <= 0)
        {
            return new Material[] { Noir, Noir, Noir };
        }

        int exposant = (int)Math.Floor(Math.Log10(valeurResistance));
        double mantisse = valeurResistance / Math.Pow(10, exposant); // Mantisse désigne la partie non entière d'un logarithme

        int chiffresSignificatifs = (int)Math.Round(mantisse * 10);

        if (chiffresSignificatifs >= 100)
        {
            chiffresSignificatifs /= 10;
            exposant++;
        }

        int chiffre1 = chiffresSignificatifs / 10;
        int chiffre2 = chiffresSignificatifs % 10;
        int ExposantMultiple = exposant - 1;

        Material bande1 = ObtenirCouleurParChiffre(chiffre1);
        Material bande2 = ObtenirCouleurParChiffre(chiffre2);
        Material multiple = GetCouleurMultiple(ExposantMultiple);

        return new Material[] { bande1, bande2, multiple };
    }

    private static Material ObtenirCouleurParChiffre(int chiffre)
    {
        if (chiffre >= 0 && chiffre < CodeMateriaux.Length)
            return CodeMateriaux[chiffre];
        return Erreur;
    }

    private static Material GetCouleurMultiple(int exposant)
    {
        switch (exposant)
        {
            case -2: return Argent;
            case -1: return Or;
            default:
                if (exposant >= 0 && exposant < CodeMateriaux.Length)
                    return CodeMateriaux[exposant];
                return Erreur;
        }
    }
}
