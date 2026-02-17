namespace WaylandDotnet;

public readonly struct WlFixed(double d) : IEquatable<WlFixed>
{
    private readonly uint value = (uint)(d * 256.0);

    public double ToDouble()
    {
        return value / 256.0;
    }

    public bool Equals(WlFixed other)
    {
        return value == other.value;
    }

    public override bool Equals(object? obj)
    {
        return obj is WlFixed t && Equals(t);
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public static bool operator ==(WlFixed left, WlFixed right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WlFixed left, WlFixed right)
    {
        return !(left == right);
    }
}