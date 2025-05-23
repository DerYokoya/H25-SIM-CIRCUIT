using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEditor.MemoryProfiler;

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance { get; private set; }

    public List<ConnectionNode> nodes = new List<ConnectionNode>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void MergeNodes(ConnectionNode targetNode, ConnectionNode nodeToMerge)
    {
        if (targetNode == nodeToMerge) return;

        foreach (Attache attache in nodeToMerge.attaches)
        {
            attache.currentConnectionNode = targetNode;
            targetNode.attaches.Add(attache);
        }

        nodes.Remove(nodeToMerge);
    }

    public void FullCleanup()
    {
        // Nettoie les nodes vides ou corrompues
        nodes.RemoveAll(node =>
            node == null ||
            node.attaches == null ||
            node.attaches.Count == 0 ||
            node.attaches.All(a => a == null));

        // Nettoie les attaches null dans les nodes restantes
        foreach (var node in nodes)
        {
            node.attaches.RemoveAll(a => a == null);
        }
    }

    public void RemoveFromNode(Attache attache)
    {
        if (attache == null) return;

        FullCleanup(); // Nettoyage préventif

        ConnectionNode node = attache.currentConnectionNode;
        if (node == null) return;

        node.attaches.Remove(attache);
        attache.currentConnectionNode = null;

        // Si le node devient trop petit
        if (node.attaches.Count <= 1)
        {
            if (node.attaches.Count == 1)
            {
                Attache remaining = node.attaches[0];
                remaining.currentConnectionNode = null;
            }
            nodes.Remove(node);
        }

        FullCleanup(); // Nettoyage final
    }

    public float GetCurrentForComponent(ComposanteDuCircuit component)
    {
        // Recherche dans toutes les connexions
        foreach (var node in nodes)
        {
            foreach (var attache in node.attaches)
            {
                if (attache.composantParent == component)
                {
                    return component.courant; // Ajoutez une propriété courant à ComposanteDuCircuit
                }
            }
        }
        return 0f;
    }


    // --------------------------       PARTIE ANALAYSE         --------------------------

    public void AnalyzeSeriesCircuit()
    {
        List<Pile> piles = nodes.SelectMany(n => n.attaches)
                                .Select(a => a.composantParent as Pile)
                                .Where(p => p != null)
                                .Distinct()
                                .ToList();

        foreach (Pile pile in piles)
        {
            if (pile.attachePlus == null || pile.attacheMinus == null)
                continue;

            ConnectionNode startNode = pile.attachePlus.currentConnectionNode;
            ConnectionNode endNode = pile.attacheMinus.currentConnectionNode;

            // Vérifier si les deux bornes sont connectées
            if (startNode == null || endNode == null)
            {
                Debug.Log("Circuit ouvert - bornes non connectées");
                ResetComponents(pile);
                continue;
            }

            List<ComposanteDuCircuit> components = new List<ComposanteDuCircuit>();
            HashSet<ConnectionNode> visitedNodes = new HashSet<ConnectionNode>();
            List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)> nodeConnections = new List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)>();

            ConnectionNode currentNode = startNode;
            bool circuitFerme = false;

            while (currentNode != null && !visitedNodes.Contains(currentNode))
            {
                visitedNodes.Add(currentNode);

                // Vérifier si on a atteint la borne négative
                if (currentNode == endNode)
                {
                    circuitFerme = true;
                    break;
                }

                foreach (Attache attache in currentNode.attaches)
                {
                    ComposanteDuCircuit comp = attache.composantParent;
                    if (comp == null || comp == pile || components.Contains(comp)) continue;

                    Attache otherAttache = GetOtherAttache(attache);
                    ConnectionNode nextNode = otherAttache.currentConnectionNode;

                    nodeConnections.Add((currentNode, nextNode, comp));
                    components.Add(comp);
                    currentNode = nextNode;
                    break;
                }
            }

            if (circuitFerme)
            {
                float totalVoltage = pile.Tension;
                float totalResistance = components.OfType<Resistance>().Sum(r => r.valeurResistance);
                float current = totalVoltage / totalResistance;

                UpdateComponents(current, components, piles);

                foreach (var connection in nodeConnections)
                {
                    Debug.Log($"COURANT: {current:F2}A...");
                }
            }
            else
            {
                Debug.Log("Circuit ouvert - pas de boucle complète");
                ResetComponents(pile);
            }
        }
    }

    private void ResetComponents(Pile pile)
    {
        // Réinitialiser tous les composants connectés
        foreach (var node in nodes)
        {
            foreach (var attache in node.attaches)
            {
                if (attache.composantParent is Ampoule ampoule)
                    ampoule.ChangementLuminosite(0f);
                else if (attache.composantParent is Fusible fusible)
                    fusible.ReparerFusible();
                attache.composantParent.courant = 0f;
            }
        }
        pile.setEstSurchauffee(false);
        pile.courant = 0f;
    }


    private Attache GetOtherAttache(Attache attache)
    {
        string suffix = attache.gameObject.name.Last().ToString();
        string otherSuffix = suffix == "1" ? "2" : "1";
        return attache.composantParent.GetComponentsInChildren<Attache>()
                                      .First(a => a.gameObject.name.EndsWith(otherSuffix));
    }

    private void UpdateComponents(float courant, List<ComposanteDuCircuit> components, List<Pile> piles)
    {
        foreach (var comp in components)
        {
            if (comp is Ampoule ampoule)
                ampoule.ChangementLuminosite(courant);
            else if (comp is Fusible fusible)
                fusible.Bruler(courant);
            comp.courant = courant;
        }

        foreach (var pile in piles)
        {
            if (courant >= float.MaxValue)
                pile.setEstSurchauffee(true);
            else
            {
                pile.setEstSurchauffee(false);
            }
            pile.courant = courant;
        }
    }

    public void Update()
    {
        AnalyzeSeriesCircuit();
    }
}

public class ConnectionNode
{
    public List<Attache> attaches = new List<Attache>();
}