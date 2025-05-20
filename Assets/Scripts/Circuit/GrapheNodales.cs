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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LogConnectionGraph();
        }
    }
}

public class ConnectionNode
{
    public List<Attache> attaches = new List<Attache>();
}