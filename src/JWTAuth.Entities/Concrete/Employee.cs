using JWTAuth.Core;

namespace JWTAuth.Entities
{
    public class Employee:BaseEntity,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
    }
}
