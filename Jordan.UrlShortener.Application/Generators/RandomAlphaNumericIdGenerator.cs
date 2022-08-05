using System.Text;
using Jordan.UrlShortener.Application.Configuration;
using Jordan.UrlShortener.Application.Services;

namespace Jordan.UrlShortener.Application.Generators
{
    public class RandomAlphaNumericIdGenerator : IRandomAlphaNumericIdGenerator
    {
        private readonly IApplicationOptions _applicationOptions;

        private const string CharTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                         "abcdefghijklmnopqrstuvwxyz" +
                                         "0123456789";

        private readonly Random _random;
        private readonly object _locker = new();

        private int GetRandomLength()
        {
            lock (_locker)
                return _random.Next(
                    _applicationOptions.ShortUrlIdMinLength,
                    _applicationOptions.ShortUrlIdMaxLength + 1
                );
        }

        public RandomAlphaNumericIdGenerator(
            IRandomSeedProvider randomSeedProvider, 
            IApplicationOptions applicationOptions
        )
        {
            _random = new Random(randomSeedProvider.Seed);
            _applicationOptions = applicationOptions;
        }

        public string Generate() =>
            GenerateRandomAlphaNumericString(GetRandomLength());

        private string GenerateRandomAlphaNumericString(int length)
        {
            var sb = new StringBuilder(length);

            for (var i = 0; i < length; i++)
                sb.Append(GetRandomChar());

            return sb.ToString();
        }

        private char GetRandomChar() =>
            CharTable[_random.Next(CharTable.Length)];
    }
}
