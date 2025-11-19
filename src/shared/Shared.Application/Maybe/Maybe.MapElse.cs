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
        public IMaybe<T> MapElse(T? value) => maybe.Match(maybe, () => Maybe.From(value));

        public IMaybe<T> MapElse(Func<T?> map)
        {
            IMaybe<T> mapNone()
            {
                var mappedValue = map();

                return Maybe.From(mappedValue);
            }

            return maybe.Match(maybe, mapNone);
        }

        public Task<IMaybe<T>> MapElse(Task<T?> value)
        {
            async Task<IMaybe<T>> mapNone()
            {
                var mappedValue = await value;

                return Maybe.From(mappedValue);
            }

            return maybe.Match(maybe, mapNone);
        }

        public Task<IMaybe<T>> MapElse(Func<Task<T?>> map)
        {
            async Task<IMaybe<T>> mapNone()
            {
                var mappedValue = await map();

                return Maybe.From(mappedValue);
            }

            return maybe.Match(maybe, mapNone);
        }
    }
}
