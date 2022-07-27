using EmployeeTrackingV2.Core.Entities;

namespace EmployeeTrackingV2.Entities
{
    public class Country : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Currency { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
