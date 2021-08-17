namespace Balto.Application.Authentication
{
    public static class Commands
    {
        public static class V1
        {
            public class UserRegister
            {
                public string Name { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string PasswordRepate { get; set; }
            }

            public class UserLogin
            {
                public string Email { get; set; }
                public string Password { get; set; }
            }

            public class UserLogout
            {
                public string Token { get; set; }
            }

            public class UserRefresh
            {
                public string Token { get; set; }
            }

            public class UserResetPassword
            {
                public string Password { get; set; }
                public string PasswordRepeat { get; set; }
            }

            public class AuthResponse
            {
                public string Token { get; set; }
                public string RefreshToken { get; set; }
            }
        }
    }
}
