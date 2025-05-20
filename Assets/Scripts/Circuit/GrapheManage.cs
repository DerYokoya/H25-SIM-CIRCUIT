using System.Collections.Generic;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    public static CircuitManager Instance;

    // Dictionnaire : Noeud ID -> Liste d'attaches connectées
    private Dictionary<int, HashSet<Attache>> noeuds = new Dictionary<int, HashSet<Attache>>();
    private int nextNodeId = 0;

    private void Awake() => Instance = this;

    public void ConnectAttaches(Attache a, Attache b)
    {
        int? nodeA = TrouverNoeud(a);
        int? nodeB = TrouverNoeud(b);

        if (!nodeA.HasValue && !nodeB.HasValue)
        {
            CreerNouveauNoeud(a, b);
        }
        else if (nodeA.HasValue != nodeB.HasValue)
        {
            AjouterAUneAttacheExistante(nodeA ?? nodeB.Value, nodeA.HasValue ? b : a);
        }
        else if (nodeA.Value != nodeB.Value)
        {
            FusionnerNoeuds(nodeA.Value, nodeB.Value);
        }
    }

    private void CreerNouveauNoeud(params Attache[] attaches)
    {
        noeuds.Add(nextNodeId, new HashSet<Attache>(attaches));
        nextNodeId++;
    }

    public List<Attache> GetAttachesConnectees(Attache attache)
    {
        foreach (var node in noeuds.Values)
            if (node.Contains(attache))
                return new List<Attache>(node);

        return new List<Attache>();
    }

    // Méthodes helper pour la gestion des noeuds
    private int? TrouverNoeud(Attache attache)
    {
        foreach (var pair in noeuds)
            if (pair.Value.Contains(attache))
                return pair.Key;

        return null;
    }
}