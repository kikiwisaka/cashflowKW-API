using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class ProjectRiskStatusConfig : EntityTypeConfiguration<ProjectRiskStatus>
    {
        public ProjectRiskStatusConfig()
        {
            //table
            ToTable("tblProjectRiskStatus");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.KodeMRisk).HasColumnName("kodeMRisk");
            Property(x => x.NamaCategoryRisk).HasColumnName("namaCategoryRisk");
            Property(x => x.Definisi).HasColumnName("definisi");
            Property(x => x.IsProjectUsed).HasColumnName("isProjectUsed");

            //foreign key
            Property(x => x.ProjectId).HasColumnName("projectId");
            Property(x => x.RiskRegistrasiId).HasColumnName("riskRegistrasiId");
        }
    }
}
