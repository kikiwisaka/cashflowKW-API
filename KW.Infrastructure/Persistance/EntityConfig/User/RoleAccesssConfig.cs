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
    public class RoleAccesssConfig : EntityTypeConfiguration<RoleAccess>
    {
        public RoleAccesssConfig()
        {
            //table
            ToTable("tblRoleAccess");

            //key
            HasKey(x => x.Id);

            //property Foreign Key
            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.MenuId).HasColumnName("MenuId");

            ////relationship1
            HasRequired(x => x.Role).WithMany(x => x.RoleAccesses).HasForeignKey(x => x.RoleId);
            HasRequired(x => x.Menu).WithMany(x => x.RoleAccessList).HasForeignKey(x => x.MenuId);
        }
    }
}
