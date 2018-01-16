using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class ExpenditureConfig : EntityTypeConfiguration<Expenditure>
    {
        public ExpenditureConfig()
        {
            //table
            ToTable("tblExpenditures");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ExpenditureDate).HasColumnName("expenditureDate");
            Property(x => x.Total).HasColumnName("total");
            Property(x => x.CreatedBy).HasColumnName("createdBy");
            Property(x => x.CreatedDate).HasColumnName("createdDate");
            Property(x => x.UpdatedBy).HasColumnName("updatedBy");
            Property(x => x.UpdatedDate).HasColumnName("updatedDate");
            Property(x => x.IsDeleted).HasColumnName("isDeleted");
        }
    }
}
