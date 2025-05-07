using System.Collections.Generic;

public class CircuitAnalyzer
{
    public bool PathExists(CircuitNode start, CircuitNode end, HashSet<CircuitNode> visited = null)
    {
        if (start == end) return true;

        visited ??= new HashSet<CircuitNode>();
        visited.Add(start);

        foreach (var edge in start.Connections)
        {
            if (!edge.IsActive) continue; // Interrupteur ouvert
            if (visited.Contains(edge.To)) continue;

            if (PathExists(edge.To, end, visited)) return true;
        }

        return false;
    }
}
