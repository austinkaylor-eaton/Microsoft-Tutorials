using Microsoft.Extensions.Logging;
using OrleansGrainInterfaces;

namespace OrleansGrains;

public class HelloGrain(ILogger<HelloGrain> logger) : Grain, IHelloGrain
{
    private readonly ILogger _logger = logger;

    ValueTask<string> IHelloGrain.SayHello(string greeting)
    {
        _logger.LogInformation("SayHello message received: greeting = \"{Greeting}\"",
            greeting);
        
        return ValueTask.FromResult($"\nClient said: \"{greeting}\", so HelloGrain says: Hello!");
    }
}