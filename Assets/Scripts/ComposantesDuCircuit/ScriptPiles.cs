using System.Collections;
using System.Collections.Generic;
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
}
