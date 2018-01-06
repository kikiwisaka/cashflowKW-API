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
    public class UseResetPasswordConfig : EntityTypeConfiguration<UserResetPassword>
    {
        public UseResetPasswordConfig()
        {
            //table
            ToTable("tblUsersResetPassword");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("userName");
            Property(x => x.RequestToken).HasColumnName("requestToken");
            Property(x => x.RequestDate).HasColumnName("requestDate");
            Property(x => x.TimeRequest).HasColumnName("timeRequest");
        }
    }
}
