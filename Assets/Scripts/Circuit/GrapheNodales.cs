using UnityEngine;
using System.Collections.Generic;
using System.Linq;


/**
 * 
 * Singleton principale qui gere le circuit du simulateur. Calcul le courant à chaque unité de temps update().
 */
public class GestionnaireGraphe : MonoBehaviour
{
    public static GestionnaireGraphe Instance { get; private set; }
    public List<ConnectionNode> noeuds = new List<ConnectionNode>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    /*
     * Méthode pour fusionner des noeud
     */
public void fusionNoeuds(ConnectionNode noeudCible, ConnectionNode neoudAFusione)
    {
        if (noeudCible == neoudAFusione) return;

        foreach (Attache attache in neoudAFusione.attaches)
        {
            attache.NoeudActuelle = noeudCible;
            noeudCible.attaches.Add(attache);
        }

        noeuds.Remove(neoudAFusione);
    }

    /*
     * Méthode qui supprime les liste des noeuds avec des réferences nulles ou corrumpues.
     */
    public void nettoyageComplet()
    {
        // Nettoie les nodes vides ou corrompues
        noeuds.RemoveAll(noeud =>
            noeud == null ||
            noeud.attaches == null ||
            noeud.attaches.Count == 0 ||
            noeud.attaches.All(a => a == null));

        // Nettoie les attaches null dans les nodes restantes
        foreach (var noeud in noeuds)
        {
            noeud.attaches.RemoveAll(a => a == null);
        }
    }

    /*
     * Méthode principale pour enlever un attache d'un noeud
     */
    public void supprimerDepuisNoeud(Attache attache)
    {
        if (attache == null) return;

        nettoyageComplet(); // Nettoyage préventif

        ConnectionNode noeud = attache.NoeudActuelle;
        if (noeud == null) return;

        noeud.attaches.Remove(attache);
        attache.NoeudActuelle = null;

        // Si le node devient trop petit
        if (noeud.attaches.Count <= 1)
        {
            if (noeud.attaches.Count == 1)
            {
                Attache remaining = noeud.attaches[0];
                remaining.NoeudActuelle = null;
            }
            noeuds.Remove(noeud);
        }

        nettoyageComplet(); // Nettoyage final
    }

    /**
     * Analyse rapide d'un circuit en série seulement. Trajectoire de la borne + à la borne - des piles.
     * Algortihme DFS (Depth-First Search). Récupération des résistences et tensions totales et calculs du courant.
     */
    public void AnalyzeSeriesCircuit()
    {
        List<Pile> piles = noeuds.SelectMany(n => n.attaches)
                                .Select(a => a.composantParent as Pile)
                                .Where(p => p != null)
                                .Distinct()
                                .ToList();

        foreach (Pile pile in piles)
        {
            if (pile.attachePlus == null || pile.attacheMinus == null)
                continue;

            ConnectionNode startNode = pile.attachePlus.NoeudActuelle;
            ConnectionNode endNode = pile.attacheMinus.NoeudActuelle;

            // Vérifier si les deux bornes sont connectées
            if (startNode == null || endNode == null)
            {
                Debug.Log("Circuit ouvert - bornes non connectées");
                reinitialiserComposantes(pile);
                continue;
            }

            List<ComposanteDuCircuit> components = new List<ComposanteDuCircuit>();
            HashSet<ConnectionNode> visitedNodes = new HashSet<ConnectionNode>();
            List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)> nodeConnections = new List<(ConnectionNode from, ConnectionNode to, ComposanteDuCircuit comp)>();

            ConnectionNode currentNode = startNode;
            bool circuitFerme = false;

            while (currentNode != null && !visitedNodes.Contains(currentNode))
            {
                visitedNodes.Add(currentNode);

                // Vérifier si on a atteint la borne négative
                if (currentNode == endNode)
                {
                    circuitFerme = true;
                    break;
                }

                foreach (Attache attache in currentNode.attaches)
                {
                    ComposanteDuCircuit comp = attache.composantParent;
                    if (comp == null || comp == pile || components.Contains(comp)) continue;

                    Attache otherAttache = GetOtherAttache(attache);
                    ConnectionNode nextNode = otherAttache.NoeudActuelle;

                    nodeConnections.Add((currentNode, nextNode, comp));
                    components.Add(comp);
                    currentNode = nextNode;
                    break;
                }
            }

            if (circuitFerme)
            {
                float totalVoltage = pile.Tension;
                float totalResistance = components.OfType<Resistance>().Sum(r => r.valeurResistance);
                float current = totalVoltage / totalResistance;

                UpdateComponents(current, components, piles);

                foreach (var connection in nodeConnections)
                {
                    Debug.Log($"COURANT: {current:F2}A...");
                }
            }
            else
            {
                Debug.Log("Circuit ouvert - pas de boucle complète");
                reinitialiserComposantes(pile);
            }
        }
    }

    /**
     * méthode utile pour ne pas activer les composantes tant que le circuit n'est pas fermé
     */
    private void reinitialiserComposantes(Pile pile)
    {
        // Réinitialiser tous les composants connectés
        foreach (var node in noeuds)
        {
            foreach (var attache in node.attaches)
            {
                if (attache.composantParent is Ampoule ampoule)
                    ampoule.ChangementLuminosite(0f);
                attache.composantParent.courant = 0f;
            }
        }
        pile.setEstSurchauffee(false);
        pile.courant = 0f;
    }

    /*
     * Méthode pour avoir l'attache opposée d'un composant.
     */
    private Attache GetOtherAttache(Attache attache)
    {
        string suffix = attache.gameObject.name.Last().ToString();
        string otherSuffix = suffix == "1" ? "2" : "1";
        return attache.composantParent.GetComponentsInChildren<Attache>()
                                      .First(a => a.gameObject.name.EndsWith(otherSuffix));
    }

    /**
     * Mettre à jour les composants, bruler les piles, allumer l'ampoule, peter un fusible.
     */
    private void UpdateComponents(float courant, List<ComposanteDuCircuit> components, List<Pile> piles)
    {
        foreach (var comp in components)
        {
            if (comp is Ampoule ampoule)
                ampoule.ChangementLuminosite(courant);
            else if (comp is Fusible fusible)
                fusible.Bruler(courant);
            comp.courant = courant;
        }

        foreach (var pile in piles)
        {
            if (courant >= float.MaxValue)
                pile.setEstSurchauffee(true);
            else
            {
                pile.setEstSurchauffee(false);
            }
            pile.courant = courant;
        }
    }

    public void Update()
    {
        AnalyzeSeriesCircuit();
    }
}


/*
 *  Classe plus ou moins type <<Wrapper>> des points de connection ou noeud du circuit. 
 *  
 *  Un noeud ou point de connection du circuit est une liste type attache (extremité des composantes du circuit) car un
 *  noeud dans un circuit peut avoir plus de deux chemins possibles par exemple une pile qui va soit vers une Bracnhe A
 *  ou vers une branche B. Cette liste sera utilie pour la détection de boucle.
 * 
 */
public class ConnectionNode
{
    public List<Attache> attaches = new List<Attache>();
}