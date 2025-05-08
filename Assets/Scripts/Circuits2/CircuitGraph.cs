using UnityEngine;
using System.Collections.Generic;

public class CircuitGraph
{
    public Dictionary<string, CircuitNode> Nodes = new Dictionary<string, CircuitNode>();

    public Dictionary<string, List<CircuitEdge>> adjacencyList = new();

    public CircuitNode GetOrCreateNode(string id)
    {
        if (!Nodes.ContainsKey(id))
            Nodes[id] = new CircuitNode(id);
        return Nodes[id];
    }

    public void AddComponent(string fromId, string toId, string type, float value, bool isActive = true)
    {
        CircuitNode from = GetOrCreateNode(fromId);
        CircuitNode to = GetOrCreateNode(toId);

        var edge = new CircuitEdge(from, to, type, value, isActive);
        from.Connections.Add(edge);

        // Si bidirectionnel (comme un fil), tu peux aussi ajouter :
        to.Connections.Add(new CircuitEdge(to, from, type, value, isActive));
    }
    public void PrintGraph()
    {
        Debug.Log("------ Graphe du circuit ------");

        foreach (var kvp in adjacencyList)
        {
            string from = kvp.Key;
            foreach (var edge in kvp.Value)
            {
                Debug.Log($"{from} -> {edge.To} | Type: {edge.Type} | Value: {edge.Value}");
            }
        }

        Debug.Log("------ Fin du graphe ------");
    }
}