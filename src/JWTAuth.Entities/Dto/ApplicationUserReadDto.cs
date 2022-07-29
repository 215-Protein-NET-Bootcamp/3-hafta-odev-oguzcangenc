using JWTAuth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Entities
{
    public class ApplicationUserReadDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
