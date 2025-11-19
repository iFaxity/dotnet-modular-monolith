namespace Shared.Application;

/// <summary>
/// Exception thrown when attempting to get the value of a <see cref="IResult"/>
/// where <see cref="IResult.IsFailure"/> is <c>true</c>.
/// </summary>
public class ResultFailedException(IError error)
    : InvalidOperationException($"{ERROR_MESSAGE}: {error.Message}")
{
    private const string ERROR_MESSAGE = "Result failed";

    /// <summary>
    /// The error of the <see cref="IResult"/> that failed.
    /// </summary>
    public IError Error { get; } = error;
}
