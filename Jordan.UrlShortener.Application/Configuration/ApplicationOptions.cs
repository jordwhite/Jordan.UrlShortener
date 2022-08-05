namespace Jordan.UrlShortener.Application.Configuration
{
    public class ApplicationOptions : IApplicationOptions
    {
        public string DefaultRedirectUrl { get; set; }
        public int ShortUrlIdMaxLength { get; set; }
        public int ShortUrlIdMinLength { get; set; }
        public int MaxSubmissionsPerHour { get; set; }
        public int MaxUniqueRandomIdGenerateRetries { get; set; }
    }
}
