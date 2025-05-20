using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapheManager : MonoBehaviour
{
    public static GrapheManager Instance { get; private set; }

    private GrapheCircuit graphe = new GrapheCircuit();

    public GrapheCircuit Graphe => graphe; //type de getter en csharp

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AjouterLien(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        graphe.AjouterLien(a, b);
    }

    // Pour debug ou récupération
    public List<ComposanteDuCircuit> ObtenirVoisins(ComposanteDuCircuit c)
    {
        return graphe.ObtenirVoisins(c);
    }
    public void AfficherGraphe()
    {
        Debug.Log("/// ===== GRAPHE CIRCUIT =====");

        var dejaAffiches = new HashSet<string>();

        foreach (var composante in graphe.ObtenirTousLesComposants())
        {
            string nomA = composante.name;
            foreach (var voisin in graphe.ObtenirVoisins(composante))
            {
                string lien = $"{nomA} <--> {voisin.name}";

                // Éviter les doublons (A <--> B affiché deux fois)
                string cle1 = $"{nomA}-{voisin.name}";
                string cle2 = $"{voisin.name}-{nomA}";
                if (!dejaAffiches.Contains(cle1) && !dejaAffiches.Contains(cle2))
                {
                    Debug.Log(lien);
                    dejaAffiches.Add(cle1);
                    dejaAffiches.Add(cle2);
                }
            }
        }
        Debug.Log("/// ==========================");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GrapheManager.Instance.AfficherGraphe();
        }
    }
}