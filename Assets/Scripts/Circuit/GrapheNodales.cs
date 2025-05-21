using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

    public void LogConnectionGraph()
    {
        Debug.Log("=== GRAPHE DE CONNEXION ===");
        foreach (var node in nodes)
        {
            var sb = new StringBuilder();
            sb.Append($"Node [{node.GetHashCode()}] contient {node.attaches.Count} attaches: ");

            foreach (var attache in node.attaches)
            {
                sb.Append($"{attache.composantParent.gameObject.name}.{attache.gameObject.name}, ");
            }

            Debug.Log(sb.ToString());
        }
        Debug.Log("===========================");
    }

    public void AnalyzeSeriesCircuit()
    {
        List<Pile> piles = nodes.SelectMany(n => n.attaches)
                                .Select(a => a.composantParent as Pile)
                                .Where(p => p != null)
                                .Distinct()
                                .ToList();

        foreach (Pile pile in piles)
        {

            ConnectionNode currentNode = pile.attachePlus.currentConnectionNode;
            List<ComposanteDuCircuit> components = new List<ComposanteDuCircuit>();
            HashSet<ConnectionNode> visitedNodes = new HashSet<ConnectionNode>();

            // Nouvelle liste pour stocker les connexions entre nœuds
            List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)> nodeConnections = new List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)>();

            while (currentNode != null && !visitedNodes.Contains(currentNode))
            {
                visitedNodes.Add(currentNode);
                foreach (Attache attache in currentNode.attaches)
                {
                    ComposanteDuCircuit comp = attache.composantParent;
                    if (comp == null || comp == pile || components.Contains(comp)) continue;

                    Attache otherAttache = GetOtherAttache(attache);
                    ConnectionNode nextNode = otherAttache.currentConnectionNode;

                    // Enregistrement de la connexion
                    nodeConnections.Add((currentNode, nextNode, comp));

                    components.Add(comp);
                    currentNode = nextNode;
                    break;
                }
            }

            float totalVoltage = pile.Tension;
            float totalResistance = components.OfType<Resistance>().Sum(r => r.valeurResistance);
            float current = totalVoltage / totalResistance;

            UpdateComponents(current, components, piles);

            // Log des courants entre nœuds
            foreach (var connection in nodeConnections)
            {
                Debug.Log($"COURANT: {current:F2}A " +
                          $"de [Node {connection.from.GetHashCode()}] " +
                          $"à [Node {connection.to.GetHashCode()}] " +
                          $"via {connection.comp.gameObject.name}");
            }
        }
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
        }

        foreach (var pile in piles)
        {
            if (courant >= float.MaxValue)
                pile.setEstSurchauffee(true);
            else
            {
                pile.setEstSurchauffee(false);
            }
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