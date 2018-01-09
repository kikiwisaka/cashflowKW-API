using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class BudgetConfig : EntityTypeConfiguration<Budget>
    {
        public BudgetConfig()
        {
            //table
            ToTable("tblBudgets");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BudgetName).HasColumnName("budgetName");
            Property(x => x.Definition).HasColumnName("definition");
            Property(x => x.CreatedBy).HasColumnName("createdBy");
            Property(x => x.CreatedDate).HasColumnName("createdDate");
            Property(x => x.UpdatedBy).HasColumnName("updatedBy");
            Property(x => x.UpdatedDate).HasColumnName("updatedDate");
            Property(x => x.IsDeleted).HasColumnName("isDeleted");
        }
    }
}
