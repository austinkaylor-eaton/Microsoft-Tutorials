using Orleans.Runtime;

namespace OrleansServer;

public sealed class CounterGrain(
    [PersistentState("count")] IPersistentState<int> count) : ICounterGrain
{
    public ValueTask<int> Get()
    {
        return ValueTask.FromResult(count.State);
    }

    public async ValueTask<int> Increment()
    {
        int result = ++count.State;
        await count.WriteStateAsync();
        return result;
    }
}