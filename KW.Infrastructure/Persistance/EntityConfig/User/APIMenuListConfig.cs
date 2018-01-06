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
    public class APIMenuListConfig : EntityTypeConfiguration<APIMenu>
    {
        public APIMenuListConfig()
        {
            //table
            ToTable("tblAPIMenu");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //property Foreign Key
            Property(x => x.ApiId).HasColumnName("APIId");
            Property(x => x.MenuId).HasColumnName("MenuId");

            ////relationship
            HasRequired(x => x.API).WithMany(x => x.APIMenuList).HasForeignKey(x => x.ApiId);
            HasRequired(x => x.Menu).WithMany(x => x.APIMenuList).HasForeignKey(x => x.MenuId);
        }
    }
}
