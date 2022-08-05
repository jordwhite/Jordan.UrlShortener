using Jordan.UrlShortener.Application.Configuration;
using Jordan.UrlShortener.Application.Services;
using Jordan.UrlShortener.Domain.Commands;
using Jordan.UrlShortener.Domain.Commands.Responses;
using MediatR;

namespace Jordan.UrlShortener.Application.CommandHandlers
{
    public class GoToShortenedUrlCommandHandler : IRequestHandler<
        GoToShortenedUrlCommand, 
        GoToShortenedUrlCommandResponse
    >
    {
        private readonly IUrlRetrievalService _urlRetrievalService;
        private readonly IApplicationOptions _applicationOptions;

        public GoToShortenedUrlCommandHandler(
            IUrlRetrievalService urlRetrievalService, 
            IApplicationOptions applicationOptions
        )
        {
            _urlRetrievalService = urlRetrievalService;
            _applicationOptions = applicationOptions;
        }

        public async Task<GoToShortenedUrlCommandResponse> Handle(
            GoToShortenedUrlCommand request, 
            CancellationToken cancellationToken
        )
        {
            var redirectUrl = await _urlRetrievalService.Retrieve(request.Id);
            return new GoToShortenedUrlCommandResponse(redirectUrl ?? _applicationOptions.DefaultRedirectUrl);
        }
    }
}
