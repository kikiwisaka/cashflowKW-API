using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class MatrixDTO
    {
        public int Id { get; set; }
        public string NamaMatrix { get; set; }
        public string NamaFormula { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public MatrixDTO(Matrix model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaMatrix = model.NamaMatrix;
            this.NamaFormula = model.NamaFormula;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static MatrixDTO From(Matrix model)
        {
            return new MatrixDTO(model);
        }

        public static IList<MatrixDTO> From(IList<Matrix> collection)
        {
            IList<MatrixDTO> colls = new List<MatrixDTO>();
            foreach (var item in collection)
            {
                colls.Add(new MatrixDTO(item));
            }
            return colls;
        }
    }
}
