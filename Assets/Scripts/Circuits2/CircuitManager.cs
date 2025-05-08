using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    public CircuitGraph Graph { get; private set; }

    public void RebuildGraph()
    {
        List<ComposanteDuCircuit> composants = FindObjectsOfType<ComposanteDuCircuit>().ToList();
        CircuitGraphBuilder builder = new();
        Graph = builder.BuildGraph(composants);

        Debug.Log(" Graphe reconstruit avec " + composants.Count + " composants.");

        Graph.PrintGraph();  //Affiche le contenu du graphe
    }


}