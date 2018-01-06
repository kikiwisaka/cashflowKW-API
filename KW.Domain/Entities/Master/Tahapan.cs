using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Tahapan : Entity
    {
        public string NamaTahapan { get; private set; }
        public string Keterangan { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        public virtual ICollection<Project> Projects { get; private set; }


        public Tahapan()
        {
            this.Projects = new List<Project>();
        }

        public Tahapan(string namaTahapan, string keterangan, int? createBy, DateTime? createDate)
        {
            this.NamaTahapan = namaTahapan;
            this.Keterangan = keterangan;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(string namaTahapan, string keterangan, int? updateBy, DateTime? updateDate)
        {
            this.NamaTahapan = namaTahapan;
            this.Keterangan = keterangan;
            this.UpdateBy= updateBy;
            this.UpdateDate= updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
