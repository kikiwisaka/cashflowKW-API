using KW.Domain;
using KW.Infrastructure.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KW.Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        //Master Data
        public IDbSet<Budget> Budgets { get; set; }


        //User
        public IDbSet<API> APIs { get; set; }
        public IDbSet<APIMenu> APIMenus { get; set; }
        public IDbSet<Menu> Menus { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<RoleAccess> RoleAccesses { get; set; }
        public IDbSet<RoleEmployeeType> RoleEmployeeTypes { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<UserResetPassword> UserResetPasswords { get; set; }


        public DatabaseContext() : base("KWConnString")
        {
            //Configuration.LazyLoadingEnabled = true;
        }
        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Master
            modelBuilder.Configurations.Add(new BudgetConfig());

            //User
            modelBuilder.Configurations.Add(new APIConfig());
            modelBuilder.Configurations.Add(new APIMenuListConfig());
            modelBuilder.Configurations.Add(new MenuConfig());
            modelBuilder.Configurations.Add(new RoleAccesssConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new RoleEmployeeTypeConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new UseRoleConfig());
            modelBuilder.Configurations.Add(new UseResetPasswordConfig());

            base.OnModelCreating(modelBuilder);
        }


    }
}
