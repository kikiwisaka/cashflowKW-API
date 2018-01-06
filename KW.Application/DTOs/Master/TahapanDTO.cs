using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class TahapanDTO
    {
        public int Id { get; set; }
        public string NamaTahapan { get; set; }
        public string Keterangan { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public TahapanDTO(Tahapan model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaTahapan = model.NamaTahapan;
            this.Keterangan = model.Keterangan;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static TahapanDTO From(Tahapan model)
        {
            return new TahapanDTO(model);
        }

        public static IList<TahapanDTO> From(IList<Tahapan> collection)
        {
            IList<TahapanDTO> colls = new List<TahapanDTO>();
            foreach (var item in collection)
            {
                colls.Add(new TahapanDTO(item));
            }
            return colls;
        }
    }
}
