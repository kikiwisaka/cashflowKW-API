using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class CommentsConfig : EntityTypeConfiguration<Comments>
    {
        public CommentsConfig()
        {
            //table
            ToTable("tblCommentss");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Comment).HasColumnName("comment");
            Property(x => x.ActionPoint).HasColumnName("actionPoint");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");

            //Foreign Key
            Property(x => x.ColorCommentId).HasColumnName("colorCommentId");
            Property(x => x.MatrixId).HasColumnName("matrixId");
        }
    }
}
