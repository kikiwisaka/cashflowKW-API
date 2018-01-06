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
    public class APIConfig : EntityTypeConfiguration<API>
    {
        public APIConfig()
        {
            //table
            ToTable("tblAPI");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Description).HasColumnName("description");
            Property(x => x.ControllerName).HasColumnName("controllername");
            Property(x => x.ActionName).HasColumnName("actionname");
            Property(x => x.Active).HasColumnName("fileType");
            Property(x => x.IsGeneralAccess).HasColumnName("isgeneralaccess");

            ////relationship
            HasMany(x => x.APIMenuList).WithRequired(x => x.API).HasForeignKey(x => x.ApiId);
        }
    }
}
