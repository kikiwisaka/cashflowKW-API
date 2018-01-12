using KW.Application;
using KW.Core;
using KW.Domain;
using KW.Infrastructure;
using KW.Infrastructure.Repositories;
using Microsoft.Practices.Unity;
using System.Web;

namespace KW.DependencyResolver
{
    public class UnityConfig
    {
        public static void RegisterComponents(UnityContainer container)
        {
            //Context life management
            if (HttpContext.Current != null)
            {
                container.RegisterType<IDatabaseContext, DatabaseContext>(new PerHttpRequestLifetimeManager());
            }
            else
            {
                container.RegisterType<IDatabaseContext, DatabaseContext>(new ContainerControlledLifetimeManager());
            }

            //UoW
            container.RegisterType<IUnitOfWork, KW.Infrastructure.UnitOfWork>();

            //Repositories
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserRoleRepository, UserRoleRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IUserResetPasswordRepository, UserResetPasswordRepository>();
            container.RegisterType<IBudgetRepository, BudgetRepository>();

            //Services
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IUserResetPasswordService, UserResetPasswordService>();
            container.RegisterType<IBudgetService, BudgetService>();


            //Configs
            container.RegisterType<IDatabaseConfiguration, EntityFrameworkConfiguration>();
        }
    }
}
