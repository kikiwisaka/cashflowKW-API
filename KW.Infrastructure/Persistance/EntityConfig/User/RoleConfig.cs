using KW.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KW.Infrastructure.EntityConfig
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            //table
            ToTable("tblRole");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Description).HasColumnName("description");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.CreateTime).HasColumnName("createTime");
            Property(x => x.Status).HasColumnName("status");
            Property(x => x.IsMasking).HasColumnName("isMasking");
            Property(x => x.IsAllJobs).HasColumnName("isAllJobs");

            //property Foreign Key
            Property(x => x.CreatorId).HasColumnName("creatorUserId");

            ////relationship1
            HasMany(x => x.RoleAccesses).WithRequired(x => x.Role).HasForeignKey(x => x.RoleId);
            //HasMany(x => x.UserRoles).WithRequired(x => x.Role).HasForeignKey(x => x.RoleId);
        }
    }
}
