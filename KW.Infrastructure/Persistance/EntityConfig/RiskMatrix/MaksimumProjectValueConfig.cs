using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class MaksimumProjectValueConfig : EntityTypeConfiguration<MaksimumProjectValue>
    {
        public MaksimumProjectValueConfig()
        {
            //table
            ToTable("tblMaksimumProjectValues");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Tahun).HasColumnName("tahun");
            Property(x => x.NilaiMaximum).HasColumnName("nilaiMaximum");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //Foreign Key
            Property(x => x.ScenarioId).HasColumnName("scenarioId");
            Property(x => x.ProjectId).HasColumnName("projectId");
        }
    }
}
