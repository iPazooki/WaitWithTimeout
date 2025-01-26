namespace WaitWithTimeout.Tests;

public class UtilityTests
{
    [Fact]
    public async Task WaitWithTimeoutAsync_TaskCompletesBeforeTimeout_ReturnsResult()
    {
        var task = Task.FromResult(42);
        var result = await task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1));
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_TaskTimesOut_ThrowsTaskCanceledException()
    {
        var task = Task.Delay(TimeSpan.FromSeconds(2));
        await Assert.ThrowsAsync<TaskCanceledException>(() => task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_ValueTaskCompletesBeforeTimeout_ReturnsResult()
    {
        var task = new ValueTask<int>(42);
        var result = await task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1));
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_ValueTaskTimesOut_ThrowsTaskCanceledException()
    {
        var task = new ValueTask(Task.Delay(TimeSpan.FromSeconds(2)));
        await Assert.ThrowsAsync<TaskCanceledException>(() => task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1)));
    }
    
    [Fact]
    public async Task WaitWithTimeoutAsync_TaskCompletesExactlyAtTimeout_ReturnsResult()
    {
        var task = Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ => 42);
        var result = await task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1.1));
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_ValueTaskCompletesExactlyAtTimeout_ReturnsResult()
    {
        var task = new ValueTask<int>(Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ => 42));
        var result = await task.WaitWithTimeoutAsync(TimeSpan.FromSeconds(1.1));
        Assert.Equal(42, result);
    }
}
