using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class StageTahunRiskMatrixConfig : EntityTypeConfiguration<StageTahunRiskMatrix>
    {
        public StageTahunRiskMatrixConfig()
        {
            //table
            ToTable("tblStageTahunRiskMatrixs");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Tahun).HasColumnName("tahun");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsUpdate).HasColumnName("isUpdate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //Foreign Key
            Property(x => x.StageId).HasColumnName("stageId");
            Property(x => x.RiskMatrixProjectId).HasColumnName("riskMatrixProjectId");
        }
    }
}
