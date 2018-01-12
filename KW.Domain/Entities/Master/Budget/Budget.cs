using KW.Core;
using System;

namespace KW.Domain
{
    public class Budget : Entity
    {
        public string BudgetName { get; private set; }
        public string Definition { get; private set; }
        public int? CreatedBy { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool? IsDeleted { get; private set; }

        public Budget() { }

        public Budget(string budgetName, string definition, int? createdBy, DateTime? createdDate)
        {
            this.BudgetName = budgetName;
            this.Definition = definition;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.IsDeleted = false;
        }

        public virtual void Update(string budgetName, string definition, int? updatedBy, DateTime? updatedDate)
        {
            this.BudgetName = budgetName;
            this.Definition = definition;
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
