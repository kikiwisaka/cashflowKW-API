using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class ProjectConfig : EntityTypeConfiguration<Project>
    {
        public ProjectConfig()
        {
            //table
            ToTable("tblProjects");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NamaProject).HasColumnName("namaProject");
            Property(x => x.StatusProject).HasColumnName("statusProject");
            Property(x => x.Minimum).HasColumnName("minimum");
            Property(x => x.Maximum).HasColumnName("maximum");
            Property(x => x.Keterangan).HasColumnName("keterangan");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //property of Foreign keys
            Property(x => x.TahapanId).HasColumnName("tahapanId");
            Property(x => x.SektorId).HasColumnName("sektorId");
            Property(x => x.UserId).HasColumnName("userId");


            //relationship
            HasRequired(x => x.Tahapan).WithMany(x => x.Projects).HasForeignKey(x => x.TahapanId);
            HasRequired(x => x.Sektor).WithMany(x => x.Projects).HasForeignKey(x => x.SektorId);
        }
    }
}
