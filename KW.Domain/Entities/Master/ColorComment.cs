using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class ColorComment : Entity
    {
        public string Warna { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        public ColorComment()
        { }

        public ColorComment(string warna, int? createBy, DateTime? createDate)
        {
            this.Warna = warna;
            this.IsDelete = false;
            this.CreateBy = createBy;
            this.CreateDate = CreateDate;
        }

        public virtual void Update(string warna, int? updateBy, DateTime? updateDate)
        {
            this.Warna = warna;
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
