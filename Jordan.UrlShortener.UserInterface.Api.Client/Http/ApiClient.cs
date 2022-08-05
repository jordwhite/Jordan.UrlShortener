using Jordan.UrlShortener.UserInterface.Api.Client.Requests;
using Jordan.UrlShortener.UserInterface.Api.Client.Responses;
using RestSharp;

namespace Jordan.UrlShortener.UserInterface.Api.Client.Http
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl) => 
            _client = new RestClient(baseUrl);

        public async Task<GenerateShortenedUrlResponse> GenerateShortenedUrl(GenerateShortenedUrlRequest request)
        {
            return await _client.PostJsonAsync<GenerateShortenedUrlRequest, GenerateShortenedUrlResponse>(
                "Generate", request, CancellationToken.None
            );
        }
    }
}
