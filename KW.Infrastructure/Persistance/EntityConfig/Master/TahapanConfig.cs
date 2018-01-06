using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class TahapanConfig : EntityTypeConfiguration<Tahapan>
    {
        public TahapanConfig()
        {
            //table
            ToTable("tblTahapans");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NamaTahapan).HasColumnName("namaTahapan");
            Property(x => x.Keterangan).HasColumnName("keterangan");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //relationship
            HasMany(t => t.Projects).WithRequired(t => t.Tahapan).HasForeignKey(t => t.TahapanId);
        }
    }
}
