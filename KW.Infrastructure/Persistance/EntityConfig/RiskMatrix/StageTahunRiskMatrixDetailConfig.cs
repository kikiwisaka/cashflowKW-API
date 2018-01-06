using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class StageTahunRiskMatrixDetailConfig : EntityTypeConfiguration<StageTahunRiskMatrixDetail>
    {
        public StageTahunRiskMatrixDetailConfig()
        {
            //table
            ToTable("tblStageTahunRiskMatrixDetails");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NilaiExpose).HasColumnName("nilaiExpose");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsUpdate).HasColumnName("isUpdate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //Foreign Key
            Property(x => x.StageTahunRiskMatrixId).HasColumnName("stageTahunRiskMatrixId");
            Property(x => x.RiskRegistrasiId).HasColumnName("riskRegistrasiId");
            Property(x => x.LikehoodDetailId).HasColumnName("likehoodDetailId");
            Property(x => x.RiskMatrixProjectId).HasColumnName("riskMatrixProjectId");
        }
    }
}
