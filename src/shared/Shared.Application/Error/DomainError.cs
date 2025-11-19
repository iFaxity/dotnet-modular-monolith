using System.Diagnostics.CodeAnalysis;

namespace Shared.Application;

/// <summary>
/// Represents an error of some operation, with an error code and message.
/// </summary>
public record DomainError : IError
{
    /// <summary>
    /// Empty error instance
    /// </summary>
    public static readonly IError None = new DomainError();

    private DomainError()
    {
        Code = string.Empty;
        Message = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainError"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    public DomainError(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        Code = code;
        Message = message;
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }

    public static bool IsNullOrNone([NotNullWhen(false)] IError? error)
    {
        return error is null || error == None;
    }

    public virtual bool Equals(DomainError? other)
    {
        return Code == other?.Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    /// <summary>
    /// Returns a string representation of the <see cref="DomainError"/>.
    /// </summary>
    /// <returns>A string representing the error code.</returns>
    public override string ToString()
    {
        if (IsNullOrNone(this))
            return "Error(None)";

        return $"Error({Code}): {Message}";
    }
}
