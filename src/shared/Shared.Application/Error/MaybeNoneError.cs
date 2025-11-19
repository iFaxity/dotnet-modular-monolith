namespace Shared.Application;

public sealed record MaybeNoneError : DomainError
{
    private const string ERROR_CODE = nameof(MaybeNoneError);

    public static readonly MaybeNoneError Default = new("The maybe is none.");

    /// <summary>
    /// Initializes a new instance of the <see cref="MaybeNoneError"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public MaybeNoneError(string message)
        : base(ERROR_CODE, message) { }

    /// <summary>
    /// Returns a string representation of the <see cref="MaybeNoneError"/>.
    /// </summary>
    /// <returns>A string representing the error.</returns>
    public override string ToString()
    {
        return $"{ERROR_CODE}: {Message}";
    }
}
