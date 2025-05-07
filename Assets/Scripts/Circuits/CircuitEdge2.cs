public class CircuitEdge
{
    public CircuitNode From;
    public CircuitNode To;
    public string Type; // "resistor", "wire", "battery", "switch"
    public float Value; // résistance (Ohm), tension (V), etc.
    public bool IsActive; // ex : interrupteur ouvert = false

    public CircuitEdge(CircuitNode from, CircuitNode to, string type, float value, bool isActive = true)
    {
        From = from;
        To = to;
        Type = type;
        Value = value;
        IsActive = isActive;
    }
}