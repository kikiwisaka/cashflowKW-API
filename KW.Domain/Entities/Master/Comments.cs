using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Comments : Entity
    {
        public string Comment { get; private set; }
        public string ActionPoint { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ColorCommentId { get; private set; }
        public int MatrixId { get; private set; }

        // Navigation properties
        public virtual ColorComment ColorComment{ get; set; }
        public virtual Matrix Matrix { get; set; }
        

        public Comments()
        {

        }

        public Comments(ColorComment ColorComment, Matrix Matrix, string comment, string actionPoint, int? createBy, DateTime? createDate)
        {
            this.ColorCommentId = ColorComment.Id;
            this.MatrixId = Matrix.Id;
            this.Comment = comment;
            this.ActionPoint = actionPoint;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(ColorComment ColorComment, Matrix Matrix, string comment, string actionPoint, int? updateBy, DateTime? updateDate)
        {
            this.ColorCommentId = ColorComment.Id;
            this.MatrixId = Matrix.Id;
            this.Comment = comment;
            this.ActionPoint = actionPoint;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
