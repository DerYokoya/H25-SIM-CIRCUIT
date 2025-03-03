using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusible : ComposanteDuCircuit
{
    public double IntensiteMax { get; private set; }
    public double Intensite { get; set; }

    public double GetIntensiteCourantMax()
    {
        return IntensiteMax;
    }

    public void SetIntensiteCourantMax(double intensiteMax)
    {
        IntensiteMax = intensiteMax;
    }

    public void VerifierIntensite()
    {
        if (Intensite > IntensiteMax)
        {
            ChangerEtat();
        }
    }
}
