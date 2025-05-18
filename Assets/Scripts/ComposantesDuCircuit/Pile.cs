using System;
using UnityEngine;

public class Pile : ComposanteDuCircuit
{
    public float Tension = 10;
    public bool Surchauffee = false;

    public float GetTension()
    {
        return Tension;
    }

    public void SetTension(float tension)
    {
        Tension = tension;
    }

    public bool EstSurchauffee()
    {
        return Surchauffee;
    }

    public override void Augmentation() => AjusterTension(10);

    public override void Diminution() => AjusterTension(-10);

    private void AjusterTension(int quantite)
    {
        Tension = Math.Clamp(Tension + quantite, 1, 120); // Minimum 1, maximum 120
    }

    public override string TexteValeur()
    {
        return Tension + " V";
    }
}
