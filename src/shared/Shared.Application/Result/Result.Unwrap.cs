using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        /// <summary>
        /// Gets the result value if the result is successful, otherwise throws an exception.
        /// </summary>
        /// <returns>The result value if the result is successful.</returns>
        /// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is <c>true</c>.</exception>
        public T Unwrap()
        {
            return result switch
            {
                ISuccess<T> success => success.Value,
                IFailure => throw new InvalidOperationException(
                    "The value of a failure result can not be accessed."
                ),
                _ => throw new InvalidOperationException(),
            };
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<T> Unwrap()
        {
            var result = await resultTask;

            return result.Unwrap();
        }
    }
}
