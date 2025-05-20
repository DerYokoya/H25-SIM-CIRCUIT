using System;
using UnityEngine;

public class Resistance : ComposanteDuCircuit
{
    public float ValeurResistance = 10f; /*Si nous appelons ceci Resistance, il y aura une erreur
                                                            car la classe s'appele déja Resistance*/

    public Renderer bandesRenderer;
    private double derniereResistance = -1; // Pour comparer pour savoir quand changer les couleurs des bandes

    protected virtual void Awake()
    {
        if (!(this is Ampoule))
        {
            bandesRenderer = transform.Find("Corps/BandesCouleur").GetComponent<Renderer>();

            CouleursResistance.Noir = Resources.Load<Material>("Couleurs/couleurNoire");
            CouleursResistance.Brun = Resources.Load<Material>("Couleurs/couleurBrune");
            CouleursResistance.Rouge = Resources.Load<Material>("Couleurs/couleurRouge");
            CouleursResistance.Orange = Resources.Load<Material>("Couleurs/couleurOrange");
            CouleursResistance.Jaune = Resources.Load<Material>("Couleurs/couleurJaune");
            CouleursResistance.Vert = Resources.Load<Material>("Couleurs/couleurVerte");
            CouleursResistance.Bleu = Resources.Load<Material>("Couleurs/couleurBleue");
            CouleursResistance.Mauve = Resources.Load<Material>("Couleurs/couleurMauve");
            CouleursResistance.Gris = Resources.Load<Material>("Couleurs/couleurGrise");
            CouleursResistance.Blanc = Resources.Load<Material>("Couleurs/couleurBlanche");

            CouleursResistance.Or = Resources.Load<Material>("Couleurs/couleurOr");
            CouleursResistance.Argent = Resources.Load<Material>("Couleurs/couleurArgent");
            CouleursResistance.Erreur = Resources.Load<Material>("Couleurs/Transparent");
        }
    }
    protected virtual void Update()
    {
        if (ValeurResistance != derniereResistance)
        {
            ModifierBandesCouleurs();
            derniereResistance = ValeurResistance;
        }
    }

    void ModifierBandesCouleurs()
    {
        Material[] bandes = CouleursResistance.GetBandesCouleurs(ValeurResistance);
        
        Material[] mats = bandesRenderer.materials;

        // Remplace seulement les index 1, 2, et 3
        if (mats.Length >= 5 && bandes.Length >= 3)
        {
            mats[1] = bandes[0];
            mats[2] = bandes[1];
            mats[3] = bandes[2];

            bandesRenderer.materials = mats;
        }
    }

    public override void Augmentation() => AjusterIntensiteMax(3);

    public override void Diminution() => AjusterIntensiteMax(-3);

    public void AjusterIntensiteMax(int quantite)
    {
        ValeurResistance = Math.Clamp(ValeurResistance + quantite, 1, 100); // Minimum 1, maximum 100
    }

    public float GetResistance()
    {
        return ValeurResistance;
    }

    public void SetResistance(float resistance)
    {
        ValeurResistance = resistance;
    }

    public override string TexteValeur()
    {
        return ValeurResistance + "\u03A9"; // '\u03A9' est le symbole des ohms
    }
}