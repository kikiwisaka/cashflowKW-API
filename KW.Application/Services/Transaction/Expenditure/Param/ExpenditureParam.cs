using System;

namespace KW.Application.Params
{
    public class ExpenditureParam
    {
        public DateTime ExpenditureDate { get; set; }
        public string ExpenditureName { get; set; }
        public string ExpenditureDefinition { get; set; }
        public double Price { get; set; }
        public int ExpenditureId { get; private set; }
        public int BudgetId { get; private set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public ExpenditureParam() { }
    }
}
