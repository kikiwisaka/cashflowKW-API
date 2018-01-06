using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class CorrelationRiskAntarProjectDTO
    {
        public int Id { get; set; }
        public int CorrelationRiskAntarSectorId { get; set; }
        public int ProjectId{ get; set; }
        public string NamaProject { get; set; }
        public int SektorId { get; set; }
        public string NamaSektor { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public ProjectDTO Project { get; set; }
        public SektorDTO Sektor { get; set; }

        public CorrelationRiskAntarProjectDTO(CorrelationRiskAntarProject model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            
        }

        public static CorrelationRiskAntarProjectDTO From(CorrelationRiskAntarProject model)
        {
            return new CorrelationRiskAntarProjectDTO(model);
        }

        public static IList<CorrelationRiskAntarProjectDTO> From(IList<CorrelationRiskAntarProject> collection)
        {
            IList<CorrelationRiskAntarProjectDTO> colls = new List<CorrelationRiskAntarProjectDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelationRiskAntarProjectDTO(item));
            }
            return colls;
        }
    }
}
