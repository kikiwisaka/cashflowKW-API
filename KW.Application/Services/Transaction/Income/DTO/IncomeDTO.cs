using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        public string IncomeName { get; set; }
        public string Definition { get; set; }
        public int IncomeDate{ get; set; }
        public int IncomeMonth{ get; set; }
        public int IncomeYear{ get; set; }
        public int BudgetId { get; set; }
        public string BudgetName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public IncomeDTO(Income model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.IncomeName = model.IncomeName;
            this.Definition = model.Definition;
            this.IncomeDate = model.IncomeDate;
            this.IncomeMonth = model.IncomeMonth;
            this.IncomeYear = model.IncomeYear; 
            this.CreatedBy = model.CreatedBy;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedBy = model.UpdatedBy;
            this.UpdatedDate = model.UpdatedDate;
            this.IsDeleted = model.IsDeleted;

            if (model.Budget != null)
            {
                this.BudgetId = model.Budget.Id;
                this.BudgetName = model.Budget.BudgetName;
            }
        }

        public static IncomeDTO From(Income model)
        {
            return new IncomeDTO(model);
        }

        public static IList<IncomeDTO> From(IList<Income> collection)
        {
            IList<IncomeDTO> dtos = new List<IncomeDTO>();
            foreach (var item in collection)
            {
                dtos.Add(new IncomeDTO(item));
            }
            return dtos;
        }
    }
}
