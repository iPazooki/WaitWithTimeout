## Async Timeout Handling

This project provides methods to handle asynchronous operations with timeout capabilities.

## Overview

This project contains extension methods for handling asynchronous operations with timeouts. The primary purpose of these methods is to provide a way to execute asynchronous tasks while ensuring that they complete within a specified time frame. If the task takes longer than the specified timeout, a `TimeoutException` is thrown.

## Usage
You can use this project to wrap your asynchronous tasks with timeout constraints. The class provides the following methods:

`WaitWithTimeoutAsync<T>(this Task<T> task, TimeSpan timeout, string exceptionMessage = "The operation has timed out.")`: Wraps an asynchronous task that returns a result with a timeout. If the task completes within the specified timeout, the result is returned. Otherwise, a TimeoutException is thrown.

`WaitWithTimeoutAsync(this Task task, TimeSpan timeout, string exceptionMessage = "The operation has timed out.")`: Wraps an asynchronous task without a return value with a timeout. If the task completes within the specified timeout, the execution continues. Otherwise, a TimeoutException is thrown.

`WaitWithTimeoutAsync<T>(this ValueTask<T> task, TimeSpan timeout)`: Wraps a ValueTask<T> with a timeout. This method internally converts the ValueTask<T> to a regular Task<T> and uses the first method mentioned for timeout handling.

Here's an example of how to use the methods:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var timeout = TimeSpan.FromSeconds(5);

        try
        {
            var result = await SomeAsyncOperation().WaitWithTimeoutAsync(timeout);
            Console.WriteLine("Operation succeeded: " + result);
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine("Operation timed out: " + ex.Message);
        }
    }

    static async Task<int> SomeAsyncOperation()
    {
        await Task.Delay(TimeSpan.FromSeconds(3)); // Simulating an async operation
        return 42;
    }
}
```

## Contributions

Contributions to this project are welcome! If you find any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request.

## License

This utility class is provided under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).
