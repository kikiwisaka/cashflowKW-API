using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class ExpenditureDetailConfig : EntityTypeConfiguration<ExpenditureDetail>
    {
        public ExpenditureDetailConfig()
        {
            //table
            ToTable("tblExpenditureDetails");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ExpenditureName).HasColumnName("expenditureName");
            Property(x => x.ExpenditureDefinition).HasColumnName("expenditureDefinition");
            Property(x => x.Price).HasColumnName("price");
            Property(x => x.CreatedBy).HasColumnName("createdBy");
            Property(x => x.CreatedDate).HasColumnName("createdDate");
            Property(x => x.UpdatedBy).HasColumnName("updatedBy");
            Property(x => x.UpdatedDate).HasColumnName("updatedDate");
            Property(x => x.IsDeleted).HasColumnName("isDeleted");

            //foreign key
            Property(x => x.ExpenditureId).HasColumnName("expenditureId");
            Property(x => x.BudgetId).HasColumnName("budgetId");
        }
    }
}
