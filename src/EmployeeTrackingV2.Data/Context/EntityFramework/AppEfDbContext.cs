using EmployeeTrackingV2.Entities;
using EmployeeTrackingV2.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrackingV2.Data.Context.EntityFramework
{
    public class AppEfDbContext : DbContext
    {
        public AppEfDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Person> Persons { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Folder> Folders { get; set; }
       
    }
}
