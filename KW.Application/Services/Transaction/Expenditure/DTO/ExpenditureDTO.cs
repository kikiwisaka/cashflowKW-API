using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class ExpenditureDTO
    {
        public int Id { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public double Total { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public IList<ExpenditureDetailDTO> ExpenditureDetail { get; set; }

        public ExpenditureDTO(Expenditure model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.ExpenditureDate = model.ExpenditureDate;
            this.CreatedBy = model.CreatedBy;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedBy = model.UpdatedBy;
            this.UpdatedDate = model.UpdatedDate;
            this.IsDeleted = model.IsDeleted;
            
            if (model.ExpenditureDetail != null)
            {
                IList<ExpenditureDetailDTO> dto = ExpenditureDetailDTO.From(model.ExpenditureDetail);
                ExpenditureDetail = dto;
            }
        }

        public static ExpenditureDTO From(Expenditure model)
        {
            return new ExpenditureDTO(model);
        }

        public static IList<ExpenditureDTO> From(IList<Expenditure> collection)
        {
            IList<ExpenditureDTO> dtos = new List<ExpenditureDTO>();
            foreach (var item in collection)
            {
                dtos.Add(new ExpenditureDTO(item));
            }
            return dtos;
        }
    }
}
