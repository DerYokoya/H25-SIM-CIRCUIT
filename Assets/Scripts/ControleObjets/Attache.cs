using System.Collections.Generic;
using UnityEngine;


/**
 * Classe principale des attaches ou extrémités des composants du circuits.
 */

public class Attache : MonoBehaviour
{
    public ComposanteDuCircuit composantParent;
    public string otherAttachTag = "Attache";
    public ConnectionNode NoeudActuelle;

    /**
     * Gestion collision entrant
     * 
     */
    private void OnTriggerEnter(Collider other)
    {

        // vérification que le Collider est aussi une attache.
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Attache ne s'attache pas avec une attache de même parent
        if (otherAttache.composantParent == composantParent) return;

        /* Gestion des noeuds pour le graphe avec 3 cas si l'attache exterieur
         * cas 1 : si les deux attaches ne sont pas dans un noeud on creer un nouveau noeud
         * cas 2 : si un des deux attaches sont dans un noeud existant l'attache singulier va rejoindre ce noeud
         * cas 3 : si les deux attaches sojnt deja dans deux noeud différents, on fusione ces 2 noeud en un seule.
         */
        bool thisHasNode = NoeudActuelle != null;
        bool otherHasNode = otherAttache.NoeudActuelle != null;

        if (!thisHasNode && !otherHasNode) // cas 1
        {
            creationNouveauNoeud(this, otherAttache);
        }
        else if (thisHasNode && !otherHasNode) // cas 2
        {
            ajoutNoeudExistant(otherAttache, NoeudActuelle);
        }
        else if (!thisHasNode && otherHasNode) // cas 2
        {
            ajoutNoeudExistant(this, otherAttache.NoeudActuelle);
        }
        else
        {
            fusionNoeudsExistants(otherAttache); //cas 3
        }
    }


    //méthode pour le cas 1 de la méthode OnTriggerEnter()
    private void creationNouveauNoeud(Attache a, Attache b)
    {
        ConnectionNode newNode = new ConnectionNode();
        newNode.attaches.Add(a);
        newNode.attaches.Add(b);
        a.NoeudActuelle = newNode;
        b.NoeudActuelle = newNode;
        GestionnaireGraphe.Instance.noeuds.Add(newNode);
    }

    //méthode pour le cas 2 de la méthode OnTriggerEnter()
    private void ajoutNoeudExistant(Attache attache, ConnectionNode node)
    {
        node.attaches.Add(attache);
        attache.NoeudActuelle = node;
    }

    //méthode pour le cas 1 de la méthode OnTriggerEnter()
    private void fusionNoeudsExistants(Attache otherAttache)
    {
        if (NoeudActuelle != otherAttache.NoeudActuelle)
        {
            GestionnaireGraphe.Instance.fusionNoeuds(
                NoeudActuelle,
                otherAttache.NoeudActuelle
            );
        }
    }

    private void OnDestroy()
    {
        CleanUpConnection();
    }

    /*
     * Que faire si on détruit le composant électrique avec la classe SupprimerObjet et la supression de ce composant des listes du graphes
     */
    private void CleanUpConnection()
    {
        if (NoeudActuelle != null)
        {
            var attachedCopies = new List<Attache>(NoeudActuelle.attaches);

            foreach (var attache in attachedCopies)
            {
                if (attache != this && attache != null)
                {
                    GestionnaireGraphe.Instance.supprimerDepuisNoeud(attache);
                }
            }

            GestionnaireGraphe.Instance.supprimerDepuisNoeud(this);
        }
    }


    /*
     * Que faire si on détache le point de connection et la déconnection du circuit au graphe
     */
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(otherAttachTag)) return;

        Attache otherAttache = other.GetComponent<Attache>();
        if (otherAttache == null) return;

        // Prevent snapping logic should also ignore same component
        if (otherAttache.composantParent == composantParent) return;

        if (NoeudActuelle != null &&
            NoeudActuelle == otherAttache.NoeudActuelle)
        {
            GestionnaireGraphe.Instance.supprimerDepuisNoeud(this);
            GestionnaireGraphe.Instance.supprimerDepuisNoeud(otherAttache);
        }
    }


    /* L'attachement visuelle
     */
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
