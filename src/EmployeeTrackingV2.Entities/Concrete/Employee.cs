using EmployeeTrackingV2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrackingV2.Entities
{
    public class Employee : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }

    }
}
