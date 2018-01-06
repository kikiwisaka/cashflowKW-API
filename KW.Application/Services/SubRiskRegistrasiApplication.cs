using KW.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class SubRiskRegistrasiService : ISubRiskRegistrasiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubRiskRegistrasiRepository _subRiskRegistrasiRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;

        public SubRiskRegistrasiService(IUnitOfWork uow, ISubRiskRegistrasiRepository subRiskRegistrasiRepository, IRiskRegistrasiRepository riskRegistrasiRepository)
        {
            _unitOfWork = uow;
            _subRiskRegistrasiRepository = subRiskRegistrasiRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
        }

        #region Query
        public IEnumerable<SubRiskRegistrasi> GetAll()
        {
            return _subRiskRegistrasiRepository.GetAll();
        }

        public IEnumerable<SubRiskRegistrasi> GetByRiskId(int riskId)
        {
            return _subRiskRegistrasiRepository.GetByRiskId(riskId);
        }
        public SubRiskRegistrasi Get(int id)
        {
            return _subRiskRegistrasiRepository.Get(id);
        }

        public void IsExistOnEditing(int id, int mRiskId, string kodeRisk, string riskEvenClaim, string descriptionRiskEvenClaim, string sugestionMigration)
        {
            if (_subRiskRegistrasiRepository.IsExist(id, kodeRisk))
            {
                throw new ApplicationException(string.Format("Kode Risk {0} sudah ada.", kodeRisk));
            }
        }

        public void isExistOnAdding(string kodeRisk)
        {
            if (_subRiskRegistrasiRepository.IsExist(kodeRisk))
            {
                throw new ApplicationException(string.Format("Kode Risk {0} Sudah ada.", kodeRisk));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(SubRiskRegistrasiParam param)
        {
            int id;
            Validate.NotNull(param.KodeRisk, "Kode Risk is required.");

            RiskRegistrasi modelParent = _riskRegistrasiRepository.Get(param.RiskRegistrasiId);
            Validate.NotNull(modelParent, "Risk is not found.");

            isExistOnAdding(param.KodeRisk);
            using (_unitOfWork)
            {
                SubRiskRegistrasi model = new SubRiskRegistrasi(modelParent, param.KodeRisk, param.RiskEvenClaim, param.DescriptionRiskEvenClaim, param.SugestionMigration, param.CreateBy, param.CreateDate);
                _subRiskRegistrasiRepository.Insert(model);

                //add detail into model parent
                modelParent.AddRiskRegistrasiDetail(model);

                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, SubRiskRegistrasiParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Kode Risk is not found.");

            RiskRegistrasi modelParent = _riskRegistrasiRepository.Get(param.RiskRegistrasiId);
            Validate.NotNull(modelParent, "Risk is not found.");

            IsExistOnEditing(id, param.RiskRegistrasiId, param.KodeRisk, param.RiskEvenClaim, param.DescriptionRiskEvenClaim, param.SugestionMigration);
            using (_unitOfWork)
            {
                model.Update(modelParent, param.KodeRisk, param.RiskEvenClaim, param.DescriptionRiskEvenClaim, param.SugestionMigration, param.UpdateBy, param.UpdateDate);
                _subRiskRegistrasiRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        
        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Kode Risk is not found.");

            RiskRegistrasi modelParent = _riskRegistrasiRepository.Get(model.RiskRegistrasiId);
            Validate.NotNull(modelParent, "Risk is not found.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _subRiskRegistrasiRepository.Delete(model.Id);
                //_subRiskRegistrasiRepository.Update(model);

                //remove current detail from model parent
                modelParent.RemoveRiskRegistrasiDetail(model);

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
