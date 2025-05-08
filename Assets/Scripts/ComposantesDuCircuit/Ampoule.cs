using System;

public class Ampoule : Resistance

{
    public double Puissance { get; set; }
    public double Tension { get; set; }
    public double Luminosite { get; set; }


    protected override void Awake()
    {
    }

    protected override void Update()
    {
    }

    public void ChangementLuminosite()
    {
    }

    public void CalculPuissance()
    {
        Puissance = Math.Pow(Tension, 2) / ValeurResistance; // Calcul de la puissance, qui va déterminer la luminosité
    }

}
