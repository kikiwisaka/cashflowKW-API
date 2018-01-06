using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Matrix : Entity
    {
        public string NamaMatrix { get; private set; }
        public string NamaFormula { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        public Matrix()
        {

        }

        public Matrix(string namaMatrix, string namaFormula, int? createBy, DateTime? createDate)
        {
            this.NamaMatrix = namaMatrix;
            this.NamaFormula = namaFormula;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(string namaMatrix, string namaFormula, int? updateBy, DateTime? updateDate)
        {
            this.NamaMatrix = namaMatrix;
            this.NamaFormula = namaFormula;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
