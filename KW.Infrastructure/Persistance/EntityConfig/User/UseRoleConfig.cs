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
    public class UseRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UseRoleConfig()
        {
            //table
            ToTable("tblUserRole");

            //key
            //HasKey(x => x.Id);
            //HasKey(x => x.RoleId);

            //property Foreign Key
            HasKey(x => x.UserId);
            Property(x => x.UserId).HasColumnName("UserId");
            HasKey(x => x.RoleId);
            Property(x => x.RoleId).HasColumnName("RoleId");

            ////relationship1
            //HasRequired(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            //HasRequired(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
        }
    }
}
