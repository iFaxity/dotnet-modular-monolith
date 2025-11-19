using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Or(TResult other) => Match(result, result, other);
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Or(TResult other)
        {
            var result = await resultTask;

            return result.Or(other);
        }
    }
}
