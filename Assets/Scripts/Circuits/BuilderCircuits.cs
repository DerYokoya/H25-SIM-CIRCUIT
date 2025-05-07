using System.Collections.Generic;

public class CircuitGraphBuilder
{
    public CircuitGraph BuildGraph(List<ComposanteDuCircuit> components)
    {
        var graph = new CircuitGraph();

        foreach (var component in components)
        {
            if (component.componentType == "wire") continue;

            var endA = FindConnectedComponent(component.PointA, component);
            var endB = FindConnectedComponent(component.PointB, component);

            if (endA != null && endB != null)
            {
                graph.AddComponent(endA.name, endB.name, component.componentType, 5); // TEMPORAIRE A ENLEVER
            }
        }

        return graph;
    }

    private ComposanteDuCircuit FindConnectedComponent(Attache start, ComposanteDuCircuit origin)
    {
        var visited = new HashSet<Attache>();
        var queue = new Queue<Attache>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited.Add(current);

            var comp = current.ParentComponent;
            if (comp != origin && comp.componentType != "wire")
            {
                return comp;
            }

            foreach (var neighbor in current.ConnectedPoints)
            {
                if (!visited.Contains(neighbor))
                    queue.Enqueue(neighbor);
            }
        }

        return null;
    }
}