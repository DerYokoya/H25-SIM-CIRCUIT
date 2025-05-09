using System.Collections.Generic;

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
}