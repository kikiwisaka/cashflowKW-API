using System;

namespace KW.Application.Params
{
    public class BudgetParam
    {
        public string BudgetName { get; set; }
        public string Definition { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public BudgetParam() { }
    }
}
