using System.Collections.Generic;
using UnityEngine;

public class Attache : MonoBehaviour
{
    public ComposanteDuCircuit composantParent;
    public string otherAttachTag = "Attache";
    public ConnectionNode currentConnectionNode;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Prevent snapping with another attach of the same component
        if (otherAttache.composantParent == composantParent) return;

        bool thisHasNode = currentConnectionNode != null;
        bool otherHasNode = otherAttache.currentConnectionNode != null;

        if (!thisHasNode && !otherHasNode)
        {
            CreateNewConnectionNode(this, otherAttache);
        }
        else if (thisHasNode && !otherHasNode)
        {
            AddToExistingNode(otherAttache, currentConnectionNode);
        }
        else if (!thisHasNode && otherHasNode)
        {
            AddToExistingNode(this, otherAttache.currentConnectionNode);
        }
        else
        {
            MergeExistingNodes(otherAttache);
        }
    }

    private void CreateNewConnectionNode(Attache a, Attache b)
    {
        ConnectionNode newNode = new ConnectionNode();
        newNode.attaches.Add(a);
        newNode.attaches.Add(b);
        a.currentConnectionNode = newNode;
        b.currentConnectionNode = newNode;
        GraphManager.Instance.nodes.Add(newNode);
    }

    private void AddToExistingNode(Attache attache, ConnectionNode node)
    {
        node.attaches.Add(attache);
        attache.currentConnectionNode = node;
    }

    private void MergeExistingNodes(Attache otherAttache)
    {
        if (currentConnectionNode != otherAttache.currentConnectionNode)
        {
            GraphManager.Instance.MergeNodes(
                currentConnectionNode,
                otherAttache.currentConnectionNode
            );
        }
    }

    private void OnDestroy()
    {
        CleanUpConnection();
    }

    private void CleanUpConnection()
    {
        if (currentConnectionNode != null)
        {
            // Create a copy to avoid modifying the list during iteration
            var attachedCopies = new List<Attache>(currentConnectionNode.attaches);

            foreach (var attache in attachedCopies)
            {
                if (attache != this && attache != null)
                {
                    GraphManager.Instance.RemoveFromNode(attache);
                }
            }

            GraphManager.Instance.RemoveFromNode(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Prevent snapping logic should also ignore same component
        if (otherAttache.composantParent == composantParent) return;

        if (currentConnectionNode != null &&
            currentConnectionNode == otherAttache.currentConnectionNode)
        {
            GraphManager.Instance.RemoveFromNode(this);
            GraphManager.Instance.RemoveFromNode(otherAttache);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Prevent staying logic for same component
        if (otherAttache.composantParent == composantParent) return;

        Vector3 offset = composantParent.transform.position - transform.position;
        composantParent.transform.position = other.transform.position + offset;
    }
}
