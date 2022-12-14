using JWTAuth.Core;

namespace JWTAuth.Entities
{
    public class UserForRegisterDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
    }
}
