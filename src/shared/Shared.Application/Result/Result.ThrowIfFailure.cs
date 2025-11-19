using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        /// <summary>
        /// Throws a <see cref="ResultFailedException"/> if the <see cref="IResult"/> is a <c>Failure</c>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult ThrowIfFailure()
        {
            if (!Result.TryUnwrapError(result, out var error))
                return result;

            throw new ResultFailedException(error);
        }
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        /// <summary>
        /// Throws a <see cref="ResultFailedException"/> if the <see cref="IResult"/> is a <c>Failure</c>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> ThrowIfFailure()
        {
            var result = await resultTask;

            return result.ThrowIfFailure();
        }
    }
}
