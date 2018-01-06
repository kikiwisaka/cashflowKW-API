using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class LikehoodDetail : Entity
    {
        public string DefinisiLikehood { get; private set; }
        public decimal Lower { get; private set; }
        public decimal Upper { get; private set; }
        public decimal Average { get; private set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public bool Status { get; private set; }

        //Foreign Key
        public int LikehoodId { get; private set; }

        // Navigation properties
        public virtual Likehood Likehoods { get; private set; }


        public LikehoodDetail()
        {
            //this.Likehoods = new Likehood();
        }

        public LikehoodDetail(string definisi, decimal lower, decimal upper, decimal average, int likelhoodId, int? createBy, DateTime? createDate)
        {
            this.DefinisiLikehood = definisi;
            this.Lower = lower;
            this.Upper = upper;
            this.Average = average;
            this.LikehoodId = likelhoodId;
            this.IsDelete = false;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
        }

        public virtual void Update(string definisi, decimal lower, decimal upper, decimal average, int likehoodId, int? updateBy, DateTime? updateDate)
        {
            this.DefinisiLikehood = definisi;
            this.Lower = lower;
            this.Upper = upper;
            this.Average = average;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
            this.LikehoodId = likehoodId;
        }

        public virtual void Delete(int likehoodId, int? updateBy, DateTime? deleteDate)
        {
            this.LikehoodId = likehoodId;
            this.UpdateBy = updateBy;
            this.UpdateDate = deleteDate;
            this.DeleteDate = deleteDate;
            this.IsDelete = true;
        }
    }
}
