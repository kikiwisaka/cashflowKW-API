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
    public class PMNService : IPMNService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPMNRepository _pmnRepository;

        public PMNService(IUnitOfWork uow, IPMNRepository pmnRepository)
        {
            _unitOfWork = uow;
            _pmnRepository = pmnRepository;
        }

        #region Query
        public IEnumerable<PMN> GetAll()
        {
            return _pmnRepository.GetAll();
        }

        public PMN Get(int id)
        {
            return _pmnRepository.Get(id);
        }

        public void IsExistOnEditing(int id, int pmnToModalDasarCap, decimal recourseDelay, decimal delayYears, decimal opexGrowth, decimal opex, bool? status)
        {
            if (_pmnRepository.IsExist(id, pmnToModalDasarCap))
            {
                throw new ApplicationException(string.Format("PMN To Modal Dasar Capital {0} sudah ada.", pmnToModalDasarCap));
            }
        }

        public void isExistOnAdding(int pmnToModalDasarCap)
        {
            if (_pmnRepository.IsExist(pmnToModalDasarCap))
            {
                throw new ApplicationException(string.Format("PMN To Modal Dasar Capital {0} sudah ada.", pmnToModalDasarCap));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(PMNParam param)
        {
            int id;
            Validate.NotNull(param.PMNToModalDasarCap, "PMN To Modal Dasar Capital wajib diisi.");

            isExistOnAdding(param.PMNToModalDasarCap);
            using (_unitOfWork)
            {
                PMN model = new PMN(param.PMNToModalDasarCap, param.RecourseDelay, param.DelayYears, param.OpexGrowth, param.Opex, param.CreateBy, param.CreateDate, param.Status, param.ValuePMNToModalDasarCap);
                _pmnRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, PMNParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "PMN To Modal Dasar Capital tidak ditemukan.");

            IsExistOnEditing(id, param.PMNToModalDasarCap, param.RecourseDelay, param.DelayYears, param.OpexGrowth, param.Opex, param.Status);
            using (_unitOfWork)
            {
                model.Update(param.PMNToModalDasarCap, param.RecourseDelay, param.DelayYears, param.OpexGrowth, param.Opex, param.UpdateBy, param.UpdateDate, param.Status, param.ValuePMNToModalDasarCap);
                _pmnRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "PMN To Modal Dasar Capital tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _pmnRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
