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
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            //table
            ToTable("tblUsers");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("userName");
            Property(x => x.Password).HasColumnName("password");
            Property(x => x.Status).HasColumnName("status");
            Property(x => x.Language).HasColumnName("language");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.CreateTime).HasColumnName("createTime");
            Property(x => x.ExpiryDate).HasColumnName("expirydate");
            Property(x => x.LastUpdateDate).HasColumnName("lastupdatedate");

            //property Foreign Key
            Property(x => x.EmployeeId).HasColumnName("employeeId");
        }
    }
}
