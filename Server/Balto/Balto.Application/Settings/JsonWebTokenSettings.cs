namespace Balto.Application.Settings
{
    public class JsonWebTokenSettings
    {
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}
