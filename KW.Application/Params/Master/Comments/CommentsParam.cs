//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class CommentsParam
    {
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


        public CommentsParam() { }
    }
}
