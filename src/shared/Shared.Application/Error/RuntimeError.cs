namespace Shared.Application;

public sealed record RuntimeError : DomainError
{
    private const string ERROR_CODE = nameof(RuntimeError);

    /// <summary>
    /// Initializes a new instance of the <see cref="RuntimeError"/> class.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public RuntimeError(Exception exception)
        : base(ERROR_CODE, exception.Message)
    {
        ArgumentNullException.ThrowIfNull(exception);

        Exception = exception;
    }

    public Exception Exception { get; }

    /// <summary>
    /// Returns a string representation of the <see cref="RuntimeError"/>.
    /// </summary>
    /// <returns>A string representing the error.</returns>
    public override string ToString()
    {
        return $"{ERROR_CODE}: {Message}";
    }
}
