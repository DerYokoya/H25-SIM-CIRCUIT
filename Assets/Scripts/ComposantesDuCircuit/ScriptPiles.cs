using System;
using UnityEngine;

public class Pile : ComposanteDuCircuit
{
    public double Tension { get; private set; }
    public double Capacite { get; private set; }
    public bool Surchauffee { get; private set; } = false;

    public double GetTension()
    {
        return Tension;
    }

    public void SetTension(double tension)
    {
        Tension = tension;
    }

    public double GetCapacite()
    {
        return Capacite;
    }

    public void SetCapacite(double capacite)
    {
        Capacite = capacite;
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
