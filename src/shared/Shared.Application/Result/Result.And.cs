using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> And<T>(IResult<T> other)
            where T : notnull
        {
            if (!Result.TryUnwrapError(result, out var error))
                return other;

            return Result.Failure<T>(error);
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> And<T>(IResult<T> other)
            where T : notnull
        {
            var result = await resultTask;

            return result.And(other);
        }
    }
}
