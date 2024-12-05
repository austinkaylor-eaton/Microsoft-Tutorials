namespace OrleansGrainInterfaces;

[Alias("OrleansGrainInterfaces.IHelloGrain")]
public interface IHelloGrain : IGrainWithIntegerKey
{
    [Alias("SayHello")]
    ValueTask<string> SayHello(string greeting);
}