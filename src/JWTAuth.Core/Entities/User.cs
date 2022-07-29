namespace JWTAuth.Core
{
    public class User : BaseEntity, IEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
