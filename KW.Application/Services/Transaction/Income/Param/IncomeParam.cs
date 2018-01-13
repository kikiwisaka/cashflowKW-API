using System;

namespace KW.Application.Params
{
    public class IncomeParam
    {
        public string IncomeName { get; set; }
        public string Definition { get; set; }
        public int IncomeDate { get; set; }
        public int IncomeMonth { get; set; }
        public int IncomeYear { get; set; }
        public int BudgetId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public IncomeParam() { }
    }
}
