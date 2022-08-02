using JWTAuth.Core;


namespace JWTAuth.Entities
{
    public class UserForChangePasswordDto:IDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
