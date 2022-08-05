using Jordan.UrlShortener.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jordan.UrlShortener.UserInterface.Api.Controllers
{
    [ApiController]
    [Route("Go")]
    public class GoToController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GoToController> _logger;

        public GoToController(IMediator mediator, ILogger<GoToController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        public async Task<RedirectResult> GoTo(string id)
        {
            var commandResponse = await _mediator.Send(new GoToShortenedUrlCommand(id));

            _logger.LogDebug($"Redirecting from shortened URL {id} to {commandResponse.RedirectUrl}.");
            return RedirectPermanent(commandResponse.RedirectUrl);
        }
    }
}