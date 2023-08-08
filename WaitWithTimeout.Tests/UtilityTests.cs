namespace WaitWithTimeout.Tests;

public class UtilityTests
{
    [Fact]
    public async Task WaitWithTimeoutAsync_ShouldThrowTimeoutException_WhenTaskIsNotCompletedWithinTimeout()
    {
        // Arrange
        var timeout = TimeSpan.FromSeconds(1);
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(() => task.WaitWithTimeoutAsync(timeout));
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_ShouldReturnResult_WhenTaskIsCompletedWithinTimeout()
    {
        // Arrange
        var timeout = TimeSpan.FromSeconds(5);
        var expected = 42;
        var task = Task.FromResult(expected);

        // Act
        var actual = await task.WaitWithTimeoutAsync(timeout);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task WaitWithTimeoutAsync_ShouldReturnResult_WhenValueTaskIsCompletedWithinTimeout()
    {
        // Arrange
        var timeout = TimeSpan.FromSeconds(5);
        var expected = 42;
        var task = new ValueTask<int>(expected);

        // Act
        var actual = await task.WaitWithTimeoutAsync(timeout);

        // Assert
        Assert.Equal(expected, actual);
    }
}
