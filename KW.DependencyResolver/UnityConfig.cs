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
            container.RegisterType<ITahapanRepository, TahapanRepository>();
            container.RegisterType<ISektorRepository, SektorRepository>();
            container.RegisterType<IStageRepository, StageRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<ILikehoodRepository, LikehoodRepository>();
            container.RegisterType<IAssetDataRepository, AssetDataRepository>();
            container.RegisterType<IRiskRegistrasiRepository, RiskRegistrasiRepository>();
            container.RegisterType<ISubRiskRegistrasiRepository, SubRiskRegistrasiRepository>();
            container.RegisterType<ICorrelationMatrixRepository, CorrelationMatrixRepository>();
            container.RegisterType<ICommentsRepository, CommentsRepository>();
            container.RegisterType<IColorCommentRepository, ColorCommentRepository>();
            container.RegisterType<ILikehoodDetailRepository, LikehoodDetailRepository>();
            container.RegisterType<IProjectRiskRegistrasiRepository, ProjectRiskRegistrasiRepository>();
            container.RegisterType<IScenarioRepository, ScenarioRepository>();
            container.RegisterType<IScenarioDetailRepository, ScenarioDetailRepository>();
            container.RegisterType<IRiskMatrixRepository, RiskMatrixRepository>();
            container.RegisterType<IRiskMatrixStageRepository, RiskMatrixStageRepository>();
            container.RegisterType<ICorrelationRiskAntarSektorRepository, CorrelationRiskAntarSektorRepository>();
            container.RegisterType<ICorrelationRiskAntarProjectRepository, CorrelationRiskAntarProjectRepository>();
            container.RegisterType<IStageTahunRiskMatrixRepository, StageTahunRiskMatrixRepository>();
            container.RegisterType<IMaksimumProjectValueRepository, MaksimumProjectValueRepository>();
            container.RegisterType<IPMNRepository, PMNRepository>();
            container.RegisterType<IRiskMatrixProjectRepository, RiskMatrixProjectRepository>();
            container.RegisterType<ICorrelatedSektorRepository, CorrelatedSektorRepository>();
            container.RegisterType<IMatrixRepository, MatrixRepository>();
            container.RegisterType<IStageTahunRiskMatrixDetailRepository, StageTahunRiskMatrixDetailRepository>();
            container.RegisterType<IProjectRiskStatusRepository, ProjectRiskStatusRepository>();
            container.RegisterType<IOverAllCommentsRepository, OverAllCommentsRepository>();
            container.RegisterType<IFunctionalRiskRepository, FunctionalRiskRepository>();
            container.RegisterType<ICorrelatedSektorDetailRepository, CorrelatedSektorDetailRepository>();
            container.RegisterType<ICorrelatedProjectRepository, CorrelatedProjectRepository>();
            container.RegisterType<ICorrelatedProjectDetailRepository, CorrelatedProjectDetailRepository>();


            //Services
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IUserResetPasswordService, UserResetPasswordService>();
            container.RegisterType<ITahapanService, TahapanService>();
            container.RegisterType<ISektorService, SektorService>();
            container.RegisterType<IStageService, StageService>();
            container.RegisterType<ILikehoodService, LikehoodService>();
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IAssetDataService, AssetDataService>();
            container.RegisterType<IRiskRegistrasiService, RiskRegistrasiService>();
            container.RegisterType<ISubRiskRegistrasiService, SubRiskRegistrasiService>();
            container.RegisterType<ICorrelationMatrixService, CorrelationMatrixService>();
            container.RegisterType<ICommentsService, CommentsService>();
            container.RegisterType<IColorCommentService, ColorCommentService>();
            container.RegisterType<ILikehoodDetailService, LikehoodDetailService>();
            container.RegisterType<IProjectRiskRegistrasiService, ProjectRiskRegistrasiService>();
            container.RegisterType<IScenarioService, ScenarioService>();
            container.RegisterType<IScenarioDetailService, ScenarioDetailService>();
            container.RegisterType<IRiskMatrixService, RiskMatrixService>();
            container.RegisterType<IRiskMatrixStageService, RiskMatrixStageService>();
            container.RegisterType<ICorrelationRiskAntarSektorService, CorrelationRiskAntarSektorService>();
            container.RegisterType<ICorrelationRiskAntarProjectService, CorrelationRiskAntarProjectService>();
            container.RegisterType<IStageTahunRiskMatrixService, StageTahunRiskMatrixService>();
            container.RegisterType<IMaksimumProjectValueService, MaksimumProjectValueService>();
            container.RegisterType<IPMNService, PMNService>();
            container.RegisterType<IRiskMatrixProjectService, RiskMatrixProjectService>();
            container.RegisterType<ICorrelatedSektorService, CorrelatedSektorService>();
            container.RegisterType<IMatrixService, MatrixService>();
            container.RegisterType<IStageTahunRiskMatrixDetailService, StageTahunRiskMatrixDetailService>();
            container.RegisterType<IOverAllCommentsService, OverAllCommentsService>();
            container.RegisterType<IFunctionalRiskService, FunctionalRiskService>();
            container.RegisterType<ICorrelatedSektorDetailService, CorrelatedSektorDetailService>();
            container.RegisterType<IDashboardRiskCapitalProjectService, DashboardRiskCapitalProjectService>();
            container.RegisterType<ICorrelatedProjectService, CorrelatedProjectService>();
            container.RegisterType<ICorrelatedProjectDetailService, CorrelatedProjectDetailService>(); ;


            //Configs
            container.RegisterType<IDatabaseConfiguration, EntityFrameworkConfiguration>();
        }
    }
}
