using Jordan.UrlShortener.Application.Services;
using Jordan.UrlShortener.Domain.Commands;
using Jordan.UrlShortener.Domain.Commands.Responses;
using Jordan.UrlShortener.Domain.Models;
using MediatR;

namespace Jordan.UrlShortener.Application.CommandHandlers
{
    public class GenerateShortenedUrlCommandHandler : IRequestHandler<
        GenerateShortenedUrlCommand, 
        GenerateShortenedUrlCommandResponse
    >
    {
        private readonly IThrottleService _throttleService;
        private readonly IShorteningService _shorteningService;

        public GenerateShortenedUrlCommandHandler(
            IThrottleService throttleService, 
            IShorteningService shorteningService
        )
        {
            _throttleService = throttleService;
            _shorteningService = shorteningService;
        }

        public async Task<GenerateShortenedUrlCommandResponse> Handle(
            GenerateShortenedUrlCommand request, 
            CancellationToken cancellationToken
        )
        {
            if (await _throttleService.ShouldThrottleClientIp(request.ClientIp))
                return ThrottledResponse;

            return CreateCommandResponse(
                await _shorteningService.Shorten(
                    request.FullUrl, request.ClientIp
                )
            );
        }

        private static GenerateShortenedUrlCommandResponse CreateCommandResponse(ShortenedUrl shortenedUrl) => 
            new(shortenedUrl.Id);

        private static GenerateShortenedUrlCommandResponse ThrottledResponse => new(null);
    }
}
