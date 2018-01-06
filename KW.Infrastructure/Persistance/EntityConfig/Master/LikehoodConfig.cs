using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class LikehoodConfig : EntityTypeConfiguration<Likehood>
    {
        public LikehoodConfig()
        {
            //table
            ToTable("tblLikehoods");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NamaLikehood).HasColumnName("namaLikehood");
            Property(x => x.Incres).HasColumnName("incres");
            Property(x => x.Status).HasColumnName("status");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");

            //HasMany(x => x.LikehoodDetails).WithMany(e => e.Id).Map({ xe => xe.MapLeftKey("LikehoodDetailId"});
        }
    }
}
