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
            if (result is not ISuccess<T> success)
                return mapDefault();

            return success.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> UnwrapOrElse(Func<Task<T>> mapDefault)
        {
            if (result is not ISuccess<T> success)
                return mapDefault();

            return Task.FromResult(success.Value);
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
