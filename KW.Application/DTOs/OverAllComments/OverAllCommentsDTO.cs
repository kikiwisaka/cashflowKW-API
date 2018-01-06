using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class OverAllCommentsDTO
    {
        public int Id { get; set; }
        public int ColorCommentId { get; set; }
        public string OverAllComment { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Warna { get; set; }

        public ColorCommentDTO ColorComment { get; set; }

        public OverAllCommentsDTO(OverAllComments model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.OverAllComment = model.OverAllComment;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.ColorComment != null)
            {
                ColorCommentDTO colorCommentDTO = ColorCommentDTO.From(model.ColorComment);
                this.ColorComment = colorCommentDTO;
                this.ColorCommentId = colorCommentDTO.Id;
                this.Warna = colorCommentDTO.Warna;
            }
        }

        public static OverAllCommentsDTO From(OverAllComments model)
        {
            return new OverAllCommentsDTO(model);
        }

        public static IList<OverAllCommentsDTO> From(IList<OverAllComments> collection)
        {
            IList<OverAllCommentsDTO> colls = new List<OverAllCommentsDTO>();
            foreach (var item in collection)
            {
                colls.Add(new OverAllCommentsDTO(item));
            }
            return colls;
        }
    }
}
