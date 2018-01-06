using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Sektor : Entity
    {
        public string NamaSektor { get; private set; }
        public decimal Minimum { get; private set; }
        public decimal Maximum { get; private set; }
        public string Definisi { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public bool Status { get; private set; }

        public virtual ICollection<Project> Projects { get; private set; }


        public Sektor()
        {
            this.Projects = new List<Project>();
        }

        public Sektor(string namaSektor, decimal minimum, decimal maximum, string definisi, int? createBy, DateTime? createDate)
        {
            this.NamaSektor = namaSektor;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Definisi = definisi;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(string namaSektor, decimal minimum, decimal maximum, string definisi, int? updateBy, DateTime? updateDate)
        {
            this.NamaSektor = namaSektor;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Definisi = definisi;
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
