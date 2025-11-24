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
            return result switch
            {
                ISuccess => result,
                IFailure failure => throw new ResultFailedException(failure.Error),
                _ => throw new InvalidOperationException(),
            };
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
