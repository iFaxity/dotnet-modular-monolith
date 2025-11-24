namespace Shared.Application;

internal sealed class Failure(IError error) : IFailure
{
    public bool IsSuccess => false;
    public bool IsFailure => true;
    public IError Error => error;

    /// <summary>
    /// Returns a string representation of the <see cref="Failure"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => $"Failure({Error.Code}): {Error.Message}";
}
