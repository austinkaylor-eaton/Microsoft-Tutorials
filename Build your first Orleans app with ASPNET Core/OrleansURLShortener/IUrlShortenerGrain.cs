namespace OrleansURLShortener;

[Alias("OrleansURLShortener.IUrlShortenerGrain")]
public interface IUrlShortenerGrain : IGrainWithStringKey
{
    [Alias("SetUrl")]
    Task SetUrl(string fullUrl);

    [Alias("GetUrl")]
    Task<string> GetUrl();
}