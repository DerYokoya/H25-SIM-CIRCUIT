using System.Collections.Generic;
using UnityEngine;
public class GrapheCircuit
{
    private Dictionary<ComposanteDuCircuit, List<ComposanteDuCircuit>> connexions = new();

    public void AjouterLien(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        if (!connexions.ContainsKey(a)) connexions[a] = new List<ComposanteDuCircuit>();
        if (!connexions.ContainsKey(b)) connexions[b] = new List<ComposanteDuCircuit>();

        if (!connexions[a].Contains(b)) connexions[a].Add(b);
        if (!connexions[b].Contains(a)) connexions[b].Add(a);
    }

    public void SupprimerComposant(ComposanteDuCircuit composant)
    {
        if (!connexions.ContainsKey(composant)) return;

        foreach (var voisin in connexions[composant])
        {
            connexions[voisin].Remove(composant);
        }

        connexions.Remove(composant);
    }

    public void RetirerLien(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        if (connexions.ContainsKey(a))
            connexions[a].Remove(b);
        if (connexions.ContainsKey(b))
            connexions[b].Remove(a);
    }

    public List<ComposanteDuCircuit> ObtenirVoisins(ComposanteDuCircuit composante)
    {
        return connexions.TryGetValue(composante, out var voisins) ? voisins : new List<ComposanteDuCircuit>();
    }

    public IEnumerable<ComposanteDuCircuit> ObtenirTousLesComposants()
    {
        return connexions.Keys;
    }

    // Calcule la différence de potentiel (ΔV) entre deux composantes données

    public float? CalculerCourantMaille(ComposanteDuCircuit source)
    {
        var dejaVisites = new HashSet<ComposanteDuCircuit>();
        float tensionTotale = 0f;
        float resistanceTotale = 0f;

        DFS_AccumulerTensionEtResistance(source, dejaVisites, ref tensionTotale, ref resistanceTotale);

        if (resistanceTotale > 0f)
            return tensionTotale / resistanceTotale;

        return null;
    }

    private void DFS_AccumulerTensionEtResistance(ComposanteDuCircuit courant, HashSet<ComposanteDuCircuit> visites, ref float tensionTotale, ref float resistanceTotale)
    {
        visites.Add(courant);

        if (courant is Pile pile)
            tensionTotale += (float)pile.GetTension();
        else if (courant is Resistance resistance)
            resistanceTotale += (float)resistance.GetResistance();

        foreach (var voisin in ObtenirVoisins(courant))
        {
            if (!visites.Contains(voisin))
                DFS_AccumulerTensionEtResistance(voisin, visites, ref tensionTotale, ref resistanceTotale);
        }
    }

    public float? CalculerDeltaV(ComposanteDuCircuit debut, ComposanteDuCircuit fin)
    {
        var dejaVisites = new HashSet<ComposanteDuCircuit>();
        var trace = new List<string>(); // pour stocker les logs intermédiaires
        var resultat = DFS_DeltaV(debut, fin, 0f, dejaVisites, trace);

        // Afficher chaque étape du ΔV
        if (resultat.HasValue)
        {
            Debug.Log("===== DÉTAIL DU CHEMIN ΔV =====");
            foreach (var ligne in trace)
            {
                Debug.Log(ligne);
            }
            Debug.Log("ΔV total = " + resultat.Value + " V");
            Debug.Log("===============================");
        }
        else
        {
            Debug.Log("Aucun chemin trouvé entre les deux composantes.");
        }

        return resultat;
    }


    private float? DFS_DeltaV(ComposanteDuCircuit courant, ComposanteDuCircuit cible, float deltaV, HashSet<ComposanteDuCircuit> dejaVisites,
    List<string> trace)
    {
        if (courant == cible)
            return deltaV;

        dejaVisites.Add(courant);

        foreach (var voisin in ObtenirVoisins(courant))
        {
            if (dejaVisites.Contains(voisin)) continue;

            float contribution = ObtenirContributionDeltaV(courant, voisin);
            trace.Add($"{courant.name} -> {voisin.name} : ΔV = {contribution} V");

            var copieTrace = new List<string>(trace); // copie pour chaque branche
            var resultat = DFS_DeltaV(voisin, cible, deltaV + contribution, dejaVisites, copieTrace);

            if (resultat.HasValue)
            {
                // Remplacer trace principale par la bonne branche
                trace.Clear();
                trace.AddRange(copieTrace);
                return resultat;
            }

            // sinon, cette branche est mauvaise → on annule le log ajouté
            trace.RemoveAt(trace.Count - 1);
        }

        return null;
    }


    // Retourne la variation de tension entre deux composantes connectées
    private float ObtenirContributionDeltaV(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        float courant = CalculerCourantMaille(a) ?? 0f;

        if (a is Pile pileA) return (float)pileA.GetTension();
        if (b is Pile pileB) return (float)-pileB.GetTension(); // si on traverse "à l'envers"

        if (a is Resistance resA) return -(float)(resA.GetResistance() * courant);
        if (b is Resistance resB) return -(float)(resB.GetResistance() * courant);

        return 0f;
    }


}