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
            Property(x => x.IncomeMonth).HasColumnName("incomeMonth");
            Property(x => x.IncomeYear).HasColumnName("incomeYear");
            Property(x => x.CreatedBy).HasColumnName("createdBy");
            Property(x => x.CreatedDate).HasColumnName("createdDate");
            Property(x => x.UpdatedBy).HasColumnName("updatedBy");
            Property(x => x.UpdatedDate).HasColumnName("updatedDate");
            Property(x => x.IsDeleted).HasColumnName("isDeleted");

            //foreign key
            Property(x => x.BudgetId).HasColumnName("budgetId");

            //relatioship
            //HasRequired(x => x.Budget).WithOptional(x => x.Income)
            //HasRequired(x => x.Employee).WithMany(x => x.JobContracts).HasForeignKey(x => x.EmployeeId);
        }
    }
}
