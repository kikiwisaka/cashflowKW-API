using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class OverAllComments : Entity
    {
        public string OverAllComment { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ColorCommentId { get; private set; }

        // Navigation properties
        public virtual ColorComment ColorComment{ get; set; }
        

        public OverAllComments()
        {

        }

        public OverAllComments(ColorComment ColorComment, string overAllComment, int? createBy, DateTime? createDate)
        {
            this.ColorCommentId = ColorComment.Id;
            this.OverAllComment = overAllComment;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(ColorComment ColorComment, string overAllComment, int? updateBy, DateTime? updateDate)
        {
            this.ColorCommentId = ColorComment.Id;
            this.OverAllComment = overAllComment;
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
