using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        /// <summary>
        /// Gets the result error if the result is a failure, otherwise throws an exception.
        /// </summary>
        /// <returns>The result error if the result is a failure.</returns>
        /// <exception cref="InvalidOperationException"> when <see cref="IsSuccess"/> is <c>true</c>.</exception>
        public IError UnwrapError()
        {
            return result switch
            {
                IFailure failure => failure.Error,
                ISuccess => throw new InvalidOperationException(
                    "The error of a successful result can not be accessed."
                ),
                _ => throw new InvalidOperationException(),
            };
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IError> UnwrapError()
        {
            var result = await resultTask;

            return result.UnwrapError();
        }
    }
}
