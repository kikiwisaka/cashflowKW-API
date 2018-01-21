using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class ExpenditureDetailDTO
    {
        public int Id { get; set; }
        public string ExpenditureName { get; set; }
        public string ExpenditureDefinition { get; set; }
        public double Price { get; set; }
        public int ExpenditureId { get; set; }
        public int BudgetId { get; set; }
        public string BudgetName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public ExpenditureDetailDTO(ExpenditureDetail model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.ExpenditureName = model.ExpenditureName;
            this.ExpenditureDefinition = model.ExpenditureDefinition;
            this.Price= model.Price;
            this.CreatedBy = model.CreatedBy;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedBy = model.UpdatedBy;
            this.UpdatedDate = model.UpdatedDate;
            this.IsDeleted = model.IsDeleted;

            this.ExpenditureId = model.ExpenditureId;

            if (model.Budget != null)
            {
                this.BudgetId = model.Budget.Id;
                this.BudgetName = model.Budget.BudgetName;
            }
        }

        public static ExpenditureDetailDTO From(ExpenditureDetail model)
        {
            return new ExpenditureDetailDTO(model);
        }

        public static IList<ExpenditureDetailDTO> From(IList<ExpenditureDetail> collection)
        {
            IList<ExpenditureDetailDTO> dtos = new List<ExpenditureDetailDTO>();
            foreach (var item in collection)
            {
                dtos.Add(new ExpenditureDetailDTO(item));
            }
            return dtos;
        }
    }
}
