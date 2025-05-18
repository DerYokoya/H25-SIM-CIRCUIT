using System;
using UnityEngine;
using System.Collections;

public class Fusible : ComposanteDuCircuit
{
    [Header("Effets visuels")]
    public GameObject prefabEffetBrulure;   // Glisse le prefab de particules ici
    public Renderer visuelFusible;          // Glisse le mesh renderer ici

    private GameObject effetInstancie;
    private bool estBrule = false;

    public float IntensiteMax = 4f;

    private void Start()
    {
        // Valeur initiale si non définie ailleurs
        if (IntensiteMax <= 0)
            IntensiteMax = 10;
    }

    public override void Augmentation() => AjusterIntensiteMax(3);

    public override void Diminution() => AjusterIntensiteMax(-3);

    private void AjusterIntensiteMax(int quantite)
    {
        IntensiteMax = Math.Clamp(IntensiteMax + quantite, 1, 20);
    }

    public double GetIntensiteCourantMax() => IntensiteMax;

    public void SetIntensiteCourantMax(float intensiteMax)
    {
        IntensiteMax = intensiteMax;
    }

    public void VerifierIntensite(float intensite)
    {
        if (intensite > IntensiteMax)
        {
            BrulerFusible();
        }
    }

    private void BrulerFusible()
    {
        if (estBrule) return;
        estBrule = true;

        if (visuelFusible != null)
            visuelFusible.material.color = Color.black;

        if (prefabEffetBrulure != null)
        {
            effetInstancie = Instantiate(prefabEffetBrulure, transform.position, transform.rotation, transform);
            var ps = effetInstancie.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // Modifier directement les propriétés du système de particules
                var main = ps.main;

                // Définir l'espace de simulation en mode World pour éviter les influences du parent
                main.simulationSpace = ParticleSystemSimulationSpace.World;

                // Contrôler la direction d'émission
                var shape = ps.shape;
                shape.enabled = true;

                // Définir la direction souhaitée (par exemple, vers le haut)
                // Remplacez Vector3.up par la direction désirée
                Vector3 direction = Vector3.up; // ou transform.up pour suivre l'orientation du fusible

                // Appliquer la direction au shape module
                shape.rotation = Quaternion.LookRotation(direction).eulerAngles;

                ps.Play();
            }
        }

        this.enabled = false;
    }
    public override string TexteValeur()
    {
        return "Maximum : " + IntensiteMax + " A";
    }
}
