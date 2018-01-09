using KW.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure
{
    public interface IDatabaseContext
    {
        //Master Data
        IDbSet<Sektor> Sektors { get; set; }
        IDbSet<Budget> Budgets { get; set; }


        //User
        IDbSet<API> APIs { get; set; }
        IDbSet<APIMenu> APIMenus { get; set; }
        IDbSet<Menu> Menus { get; set; }
        IDbSet<Role> Roles { get; set; }
        IDbSet<RoleAccess> RoleAccesses { get; set; }
        IDbSet<RoleEmployeeType> RoleEmployeeTypes { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<UserRole> UserRoles { get; set; }
        IDbSet<UserResetPassword> UserResetPasswords { get; set; }


        int SaveChanges();
        DbEntityEntry Entry(object entity);
        void Dispose();
    }
}
