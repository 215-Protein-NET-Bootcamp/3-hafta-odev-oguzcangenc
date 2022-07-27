using EmployeeTrackingV2.Core.Entities;

namespace EmployeeTrackingV2.Entities
{
    public class Department : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
