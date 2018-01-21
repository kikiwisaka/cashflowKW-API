using KW.Core;
using System;
using System.Collections.Generic;

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

        //Navigation Properties
        public virtual IList<ExpenditureDetail> ExpenditureDetail { get; set; }


        public Expenditure() { }

        public Expenditure(DateTime expenditureDate, double total, int? createdBy, DateTime? createdDate)
        {
            this.ExpenditureDate = expenditureDate.Date;
            this.Total = total;
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

        public virtual void AddExpenditureDetail(IList<ExpenditureDetail> expenditureDetails)
        {
            if (!this.Equals(expenditureDetails))
                this.ExpenditureDetail = expenditureDetails;
        }

        public virtual void RemoveExpenditureDetail(ExpenditureDetail expenditureDetail)
        {
            this.ExpenditureDetail.Remove(expenditureDetail);
        }

        public virtual void Delete(int? updatedBy, DateTime? updatedDate)
        {
            this.IsDeleted = true;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }
    }
}
