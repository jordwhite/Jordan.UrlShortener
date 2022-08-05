using System.Net.Mime;
using Jordan.UrlShortener.UserInterface.Api.Client.Requests;
using Jordan.UrlShortener.UserInterface.Api.Client.Responses;
using Jordan.UrlShortener.UserInterface.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jordan.UrlShortener.UserInterface.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GenerateController> _logger;

        public GenerateController(IMediator mediator, ILogger<GenerateController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenerateShortenedUrlResponse>> Generate(GenerateShortenedUrlRequest request)
        {
            _logger.LogDebug($"Creating a shortened URL for full URL: {request.FullUrl}.");
            var response = (await _mediator.Send(request.ToCommand(Request))).ToClientResponse();

            return response.Id != null ? Ok(response) : StatusCode(429, response);
        }
    }
}