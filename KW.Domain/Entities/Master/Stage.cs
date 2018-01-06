using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Stage : Entity
    {
        public string NamaStage { get; private set; }
        public string Keterangan { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        public Stage()
        { }

        public Stage(string namaStage, string keterangan, int? createBy, DateTime? createDate)
        {
            this.NamaStage = namaStage;
            this.Keterangan = keterangan;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(string namaStage, string keterangan, int? updateBy, DateTime? updateDate)
        {
            this.NamaStage = namaStage;
            this.Keterangan = keterangan;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
