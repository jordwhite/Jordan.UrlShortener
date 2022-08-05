using Jordan.UrlShortener.Domain.Commands.Responses;
using Jordan.UrlShortener.UserInterface.Api.Client.Responses;

namespace Jordan.UrlShortener.UserInterface.Api.Extensions
{
    public static class GenerateShortenedUrlCommandResponseExtensions
    {
        public static GenerateShortenedUrlResponse ToClientResponse(
            this GenerateShortenedUrlCommandResponse commandResponse
        ) =>
            new()
            {
                Id = commandResponse.Id
            };
    }
}
