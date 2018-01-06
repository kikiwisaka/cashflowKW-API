using KW.Core;
using System;

namespace KW.Domain
{
    public class StageTahunRiskMatrixDetail : Entity
    {
        public decimal? NilaiExpose { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsUpdate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int StageTahunRiskMatrixId { get; private set; }
        public int RiskRegistrasiId { get; private set; }
        public int LikehoodDetailId { get; private set; }
        public int RiskMatrixProjectId { get; private set; }

        //Navigation properties
        public virtual StageTahunRiskMatrix StageTahunRiskMatrix { get; set; }
        public virtual RiskRegistrasi RiskRegistrasi { get; set; }
        public virtual LikehoodDetail LikehoodDetail { get; set; }

        public StageTahunRiskMatrixDetail()
        { }

        public StageTahunRiskMatrixDetail(StageTahunRiskMatrix stageTahunRiskMatrix, RiskRegistrasi riskRegistrasi, int riskMatrixProjectId, int likehoodDetailId, decimal? nilaiExpose, int? createBy, DateTime? createDate)
        {
            this.NilaiExpose = nilaiExpose;
            this.StageTahunRiskMatrixId = stageTahunRiskMatrix.Id;
            this.RiskRegistrasiId = riskRegistrasi.Id;
            this.LikehoodDetailId = likehoodDetailId;
            this.RiskMatrixProjectId = riskMatrixProjectId;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(RiskRegistrasi riskRegistrasi, LikehoodDetail likehoodDetail, decimal? nilaiExpose, int? updateBy, DateTime? updateDate)
        {
            this.NilaiExpose = nilaiExpose;
            this.RiskRegistrasiId = riskRegistrasi.Id;
            this.LikehoodDetailId = likehoodDetail.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void UpdateStageTahun(int stageTahunRiskMatrixId, int? updateBy, DateTime? updateDate)
        {
            this.StageTahunRiskMatrixId = stageTahunRiskMatrixId;
            this.IsUpdate = true;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }
    }
}
