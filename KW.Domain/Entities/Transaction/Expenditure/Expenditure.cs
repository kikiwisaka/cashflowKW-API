using KW.Core;
using System;

namespace KW.Domain
{
    public class Expenditure : Entity
    {
        public DateTime ExpenditureDate { get; private set; }
        public double Total { get; private set; }
        public int? CreatedBy { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool? IsDeleted { get; private set; }


        public Expenditure() { }

        public Expenditure(DateTime expenditureDate, int? createdBy, DateTime? createdDate)
        {
            this.ExpenditureDate = expenditureDate.Date;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.IsDeleted = false;
        }

        public virtual void Update(DateTime expenditureDate, int? updatedBy, DateTime? updatedDate)
        {
            this.ExpenditureDate = expenditureDate.Date;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }

        public virtual void AddTotal(double total)
        {
            this.Total = total;
        }

        public virtual void Delete(int? updatedBy, DateTime? updatedDate)
        {
            this.IsDeleted = true;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }
    }
}
