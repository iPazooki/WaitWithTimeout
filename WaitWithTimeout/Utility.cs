namespace WaitWithTimeout;

public static class Utility
{
    private const string ExceptionMessage = "The operation has timed out.";

    /// <summary>
    /// Waits for the task to complete or times out after a specified time.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the task.</typeparam>
    /// <param name="task">The task to wait for.</param>
    /// <param name="timeout">The maximum time to wait for the task to complete.</param>
    /// <param name="exceptionMessage">The message to include in the exception if the operation times out.</param>
    /// <returns>The result produced by the task.</returns>
    public static async Task<T> WaitWithTimeoutAsync<T>(this Task<T> task, TimeSpan timeout, string exceptionMessage = ExceptionMessage)
    {
        using var cts = new CancellationTokenSource(timeout);

        var completedTask = await Task.WhenAny(task, Task.Delay(-1, cts.Token));

        if (completedTask == task)
        {
            cts.Cancel();

            return await task;
        }
        else
        {
            throw new TimeoutException(exceptionMessage);
        }
    }


    /// <summary>
    /// Waits for the task to complete or times out after a specified time.
    /// </summary>
    /// <param name="task">The task to wait for.</param>
    /// <param name="timeout">The maximum time to wait for the task to complete.</param>
    /// <param name="exceptionMessage">The message to include in the exception if the operation times out.</param>
    public static async Task WaitWithTimeoutAsync(this Task task, TimeSpan timeout, string exceptionMessage = ExceptionMessage)
    {
        using var cts = new CancellationTokenSource(timeout);

        var completedTask = await Task.WhenAny(task, Task.Delay(-1, cts.Token));

        if (completedTask == task)
        {
            cts.Cancel();

            await task;
        }
        else
        {
            throw new TimeoutException(exceptionMessage);
        }
    }

    /// <summary>
    /// Waits for the value task to complete or times out after a specified time.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the value task.</typeparam>
    /// <param name="task">The value task to wait for.</param>
    /// <param name="timeout">The maximum time to wait for the value task to complete.</param>
    public static async ValueTask<T> WaitWithTimeoutAsync<T>(this ValueTask<T> task, TimeSpan timeout) => await task.AsTask().WaitWithTimeoutAsync(timeout);
}