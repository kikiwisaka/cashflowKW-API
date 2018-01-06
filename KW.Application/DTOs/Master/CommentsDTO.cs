using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class CommentsDTO
    {
        public int Id { get; set; }
        public int ColorCommentId { get; set; }
        public int MatrixId { get; set; }
        public string Comment { get; set; }
        public string ActionPoint { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string NamaMatrix { get; set; }
        public string Warna { get; set; }

        public ColorCommentDTO ColorComment { get; set; }
        public MatrixDTO Matrix { get; set; }

        public CommentsDTO(Comments model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.Comment = model.Comment;
            this.ActionPoint = model.ActionPoint;
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

            if (model.Matrix != null)
            {
                MatrixDTO matrixDTO = MatrixDTO.From(model.Matrix);
                this.Matrix = matrixDTO;
                this.MatrixId = matrixDTO.Id;
                this.NamaMatrix = matrixDTO.NamaMatrix;
            }
        }

        public static CommentsDTO From(Comments model)
        {
            return new CommentsDTO(model);
        }

        public static IList<CommentsDTO> From(IList<Comments> collection)
        {
            IList<CommentsDTO> colls = new List<CommentsDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CommentsDTO(item));
            }
            return colls;
        }
    }
}
