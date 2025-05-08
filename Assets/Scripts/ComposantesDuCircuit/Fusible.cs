using System;
using UnityEngine;

public class Fusible : ComposanteDuCircuit
{
    public double IntensiteMax { get; private set; }

    public override void Augmentation() => AjusterIntensiteMax(3);

    public override void Diminution() => AjusterIntensiteMax(-3);

    private void AjusterIntensiteMax(int quantite)
    {
        IntensiteMax = Math.Clamp(IntensiteMax + quantite, 1, 20); // Minimum 1, maximum 20
    }

    public double GetIntensiteCourantMax()
    {
        return IntensiteMax;
    }

    public void SetIntensiteCourantMax(double intensiteMax)
    {
        IntensiteMax = intensiteMax;
    }

    public void VerifierIntensite(float intensite)
    {
        if (intensite > IntensiteMax)
        {
            ChangerEtat();
        }
    }

    public override string TexteValeur()
    {
        return "Maximum : " + IntensiteMax + " A";
    }
}
