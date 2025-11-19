using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull(nameof(defaultValue))]
        public T? UnwrapOr(T? defaultValue)
        {
            if (!Result.TryUnwrap(result, out var value))
                return defaultValue;

            return value;
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull(nameof(defaultValue))]
        public async Task<T?> UnwrapOr(T? defaultValue)
        {
            var result = await resultTask;

            return result.UnwrapOr(defaultValue);
        }
    }
}
