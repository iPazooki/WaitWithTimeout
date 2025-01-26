namespace WaitWithTimeout
{
    public static class Utility
    {
        /// <summary>
        /// Waits for the task to complete or times out after a specified time.
        /// </summary>
        /// <typeparam name="T">The type of the result produced by the task.</typeparam>
        /// <param name="task">The task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the task to complete.</param>
        /// <returns>The result produced by the task.</returns>
        /// <exception cref="TaskCanceledException">Thrown if the task does not complete within the specified timeout.</exception>
        public static async Task<T> WaitWithTimeoutAsync<T>(this Task<T> task, TimeSpan timeout) => await WaitWithTimeoutInternalAsync(task, timeout);

        /// <summary>
        /// Waits for the task to complete or times out after a specified time.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the task to complete.</param>
        /// <exception cref="TaskCanceledException">Thrown if the task does not complete within the specified timeout.</exception>
        public static async Task WaitWithTimeoutAsync(this Task task, TimeSpan timeout) => await WaitWithTimeoutInternalAsync(task, timeout);

        /// <summary>
        /// Waits for the value task to complete or times out after a specified time.
        /// </summary>
        /// <param name="task">The value task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the value task to complete.</param>
        /// <exception cref="TaskCanceledException">Thrown if the value task does not complete within the specified timeout.</exception>
        public static async Task WaitWithTimeoutAsync(this ValueTask task, TimeSpan timeout) => await WaitWithTimeoutInternalAsync(task.AsTask(), timeout);

        /// <summary>
        /// Waits for the value task to complete or times out after a specified time.
        /// </summary>
        /// <typeparam name="T">The type of the result produced by the value task.</typeparam>
        /// <param name="task">The value task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the value task to complete.</param>
        /// <returns>The result produced by the value task.</returns>
        /// <exception cref="TaskCanceledException">Thrown if the value task does not complete within the specified timeout.</exception>
        public static async ValueTask<T> WaitWithTimeoutAsync<T>(this ValueTask<T> task, TimeSpan timeout) => await WaitWithTimeoutInternalAsync(task.AsTask(), timeout);

        /// <summary>
        /// Waits for the task to complete or times out after a specified time.
        /// </summary>
        /// <typeparam name="T">The type of the result produced by the task.</typeparam>
        /// <param name="task">The task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the task to complete.</param>
        /// <returns>The result produced by the task.</returns>
        /// <exception cref="TaskCanceledException">Thrown if the task does not complete within the specified timeout.</exception>
        private static async Task<T> WaitWithTimeoutInternalAsync<T>(Task<T> task, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource(timeout);
            return await task.WaitAsync(cts.Token);
        }

        /// <summary>
        /// Waits for the task to complete or times out after a specified time.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="timeout">The maximum time to wait for the task to complete.</param>
        /// <exception cref="TaskCanceledException">Thrown if the task does not complete within the specified timeout.</exception>
        private static async Task WaitWithTimeoutInternalAsync(Task task, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource(timeout);
            await task.WaitAsync(cts.Token);
        }
    }
}