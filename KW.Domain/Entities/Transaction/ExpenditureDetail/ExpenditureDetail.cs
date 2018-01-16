using KW.Core;
using System;

namespace KW.Domain
{
    public class ExpenditureDetail : Entity
    {
        public string ExpenditureName { get; private set; }
        public string ExpenditureDefinition { get; private set; }
        public double Price { get; private set; }
        public int? CreatedBy { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool? IsDeleted { get; private set; }

        //Foreign Key
        public int ExpenditureId { get; private set; }
        public int BudgetId { get; private set; }

        //navigation properties
        public virtual Budget Budget { get; set; }

        public ExpenditureDetail() { }

        public ExpenditureDetail(string expenditureName, string expenditureDefinition, double price, Expenditure expenditure, Budget budget, int? createdBy, DateTime? createdDate)
        {
            this.ExpenditureName = expenditureName;
            this.ExpenditureDefinition = expenditureDefinition;
            this.Price = price;
            this.ExpenditureId = expenditure.Id;
            this.BudgetId = budget.Id;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.IsDeleted = false;
        }

        public virtual void Update(string expenditureName, string expenditureDefinition, double price, Expenditure expenditure, Budget budget, int? updatedBy, DateTime? updatedDate)
        {
            this.ExpenditureName = expenditureName;
            this.ExpenditureDefinition = expenditureDefinition;
            this.Price = price;
            this.ExpenditureId = expenditure.Id;
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
