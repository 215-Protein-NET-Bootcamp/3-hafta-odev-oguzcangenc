using JWTAuth.Core;
namespace JWTAuth.Entities
{
    public class ApplicationUser : User, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
