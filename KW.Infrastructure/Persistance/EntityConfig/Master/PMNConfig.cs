using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class PMNConfig : EntityTypeConfiguration<PMN>
    {
        public PMNConfig()
        {
            //table
            ToTable("tblPMNs");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PMNToModalDasarCap).HasColumnName("pmnToModalDasarCap");
            Property(x => x.RecourseDelay).HasColumnName("recourseDelay");
            Property(x => x.DelayYears).HasColumnName("delayYears");
            Property(x => x.OpexGrowth).HasColumnName("opexGrowth");
            Property(x => x.Opex).HasColumnName("opex");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");
            Property(x => x.Status).HasColumnName("status");
            Property(x => x.ValuePMNToModalDasarCap).HasColumnName("valuePMNToModalDasarCap");
        }
    }
}
