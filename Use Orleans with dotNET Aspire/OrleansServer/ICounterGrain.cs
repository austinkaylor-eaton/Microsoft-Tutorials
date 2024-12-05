using Orleans;

namespace OrleansServer;

public interface ICounterGrain: IGrainWithStringKey
{
    ValueTask<int> Get();
    ValueTask<int> Increment();
}