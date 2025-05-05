using System.Collections.Generic;
using UnityEngine;

public class ResoudreCircuit : MonoBehaviour
{
    private ComposanteDuCircuit pointDeDepart;

    private HashSet<ComposanteDuCircuit> visites;
    private float tensionTotale;
    private float resistanceTotale;

    public void ForcerRecalcul()
    {
        pointDeDepart = TrouverPile();

        if (pointDeDepart == null || !pointDeDepart.connecte)
        {
            Debug.LogWarning("Aucune pile connectée trouvée. Circuit incomplet.");
            return;
        }

        Debug.Log("Recalcul du circuit...");
        Resoudre();
    }

    void Resoudre()
    {
        visites = new HashSet<ComposanteDuCircuit>();
        tensionTotale = 0f;
        resistanceTotale = 0f;

        CalculerCircuit(pointDeDepart);

        if (resistanceTotale > 0)
        {
            float courant = tensionTotale / resistanceTotale;
            Debug.Log($"Tension: {tensionTotale} V");
            Debug.Log($"Résistance équivalente: {resistanceTotale} Ohms");
            Debug.Log($"Courant total: {courant} A");
        }
        else
        {
            Debug.Log("Circuit ouvert ou court-circuit détecté.");
        }
    }

    void CalculerCircuit(ComposanteDuCircuit composant)
    {
        if (visites.Contains(composant) || !composant.connecte)
            return;

        visites.Add(composant);

        if (composant is Pile pile)
            tensionTotale += (float) pile.GetTension();
        else if (composant is Resistance resistance)
            resistanceTotale += (float)resistance.GetResistance();

        foreach (var voisin in composant.voisins)
            CalculerCircuit(voisin);
    }

    ComposanteDuCircuit TrouverPile()
    {
        Pile[] piles = FindObjectsOfType<Pile>();
        foreach (Pile pile in piles)
        {
            if (pile.connecte)
                return pile;
        }
        return null;
    }
}