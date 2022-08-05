namespace Jordan.UrlShortener.Domain.Commands.Responses
{
    public class GenerateShortenedUrlCommandResponse
    {
        public GenerateShortenedUrlCommandResponse(string id) => 
            Id = id;

        public string Id { get; }
    }
}
