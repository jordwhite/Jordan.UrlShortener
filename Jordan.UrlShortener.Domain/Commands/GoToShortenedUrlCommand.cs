using Jordan.UrlShortener.Domain.Commands.Responses;
using MediatR;

namespace Jordan.UrlShortener.Domain.Commands
{
    public class GoToShortenedUrlCommand : IRequest<GoToShortenedUrlCommandResponse>
    {
        public GoToShortenedUrlCommand(string id) => 
            Id = id;

        public string Id { get; }
    }
}
