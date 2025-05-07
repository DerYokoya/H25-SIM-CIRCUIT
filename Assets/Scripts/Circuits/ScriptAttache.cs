using System.Collections.Generic;
using UnityEngine;

public class Attache : MonoBehaviour
{
    public ComposanteDuCircuit ParentComponent;
    public List<Attache> ConnectedPoints = new();
}