using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Attache> Attaches = new List<Attache>(); // Toutes les attaches connectées
    public bool IsVisited = false; // Pour BFS/DFS

    public void Merge(Node otherNode)
    {
        foreach (var attach in otherNode.Attaches)
        {
            if (!Attaches.Contains(attach))
            {
                Attaches.Add(attach);
                attach.LinkedNode = this; // Mise à jour de la référence
            }
        }
    }
}