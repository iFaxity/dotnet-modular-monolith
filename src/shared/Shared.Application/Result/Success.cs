namespace Shared.Application;

internal sealed class Success : ISuccess
{
    public bool IsSuccess => true;
    public bool IsFailure => false;

    /// <summary>
    /// Returns a string representation of the <see cref="Success"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => "Success";
}
