using KW.Domain;
using KW.Infrastructure.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KW.Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        //Master Data
        public IDbSet<Tahapan> Tahapans { get; set; }
        public IDbSet<Sektor> Sektors { get; set; }
        public IDbSet<Stage> Stages { get; set; }
        public IDbSet<Project> Projects{ get; set; }
        public IDbSet<Likehood> Likehoods { get; set; }
        public IDbSet<AssetData> AssetDatas { get; set; }
        public IDbSet<RiskRegistrasi> RiskRegistrasis { get; set; }
        public IDbSet<SubRiskRegistrasi> SubRiskRegistrasis { get; set; }
        public IDbSet<CorrelationMatrix> CorrelationMatrixs { get; set; }
        public IDbSet<Comments> Commentss { get; set; }
        public IDbSet<ColorComment> ColorComments { get; set; }
        public IDbSet<LikehoodDetail> LikehoodDetails { get; set; }
        public IDbSet<ProjectRiskRegistrasi> ProjectRiskRegistrasis { get; set; }
        public IDbSet<PMN> PMNs { get; set; }
        public IDbSet<Matrix> Matrixs { get; set; }
        public IDbSet<ProjectRiskStatus> ProjectRiskStatus { get; set; }
        public IDbSet<FunctionalRisk> FunctionalRisks { get; set; }

        //Scenario
        public IDbSet<Scenario> Scenarios { get; set; }
        public IDbSet<ScenarioDetail> ScenarioDetails { get; set; }

        //RiskMatrix
        public IDbSet<RiskMatrix> RiskMatrixs { get; set; }
        public IDbSet<RiskMatrixStage> RiskMatrixStages { get; set; }
        public IDbSet<StageTahunRiskMatrix> StageTahunRiskMatrixs { get; set; }
        public IDbSet<MaksimumProjectValue> MaksimumProjectValues { get; set; }
        public IDbSet<RiskMatrixProject> RiskMatrixProjects { get; set; }
        public IDbSet<StageTahunRiskMatrixDetail> StageTahunRiskMatrixDetails { get; set; }

        //Correlation
        public IDbSet<CorrelationRiskAntarSektor> CorrelationRiskAntarSektors { get; set; }
        public IDbSet<CorrelationRiskAntarProject> CorrelationRiskAntarProjects { get; set; }
        public IDbSet<CorrelatedSektor> CorrelatedSektors { get; set; }
        public IDbSet<CorrelatedSektorDetail> CorrelatedSektorDetails { get; set; }
        public IDbSet<CorrelatedProject> CorrelatedProjects { get; set; }
        public IDbSet<CorrelatedProjectDetail> CorrelatedProjectDetails { get; set; }


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

        //OverAllComment
        public IDbSet<OverAllComments> OverAllCommentss { get; set; }


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
            modelBuilder.Configurations.Add(new TahapanConfig());
            modelBuilder.Configurations.Add(new StageConfig());
            modelBuilder.Configurations.Add(new SektorConfig());
            modelBuilder.Configurations.Add(new LikehoodConfig());
            modelBuilder.Configurations.Add(new ProjectConfig());
            modelBuilder.Configurations.Add(new AssetDataConfig());
            modelBuilder.Configurations.Add(new RiskRegistrasiConfig());
            modelBuilder.Configurations.Add(new SubRiskRegistrasiConfig());
            modelBuilder.Configurations.Add(new CorrelationMatrixConfig());
            modelBuilder.Configurations.Add(new CommentsConfig());
            modelBuilder.Configurations.Add(new ColorCommentConfig());
            modelBuilder.Configurations.Add(new LikehoodDetailConfig());
            modelBuilder.Configurations.Add(new ProjectRiskRegistrasiConfig());
            modelBuilder.Configurations.Add(new PMNConfig());
            modelBuilder.Configurations.Add(new MatrixConfig());
            modelBuilder.Configurations.Add(new ProjectRiskStatusConfig());
            modelBuilder.Configurations.Add(new FunctionalRiskConfig());

            //Scenario
            modelBuilder.Configurations.Add(new ScenarioConfig());
            modelBuilder.Configurations.Add(new ScenarioDetailConfig());

            //RiskMatrix
            modelBuilder.Configurations.Add(new RiskMatrixConfig());
            modelBuilder.Configurations.Add(new RiskMatrixStageConfig());
            modelBuilder.Configurations.Add(new StageTahunRiskMatrixConfig());
            modelBuilder.Configurations.Add(new MaksimumProjectValueConfig());
            modelBuilder.Configurations.Add(new RiskMatrixProjectConfig());
            modelBuilder.Configurations.Add(new StageTahunRiskMatrixDetailConfig());

            //Correlation
            modelBuilder.Configurations.Add(new CorrelationRiskAntarSektorConfig());
            modelBuilder.Configurations.Add(new CorrelationRiskAntarProjectConfig());
            modelBuilder.Configurations.Add(new CorrelatedSektorConfig());
            modelBuilder.Configurations.Add(new CorrelatedSektorDetailConfig());
            modelBuilder.Configurations.Add(new CorrelatedProjectConfig());
            modelBuilder.Configurations.Add(new CorrelatedProjectDetailConfig());


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

            //OverAllComment
            modelBuilder.Configurations.Add(new OverAllCommentsConfig());

            base.OnModelCreating(modelBuilder);
        }


    }
}
