namespace Shared.Application;

/// <summary>
/// Thrown when attempting to instantiate a <see cref="Maybe{T}"/> from an empty value.
/// </summary>
public class MaybeNoneException() : InvalidOperationException(ERROR_MESSAGE)
{
    private const string ERROR_MESSAGE = "The value of a None Maybe can not be accessed.";
}
