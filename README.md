![Nuget](https://img.shields.io/nuget/v/WaitWithTimeout)
![GitHub](https://img.shields.io/github/license/ipazooki/WaitWithTimeout)
![GitHub contributors](https://img.shields.io/github/contributors/ipazooki/WaitWithTimeout)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/ipazooki/WaitWithTimeout/dotnet.yml)

## Async Timeout Handling

This project provides methods to handle asynchronous operations with timeout capabilities.

## Overview

This project contains extension methods for handling asynchronous operations with timeouts. The primary purpose of these methods is to provide a way to execute asynchronous tasks while ensuring that they complete within a specified time frame. If the task takes longer than the specified timeout, a `TaskCanceledException` is thrown.

## Usage
You can use this project to wrap your asynchronous tasks with timeout constraints. The class provides the following methods:

`WaitWithTimeoutAsync<T>(this Task<T> task, TimeSpan timeout)`: Wraps an asynchronous task that returns a result with a timeout. If the task completes within the specified timeout, the result is returned. Otherwise, a TimeoutException is thrown.

`WaitWithTimeoutAsync(this Task task, TimeSpan timeout)`: Wraps an asynchronous task without a return value with a timeout. If the task completes within the specified timeout, the execution continues. Otherwise, a TimeoutException is thrown.

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
        catch (TaskCanceledException ex)
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

### Contribution
👍 You are encouraged to contribute to this project by forking it or giving it a star if you find it valuable :)

### Social Media

[![Email](https://img.shields.io/badge/Email-gray?logo=gmail&style=flat-square)](mailto:ipazooki@gmail.com)
[![Stack Overflow](https://img.shields.io/badge/Stackoverflow-gray?logo=stackoverflow&style=flat-square)](https://stackoverflow.com/users/1424065/mrp)
[![Linkedin](https://img.shields.io/badge/-LinkedIn-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/pazooki)](https://www.linkedin.com/in/pazooki/)
![Twitter Follow](https://img.shields.io/twitter/follow/ipazooki)
