namespace Jordan.UrlShortener.Domain.Commands.Responses
{
    public class GoToShortenedUrlCommandResponse
    {
        public GoToShortenedUrlCommandResponse(string redirectUrl) => 
            RedirectUrl = redirectUrl;

        public string RedirectUrl { get; }
    }
}
