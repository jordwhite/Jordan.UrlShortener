namespace Jordan.UrlShortener.Application.Generators;

public interface IRandomUniqueAlphaNumericIdGenerator
{
    Task<string> Generate();
}