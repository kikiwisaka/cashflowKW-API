using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class LikehoodDTO
    {
        public int Id { get; set; }
        public string NamaLikehood { get; set; }
        public decimal Incres { get; set; }
        public bool Status{ get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IList<LikehoodDetailDTO> LikehoodDetail { get; set; }

        public LikehoodDTO(Likehood model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaLikehood = model.NamaLikehood;
            this.Incres = model.Incres;
            this.Status = model.Status;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if(model.LikehoodDetails != null)
            {
                IList<LikehoodDetailDTO> likehoodDetailDTOs = LikehoodDetailDTO.From(model.LikehoodDetails, model);
                this.LikehoodDetail = likehoodDetailDTOs;
            }
        }

        public static LikehoodDTO From(Likehood model)
        {
            return new LikehoodDTO(model);
        }

        public static IList<LikehoodDTO> From(IList<Likehood> collection)
        {
            IList<LikehoodDTO> colls = new List<LikehoodDTO>();
            foreach (var item in collection)
            {
                colls.Add(new LikehoodDTO(item));
            }
            return colls;
        }
    }
}
