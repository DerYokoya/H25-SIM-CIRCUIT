using System;
using UnityEngine;

/* Classe statique pour gérer les couleurs des bandes des résistances électroniques.
   Elle convertit une valeur de résistance en ohms vers les couleurs correspondantes 
   selon le code couleur standard des résistances. */
public static class CouleursResistance
{
    // Matériaux représentant chaque couleur du code couleur des résistances
    public static Material Noir;      // 0
    public static Material Brun;      // 1
    public static Material Rouge;     // 2
    public static Material Orange;    // 3
    public static Material Jaune;     // 4
    public static Material Vert;      // 5
    public static Material Bleu;      // 6
    public static Material Mauve;     // 7
    public static Material Gris;      // 8
    public static Material Blanc;     // 9
    public static Material Or;        // Multiplicateur 0.1
    public static Material Argent;    // Multiplicateur 0.01
    public static Material Erreur;    // Couleur d'erreur quand la valeur est invalide

    // Tableau des couleurs organisées par index (0-9)
    private static Material[] CodeMateriaux => new Material[]
    {
        Noir, Brun, Rouge, Orange, Jaune,
        Vert, Bleu, Mauve, Gris, Blanc
    };

    /* Convertit une valeur de résistance en ohms en un tableau de 3 matériaux
       représentant les couleurs des bandes de la résistance.
       Format: [1er chiffre, 2ème chiffre, multiplicateur] */
    public static Material[] GetBandesCouleurs(double valeurResistance)
    {
        if (valeurResistance <= 0)
        {
            return new Material[] { Noir, Noir, Noir };
        }

        // Calcul de l'exposant (puissance de 10) de la valeur
        int exposant = (int)Math.Floor(Math.Log10(valeurResistance));

        // Calcul de la mantisse (partie significative normalisée)
        double mantisse = valeurResistance / Math.Pow(10, exposant);

        // Conversion en chiffres significatifs
        int chiffresSignificatifs = (int)Math.Round(mantisse * 10);

        // Ajustement si on dépasse 99 (ex: 100 devient 10 avec exposant+1)
        if (chiffresSignificatifs >= 100)
        {
            chiffresSignificatifs /= 10;
            exposant++;
        }

        // Extraction des deux chiffres significatifs
        int chiffre1 = chiffresSignificatifs / 10;  // Dizaines
        int chiffre2 = chiffresSignificatifs % 10;  // Unités

        // Le multiplicateur correspond à l'exposant - 1
        // (car on utilise déjà 2 chiffres significatifs)
        int ExposantMultiple = exposant - 1;

        // Conversion des chiffres en couleurs
        Material bande1 = ObtenirCouleurParChiffre(chiffre1);
        Material bande2 = ObtenirCouleurParChiffre(chiffre2);
        Material multiple = GetCouleurMultiple(ExposantMultiple);

        return new Material[] { bande1, bande2, multiple };
    }

    // Retourne le matériau correspondant à un chiffre (0-9)
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
            case -2: return Argent;  // ×0.01
            case -1: return Or;      // ×0.1
            default:

                if (exposant >= 0 && exposant < CodeMateriaux.Length)
                    return CodeMateriaux[exposant];
                return Erreur;
        }
    }
}