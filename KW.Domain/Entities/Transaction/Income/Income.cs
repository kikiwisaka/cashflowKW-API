using KW.Core;
using System;

namespace KW.Domain
{
    public class Income : Entity
    {
        public string IncomeName { get; private set; }
        public string Definition { get; private set; }
        public DateTime IncomeDate { get; private set; }
        public int? CreatedBy { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool? IsDeleted { get; private set; }

        //foreign key
        public int BudgetId { get; private set; }

        //navigation properties
        public virtual Budget Budget { get; set; }

        public Income() { }

        public Income(string incomeName, string definition, DateTime incomeDate, Budget budget, int? createdBy, DateTime? createdDate)
        {
            this.IncomeName = incomeName;
            this.Definition = definition;
            this.IncomeDate = incomeDate.Date;
            this.BudgetId = budget.Id;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.IsDeleted = false;
        }

        public virtual void Update(string incomeName, string definition, DateTime incomeDate, Budget budget, int? updatedBy, DateTime? updatedDate)
        {
            this.IncomeName = incomeName;
            this.Definition = definition;
            this.IncomeDate = incomeDate.Date;
            this.BudgetId = budget.Id;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }

        public virtual void Delete(int? updatedBy, DateTime? updatedDate)
        {
            this.IsDeleted = true;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }
    }
}
