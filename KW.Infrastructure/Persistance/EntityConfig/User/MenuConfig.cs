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
    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        public MenuConfig()
        {
            //table
            ToTable("tblMenu");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Description).HasColumnName("description");
            Property(x => x.ControllerName).HasColumnName("controllername");
            Property(x => x.ActionName).HasColumnName("actionname");
            Property(x => x.Active).HasColumnName("active");
            Property(x => x.IsGeneralAccess).HasColumnName("isgeneralaccess");
            Property(x => x.IsOnMenu).HasColumnName("isonmenu");
            Property(x => x.StyleClass).HasColumnName("styleclass");
            Property(x => x.Icon).HasColumnName("icon");
            Property(x => x.Sequence).HasColumnName("sequence");
            Property(x => x.IsSuperAdminOnly).HasColumnName("isSuperAdmin");

            //property Foreign Key
            Property(x => x.ParentId).HasColumnName("ParentId");

            ////relationship
            HasMany(x => x.APIMenuList).WithRequired(x => x.Menu).HasForeignKey(x => x.MenuId);
            HasMany(x => x.RoleAccessList).WithRequired(x => x.Menu).HasForeignKey(x => x.MenuId);
        }
    }
}
