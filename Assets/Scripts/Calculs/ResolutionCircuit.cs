using UnityEngine;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

public class CircuitSolver
{
    private GrapheCircuit graphe;

    public CircuitSolver(GrapheCircuit graphe)
    {
        this.graphe = graphe;
    }

    public void Resoudre()
    {
        // 1. Indexer tous les nœuds (chaque composant est un noeud électrique ici)
        var noeudIndex = graphe.ObtenirTousLesComposants()
                              .Select((comp, idx) => (comp, idx))
                              .ToDictionary(t => t.comp, t => t.idx);
        int N = noeudIndex.Count;

        // 2. Construire la matrice G (NxN) et le vecteur B (N)
        var G = Matrix<double>.Build.Dense(N, N);
        var B = Vector<double>.Build.Dense(N);

        // 3. Pour chaque branche a<->b, ajouter les termes à G et B
        foreach (var a in noeudIndex.Keys)
        {
            int i = noeudIndex[a];
            foreach (var b in graphe.ObtenirVoisins(a))
            {
                int j = noeudIndex[b];

                // Récupérer R et E pour la branche a->b
                float R = ObtenirResistance(a, b);
                float E = ObtenirFem(a, b);

                double conductance = 1.0 / R;

                // Ajout de la conductance à la matrice
                G[i, i] += conductance;
                G[i, j] -= conductance;

                // Contribution de la source de tension
                B[i] += E / R;
            }
        }

        // 4. Choisir un nœud de référence (ici, on prend le dernier) et "supprimer" sa ligne/colonne
        int refIdx = N - 1;
        // On retire la ligne refIdx et la colonne refIdx de G et l’entrée refIdx de B
        G = G.RemoveRow(refIdx).RemoveColumn(refIdx);

        N = B.Count;

        var B_reduced = Vector<double>.Build.Dense(N - 1);
        for (int i = 0; i < refIdx; i++)
            B_reduced[i] = B[i];
        for (int i = refIdx + 1; i < N; i++)
            B_reduced[i - 1] = B[i];
        B = B_reduced;

        // 5. Résoudre G_reduced · V = B_reduced
        Vector<double> V_reduced = G.Solve(B);

        // 6. Reconstruire le vecteur complet des tensions en réinsérant V[refIdx]=0
        var V = Vector<double>.Build.Dense(N);
        for (int k = 0, r = 0; k < N; k++)
        {
            if (k == refIdx)
                V[k] = 0.0;
            else
                V[k] = V_reduced[r++];
        }

        // 7. Calcul des courants et vérification des fusibles
        foreach (var a in noeudIndex.Keys)
        {
            int i = noeudIndex[a];
            foreach (var b in graphe.ObtenirVoisins(a))
            {
                int j = noeudIndex[b];
                float R = ObtenirResistance(a, b);
                float E = ObtenirFem(a, b);

                double I = (V[i] - V[j] - E) / R;  // courant de a vers b

                // Si c’est un fusible, vérifier la limite
                if (a is Fusible fusibleA)
                {
                    if (Mathf.Abs((float)I) > fusibleA.GetIntensiteCourantMax())
                    {
                        Debug.Log($"Fusible {a.name} grillé (I={I:F2}A) — branche ouverte.");
                        graphe.RetirerLien(a, b);
                    }
                }

                // On peut stocker le courant dans le composant si tu as un champ pour ça
                if (a is IHasCurrent hasCurrent)
                    hasCurrent.SetCurrent((float)I);

                Debug.Log($"Courant {a.name}?{b.name} = {I:F3} A");
            }
        }
    }

    private float ObtenirResistance(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        // Si c’est une résistance ou ampoule
        if (a is Resistance r) return r.GetResistance();
        if (b is Resistance r2) return r2.GetResistance();
        // Pour un fil idéal, on peut prendre R très faible
        return 1e-6f;
    }

    private float ObtenirFem(ComposanteDuCircuit a, ComposanteDuCircuit b)
    {
        // Si a est une pile, tension orientée a?b
        if (a is Pile pileA) return pileA.GetTension();
        // Si b est une pile, alors tension de b?a = –tensionVolt
        if (b is Pile pileB) return -pileB.GetTension();
        return 0f;
    }
}

// Interface facultative pour stocker le courant
public interface IHasCurrent
{
    void SetCurrent(float ampere);
}