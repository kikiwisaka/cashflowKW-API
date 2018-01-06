using System;
using System.Collections.Generic;
using KW.Domain;

namespace KW.Application.DTO
{
    public class CorrelationMatrixDTO
    {
        public int Id { get; set; }
        public string NamaCorrelationMatrix{ get; set; }
        public decimal Nilai { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; private set; }

        public CorrelationMatrixDTO(CorrelationMatrix model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaCorrelationMatrix = model.NamaCorrelationMatrix;
            this.Nilai= model.Nilai;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
        }

        public static CorrelationMatrixDTO From(CorrelationMatrix model)
        {
            return new CorrelationMatrixDTO(model);
        }

        public static IList<CorrelationMatrixDTO> From(IList<CorrelationMatrix> collection)
        {
            IList<CorrelationMatrixDTO> colls = new List<CorrelationMatrixDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelationMatrixDTO(item));
            }
            return colls;
        }
    }
}
