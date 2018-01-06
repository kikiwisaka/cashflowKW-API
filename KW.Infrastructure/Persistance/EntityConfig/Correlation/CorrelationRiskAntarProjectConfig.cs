using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class CorrelationRiskAntarProjectConfig : EntityTypeConfiguration<CorrelationRiskAntarProject>
    {
        public CorrelationRiskAntarProjectConfig()
        {
            //table
            ToTable("tblCorrelationRiskAntarProjects");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Status).HasColumnName("status");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //foreign key
            Property(x => x.CorrelationRiskAntarSektorId).HasColumnName("correlationRiskAntarSektorId");
            Property(x => x.ProjectIdRow).HasColumnName("projectIdRow");
            Property(x => x.ProjectIdCol).HasColumnName("projectIdCol");
            Property(x => x.CorrelationMatrixId).HasColumnName("correlationMatrixId");
        }
    }
}
