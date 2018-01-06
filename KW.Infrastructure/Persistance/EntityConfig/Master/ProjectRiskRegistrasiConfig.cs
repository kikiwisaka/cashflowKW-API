using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class ProjectRiskRegistrasiConfig : EntityTypeConfiguration<ProjectRiskRegistrasi>
    {
        public ProjectRiskRegistrasiConfig()
        {
            //table
            ToTable("tblProjectRiskRegistrasis");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //foreign key
            Property(x => x.ProjectId).HasColumnName("projectId");
            Property(x => x.RiskRegistrasiId).HasColumnName("riskRegistrasiId");

            //relationship
            HasRequired(x => x.Project).WithMany(p => p.ProjectRiskRegistrasi).HasForeignKey(f => f.ProjectId);
        }
    }
}
