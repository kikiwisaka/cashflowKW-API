using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class CorrelatedProjectDetailConfig : EntityTypeConfiguration<CorrelatedProjectDetail>
    {
        public CorrelatedProjectDetailConfig()
        {
            //table
            ToTable("tblCorrelatedProjectDetails");

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
            Property(x => x.CorrelatedProjectId).HasColumnName("correlatedProjectId");
            Property(x => x.ProjectIdRow).HasColumnName("projectIdRow");
            Property(x => x.ProjectIdCol).HasColumnName("projectIdCol");
            Property(x => x.CorrelationMatrixId).HasColumnName("correlationMatrixId");
        }
    }
}
