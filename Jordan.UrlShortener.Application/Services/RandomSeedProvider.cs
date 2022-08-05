namespace Jordan.UrlShortener.Application.Services
{
    public class RandomSeedProvider : IRandomSeedProvider
    {
        public int Seed => Environment.TickCount;
    }
}
