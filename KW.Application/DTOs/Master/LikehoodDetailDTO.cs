using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class LikehoodDetailDTO
    {
        public int Id { get; set; }
        public string DefinisiLikehood { get; set; }
        public decimal Lower { get; set; }
        public decimal Upper { get; set; }
        public decimal Average { get; set; }
        public int LikehoodId { get; set; }
        public string NamaLikehood { get; set; }
        public decimal Incres { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }

        public LikehoodDetailDTO(LikehoodDetail model, Likehood likehoodParent)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.LikehoodId = model.LikehoodId;
            this.DefinisiLikehood = model.DefinisiLikehood;
            this.Lower = model.Lower;
            this.Upper = model.Upper;
            this.Average = model.Average;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.Status = model.Status;

            if(likehoodParent != null)
            {
                this.NamaLikehood = likehoodParent.NamaLikehood;
                this.Incres = likehoodParent.Incres;
            }
        }

        public static LikehoodDetailDTO From(LikehoodDetail model, Likehood likehoodParent)
        {
            return new LikehoodDetailDTO(model, likehoodParent);
        }

        public static IList<LikehoodDetailDTO> From(IList<LikehoodDetail> collection, Likehood likehoodParent)
        {
            IList<LikehoodDetailDTO> colls = new List<LikehoodDetailDTO>();
            foreach (var item in collection)
            {
                colls.Add(new LikehoodDetailDTO(item, likehoodParent));
            }
            return colls;
        }
    }
}
