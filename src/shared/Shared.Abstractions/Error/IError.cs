namespace Shared.Abstractions;

/// <summary>
/// Represents an error of some operation, with an error code and message.
/// </summary>
public interface IError
{

    /// <summary>
    /// Gets the error code.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    string Message { get; }
}
