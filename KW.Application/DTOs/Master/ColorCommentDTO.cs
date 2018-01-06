using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class ColorCommentDTO
    {
        public int Id { get; set; }
        public string Warna { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ColorCommentDTO(ColorComment model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.Warna = model.Warna;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static ColorCommentDTO From(ColorComment model)
        {
            return new ColorCommentDTO(model);
        }

        public static IList<ColorCommentDTO> From(IList<ColorComment> collection)
        {
            IList<ColorCommentDTO> colls = new List<ColorCommentDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ColorCommentDTO(item));
            }
            return colls;
        }
    }
}
