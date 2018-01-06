using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class SektorConfig : EntityTypeConfiguration<Sektor>
    {
        public SektorConfig()
        {
            //table
            ToTable("tblSektors");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NamaSektor).HasColumnName("namaSektor");
            Property(x => x.Minimum).HasColumnName("minimum");
            Property(x => x.Maximum).HasColumnName("maximum");
            Property(x => x.Definisi).HasColumnName("definisi");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //relationship
            HasMany(t => t.Projects).WithRequired(t => t.Sektor).HasForeignKey(t => t.SektorId);
        }
    }
}
