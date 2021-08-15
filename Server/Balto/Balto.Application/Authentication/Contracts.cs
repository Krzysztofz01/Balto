namespace Balto.Application.Authentication
{
    public static class Contracts
    {
        public static class V1
        {
            public class AuthResponse
            {
                public string Token { get; set; }
                public string RefreshToken { get; set; }
            }
        }
    }
}
