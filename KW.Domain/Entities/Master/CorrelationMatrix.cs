using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelationMatrix : Entity
    {
        public string NamaCorrelationMatrix { get; private set; }
        public decimal Nilai { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public bool? Status { get; private set; }

        public CorrelationMatrix()
        { }

        public CorrelationMatrix(string namaCorrelationMatrix, decimal nilai, int? createBy, DateTime? createDate)
        {
            this.NamaCorrelationMatrix = namaCorrelationMatrix;
            this.Nilai = nilai;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.Status = false;
        }

        public virtual void Update(string namaCorrelationMatrix, decimal nilai, int? updateBy, DateTime? updateDate)
        {
            this.NamaCorrelationMatrix = namaCorrelationMatrix;
            this.Nilai = nilai;
            this.UpdateBy = updateBy;
            this.UpdateDate = UpdateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
