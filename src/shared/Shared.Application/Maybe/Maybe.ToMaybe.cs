using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IMaybe{T}"/> type.
/// </summary>
public static partial class MaybeExtensions
{
    extension<T>(T? obj)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<T> ToMaybe() => Maybe.From(obj);
    }

    extension<T>(Task<T?> task)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> ToMaybe()
        {
            var obj = await task;

            return Maybe.From(obj);
        }
    }
}
