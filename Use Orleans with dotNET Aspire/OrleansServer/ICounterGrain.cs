namespace OrleansServer;

public interface ICounterGrain
{
    ValueTask<int> Get();
    ValueTask<int> Increment();
}