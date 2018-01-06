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
    public class RoleEmployeeTypeConfig : EntityTypeConfiguration<RoleEmployeeType>
    {
        public RoleEmployeeTypeConfig()
        {
            //table
            ToTable("tblRoleEmployeeTypes");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //property Foreign Key
            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.EmployeeTypeId).HasColumnName("EmployeeTypeId");

            ////relationship1
            HasRequired(x => x.Role).WithMany(x => x.RoleEmployeeTypes).HasForeignKey(x => x.RoleId);
        }
    }
}
