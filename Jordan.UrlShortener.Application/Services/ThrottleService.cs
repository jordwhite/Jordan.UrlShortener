using Jordan.UrlShortener.Application.Configuration;
using Jordan.UrlShortener.Application.Queries;

namespace Jordan.UrlShortener.Application.Services
{
    public class ThrottleService : IThrottleService
    {
        private readonly ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery _urlsByClientIpWithinLastNQuery;
        private readonly IApplicationOptions _applicationOptions;

        private const double OneHourInSeconds = 60 * 60;

        public ThrottleService(
            ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery urlsByClientIpWithinLastNQuery,
            IApplicationOptions applicationOptions
        )
        {
            _urlsByClientIpWithinLastNQuery = urlsByClientIpWithinLastNQuery;
            _applicationOptions = applicationOptions;
        }

        public async Task<bool> ShouldThrottleClientIp(string clientIp)
        {
            var submissionCount = await _urlsByClientIpWithinLastNQuery.CountAll(
                clientIp, OneHourInSeconds
            );

            return submissionCount > _applicationOptions.MaxSubmissionsPerHour;
        }
    }
}
