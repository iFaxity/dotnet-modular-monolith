using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IMaybe{T}"/> type.
/// </summary>
public static partial class MaybeExtensions
{
    extension<T>(IMaybe<T> maybe)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<T> WithDefault(Func<T> selector) =>
            maybe.IsSome ? maybe : Maybe.From(selector());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<T> WithDefault(T value) => maybe.IsSome ? maybe : Maybe.From(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> WithDefault(Func<Task<T>> selector) =>
            maybe.IsSome ? maybe : Maybe.From(await selector());
    }
}
