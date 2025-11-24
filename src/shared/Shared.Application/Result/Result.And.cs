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
            return result switch
            {
                ISuccess => other,
                IFailure failure => Result.Failure<T>(failure.Error),
                _ => throw new InvalidOperationException(),
            };
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
