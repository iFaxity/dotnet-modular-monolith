namespace Shared.Abstractions;

/// <summary>
/// Represents the absence of a value (like void) for non-generic operations.
/// </summary>
public sealed class Unit
{
    public static readonly Unit Instance = new();

    private Unit() { }
}
