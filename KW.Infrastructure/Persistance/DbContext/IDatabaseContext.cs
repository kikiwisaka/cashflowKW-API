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
        IDbSet<Tahapan> Tahapans { get; set; }
        IDbSet<Sektor> Sektors { get; set; }
        IDbSet<Stage> Stages { get; set; }
        IDbSet<Likehood> Likehoods { get; set; }
        IDbSet<Project> Projects{ get; set; }
        IDbSet<AssetData> AssetDatas { get; set; }
        IDbSet<RiskRegistrasi> RiskRegistrasis { get; set; }
        IDbSet<SubRiskRegistrasi> SubRiskRegistrasis { get; set; }
        IDbSet<CorrelationMatrix> CorrelationMatrixs { get; set; }
        IDbSet<Comments> Commentss { get; set; }
        IDbSet<ColorComment> ColorComments { get; set; }
        IDbSet<LikehoodDetail> LikehoodDetails { get; set; }
        IDbSet<ProjectRiskRegistrasi> ProjectRiskRegistrasis { get; set; }
        IDbSet<PMN> PMNs { get; set; }
        IDbSet<Matrix> Matrixs { get; set; }
        IDbSet<ProjectRiskStatus> ProjectRiskStatus { get; set; }
        IDbSet<FunctionalRisk> FunctionalRisks { get; set; }

        //Scenario
        IDbSet<Scenario> Scenarios { get; set; }
        IDbSet<ScenarioDetail> ScenarioDetails { get; set; }

        //RiskMatrix
        IDbSet<RiskMatrix> RiskMatrixs { get; set; }
        IDbSet<RiskMatrixStage> RiskMatrixStages { get; set; }
        IDbSet<StageTahunRiskMatrix> StageTahunRiskMatrixs { get; set; }
        IDbSet<MaksimumProjectValue> MaksimumProjectValues { get; set; }
        IDbSet<RiskMatrixProject> RiskMatrixProjects { get; set; }
        IDbSet<StageTahunRiskMatrixDetail> StageTahunRiskMatrixDetails { get; set; }

        //Correlation
        IDbSet<CorrelationRiskAntarSektor> CorrelationRiskAntarSektors { get; set; }
        IDbSet<CorrelationRiskAntarProject> CorrelationRiskAntarProjects { get; set; }
        IDbSet<CorrelatedSektor> CorrelatedSektors { get; set; }
        IDbSet<CorrelatedSektorDetail> CorrelatedSektorDetails { get; set; }
        IDbSet<CorrelatedProject> CorrelatedProjects { get; set; }
        IDbSet<CorrelatedProjectDetail> CorrelatedProjectDetails { get; set; }


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

        //
        IDbSet<OverAllComments> OverAllCommentss { get; set; }


        int SaveChanges();
        DbEntityEntry Entry(object entity);
        void Dispose();
    }
}
