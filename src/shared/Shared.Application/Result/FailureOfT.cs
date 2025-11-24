namespace Shared.Application;

internal sealed class Failure<T>(IError error) : IFailure<T>
    where T : notnull
{
    public bool IsSuccess => false;
    public bool IsFailure => true;
    public IError Error => error;

    /// <summary>
    /// Returns a string representation of the <see cref="Failure{T}"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => $"Failure({Error.Code}): {Error.Message}";
}
