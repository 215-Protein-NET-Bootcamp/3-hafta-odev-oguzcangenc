using EmployeeTrackingV2.Core.Entities;

namespace EmployeeTrackingV2.Entities
{
    public class Folder : BaseEntity, IEntity
    {
        public string AccessType { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
