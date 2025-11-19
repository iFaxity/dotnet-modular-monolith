namespace Shared.Application;

public static class ErrorExtensions
{
    extension(IError error)
    {
        public Exception? GetException() =>
            error is RuntimeError runtimeError ? runtimeError.Exception : null;
    }
}
