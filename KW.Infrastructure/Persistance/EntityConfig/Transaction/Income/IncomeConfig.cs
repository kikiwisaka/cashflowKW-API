using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class IncomeConfig : EntityTypeConfiguration<Income>
    {
        public IncomeConfig()
        {
            //table
            ToTable("tblIncomes");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.IncomeName).HasColumnName("incomeName");
            Property(x => x.Definition).HasColumnName("definition");
            Property(x => x.IncomeDate).HasColumnName("incomeDate");
            Property(x => x.Amount).HasColumnName("amount");
            Property(x => x.CreatedBy).HasColumnName("createdBy");
            Property(x => x.CreatedDate).HasColumnName("createdDate");
            Property(x => x.UpdatedBy).HasColumnName("updatedBy");
            Property(x => x.UpdatedDate).HasColumnName("updatedDate");
            Property(x => x.IsDeleted).HasColumnName("isDeleted");

            //foreign key
            Property(x => x.BudgetId).HasColumnName("budgetId");
        }
    }
}
