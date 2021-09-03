using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Authentication
{
    public static class Commands
    {
        public static class V1
        {
            public class UserRegister
            {
                [Required]
                public string Name { get; set; }

                [Required]
                public string Email { get; set; }

                [Required]
                public string Password { get; set; }

                [Required]
                public string PasswordRepeat { get; set; }
            }

            public class UserLogin
            {
                [Required]
                public string Email { get; set; }

                [Required]
                public string Password { get; set; }
            }

            public class UserLogout
            {
            }

            public class UserRefresh
            {
            }

            public class UserResetPassword
            {
                [Required]
                public string Password { get; set; }

                [Required]
                public string PasswordRepeat { get; set; }
            }

            public class AuthResponse
            {
                public string Token { get; set; }
            }
        }
    }
}
