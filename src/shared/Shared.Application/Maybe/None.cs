namespace Shared.Application;

internal sealed class None : INone
{
    public static readonly INone Instance = new None();

    public bool IsSome => false;
    public bool IsNone => true;

    private None() { }
}
