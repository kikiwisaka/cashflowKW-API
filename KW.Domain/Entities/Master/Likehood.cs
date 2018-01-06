using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class Likehood : Entity
    {
        public string NamaLikehood { get; set; }
        public decimal Incres { get; private set; }
        public bool Status { get; private set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        public virtual IList<LikehoodDetail> LikehoodDetails { get; private set; }

        public Likehood()
        {
            this.LikehoodDetails = new List<LikehoodDetail>();
        }

        public Likehood(string namaLikehood, int? createBy, DateTime? createDate)
        {
            this.NamaLikehood = namaLikehood;
            //this.Incres = incres;
            this.Status = false;
            this.IsDelete = false;
            this.CreateBy = createBy;
            this.CreateDate = CreateDate;
        }

        public virtual void Update(string namaLikehood, int? updateBy, DateTime? updateDate)
        {
            this.NamaLikehood = namaLikehood;
            //this.Incres = incres;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.UpdateBy = deleteBy;
            this.UpdateDate = deleteDate;
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }

        public virtual void SetDefault(int? updateBy, DateTime? updateDate)
        {
            this.Status = true;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void RemoveDefault(int? updateBy, DateTime? updateDate)
        {
            this.Status = false;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void AddLikehoodDetail(IList<LikehoodDetail> likehoodDetails)
        {
            if (!this.Equals(likehoodDetails))
                this.LikehoodDetails = likehoodDetails;
        }
    }
}
