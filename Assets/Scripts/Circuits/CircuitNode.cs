using System.Collections.Generic;

public class CircuitNode
{
    public string Id; // Nom du nœud, comme "A", "B", etc.
    public List<CircuitEdge> Connections = new List<CircuitEdge>();

    public CircuitNode(string id)
    {
        Id = id;
    }
}