namespace Jordan.UrlShortener.Application.Services;

public interface IUrlRetrievalService
{
    Task<string> Retrieve(string shortenedUrlId);
}