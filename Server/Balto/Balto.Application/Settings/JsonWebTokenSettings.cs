namespace Balto.Application.Settings
{
    public class JsonWebTokenSettings
    {
        public string TokenSecret { get; }
        public int TokenExpirationInMinutes { get; }
    }
}
