namespace WaylandDotnet;

/// <summary> Wayland fixed-point 26.6 number. </summary>
/// <param name="d">The value to encode.</param>
public readonly struct WlFixed(double d) : IEquatable<WlFixed>
{
    private readonly uint value = (uint)(d * 256.0);

    /// <summary> Converts the fixed-point value to a double. </summary>
    public double ToDouble()
    {
        return value / 256.0;
    }

    /// <inheritdoc />
    public bool Equals(WlFixed other)
    {
        return value == other.value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is WlFixed t && Equals(t);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    /// <summary> Determines whether two values are equal. </summary>
    public static bool operator ==(WlFixed left, WlFixed right)
    {
        return left.Equals(right);
    }

    /// <summary> Determines whether two values are not equal. </summary>
    public static bool operator !=(WlFixed left, WlFixed right)
    {
        return !(left == right);
    }
}