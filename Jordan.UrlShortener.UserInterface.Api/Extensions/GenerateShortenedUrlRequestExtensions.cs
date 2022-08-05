using Jordan.UrlShortener.Domain.Commands;
using Jordan.UrlShortener.UserInterface.Api.Client.Requests;

namespace Jordan.UrlShortener.UserInterface.Api.Extensions
{
    public static class GenerateShortenedUrlRequestExtensions
    {
        public static GenerateShortenedUrlCommand ToCommand(
            this GenerateShortenedUrlRequest request,
            HttpRequest httpRequest
        ) =>
            new(request.FullUrl, GetIpAddress(httpRequest));

        private static string GetIpAddress(HttpRequest httpRequest) =>
            httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}
