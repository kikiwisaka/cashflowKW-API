using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class SektorDTO
    {
        public int Id { get; set; }
        public string NamaSektor { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Status { get; private set; }

        public SektorDTO(Sektor model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaSektor = model.NamaSektor;
            this.Minimum = model.Minimum;
            this.Maximum = model.Maximum;
            this.Definisi = model.Definisi;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
        }

        public static SektorDTO From(Sektor model)
        {
            return new SektorDTO(model);
        }

        public static IList<SektorDTO> From(IList<Sektor> collection)
        {
            IList<SektorDTO> colls = new List<SektorDTO>();
            foreach (var item in collection)
            {
                colls.Add(new SektorDTO(item));
            }
            return colls;
        }
    }
}
