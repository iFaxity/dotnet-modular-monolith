using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOrElse(Func<T> mapDefault)
        {
            if (!Result.TryUnwrap(result, out var value))
                return mapDefault();

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> UnwrapOrElse(Func<Task<T>> mapDefault)
        {
            if (!Result.TryUnwrap(result, out var value))
                return mapDefault();

            return Task.FromResult(value);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<T> UnwrapOrElse(Func<T> defaultValue)
        {
            var result = await resultTask;

            return result.UnwrapOrElse(defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<T> UnwrapOrElse(Func<Task<T>> defaultValue)
        {
            var result = await resultTask;

            return await result.UnwrapOrElse(defaultValue);
        }
    }
}
