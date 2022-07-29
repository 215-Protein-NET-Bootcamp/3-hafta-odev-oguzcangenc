using JWTAuth.Core;
namespace JWTAuth.Entities
{
    public class ApplicationUser : User, IEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
