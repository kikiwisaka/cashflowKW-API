using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class LikehoodDetailConfig : EntityTypeConfiguration<LikehoodDetail>
    {
        public LikehoodDetailConfig()
        {
            //table
            ToTable("tblLikehoodDetails");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DefinisiLikehood).HasColumnName("definisiLikehood");
            Property(x => x.Lower).HasColumnName("lower");
            Property(x => x.Upper).HasColumnName("upper");
            Property(x => x.Average).HasColumnName("average");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");
            Property(x => x.Status).HasColumnName("status");

            //Foreign Key
            Property(x => x.LikehoodId).HasColumnName("likehoodId");

            //relationship
            //HasRequired(x => x.Likehoods).WithMany(x => x.)
        }
    }
}
