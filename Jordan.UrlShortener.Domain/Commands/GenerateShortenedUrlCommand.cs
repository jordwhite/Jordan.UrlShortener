using Jordan.UrlShortener.Domain.Commands.Responses;
using MediatR;

namespace Jordan.UrlShortener.Domain.Commands
{
    public class GenerateShortenedUrlCommand : IRequest<GenerateShortenedUrlCommandResponse>
    {
        public GenerateShortenedUrlCommand(string fullUrl, string clientIp)
        {
            FullUrl = fullUrl;
            ClientIp = clientIp;
        }

        public string FullUrl { get; }
        public string ClientIp { get; }
    }
}
