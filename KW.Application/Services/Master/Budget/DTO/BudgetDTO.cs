using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class BudgetDTO
    {
        public int Id { get; set; }
        public string BudgetName { get; set; }
        public string Definition { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public BudgetDTO(Budget model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.BudgetName = model.BudgetName;
            this.Definition= model.Definition;
            this.CreatedBy = model.CreatedBy;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedBy = model.UpdatedBy;
            this.UpdatedDate = model.UpdatedDate;
            this.IsDeleted = model.IsDeleted;
        }

        public static BudgetDTO From(Budget model)
        {
            return new BudgetDTO(model);
        }

        public static IList<BudgetDTO> From(IList<Budget> collection)
        {
            IList<BudgetDTO> dtos = new List<BudgetDTO>();
            foreach (var item in collection)
            {
                dtos.Add(new BudgetDTO(item));
            }
            return dtos;
        }
    }
}
