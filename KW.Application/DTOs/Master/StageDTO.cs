using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class StageDTO
    {
        public int Id { get; set; }
        public string NamaStage { get; set; }
        public string Keterangan { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public StageDTO(Stage model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaStage = model.NamaStage;
            this.Keterangan = model.Keterangan;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static StageDTO From(Stage model)
        {
            return new StageDTO(model);
        }

        public static IList<StageDTO> From(IList<Stage> collection)
        {
            IList<StageDTO> colls = new List<StageDTO>();
            foreach (var item in collection)
            {
                colls.Add(new StageDTO(item));
            }
            return colls;
        }
    }
}
