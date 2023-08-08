namespace WaitWithTimeout;

public static class Utility
{
    private const string ExceptionMessage = "The operation has timed out.";

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

    public static async ValueTask<T> WaitWithTimeoutAsync<T>(this ValueTask<T> task, TimeSpan timeout) => await task.AsTask().WaitWithTimeoutAsync(timeout);
}