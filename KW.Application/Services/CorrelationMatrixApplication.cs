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
    public class CorrelationMatrixService : ICorrelationMatrixService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelationMatrixRepository _correlationMatrixRepository;

        public CorrelationMatrixService(IUnitOfWork unitOfWork, ICorrelationMatrixRepository correlationMatrixRepository)
        {
            _unitOfWork = unitOfWork;
            _correlationMatrixRepository = correlationMatrixRepository;
        }

        #region Query
        public IEnumerable<CorrelationMatrix> GetAll()
        {
            return _correlationMatrixRepository.GetAll(); 
        }

        public CorrelationMatrix Get(int id)
        {
            return _correlationMatrixRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string namaCorrelationMatrix)
        {
            if (_correlationMatrixRepository.IsExist(id, namaCorrelationMatrix))
            {
                throw new ApplicationException(string.Format("Nama Correlation Matrix {0} already exist.", namaCorrelationMatrix));
            }
        }

        public void isExistOnAdding(string namaCorrelationMatrix)
        {
            if (_correlationMatrixRepository.IsExist(namaCorrelationMatrix))
            {
                throw new ApplicationException(string.Format("Nama Correlation Matrix  {0} already exist.", namaCorrelationMatrix));
            }
        }
        #endregion Query

        #region Manipulation
        public int Add(CorrelationMatrixParam param)
        {
            int id;
            Validate.NotNull(param.NamaCorrelationMatrix, "Nama Correlation Matrix is required.");
            Validate.NotNull(param.Nilai, "Nilai is required.");

            isExistOnAdding(param.NamaCorrelationMatrix);
            using (_unitOfWork)
            {
                CorrelationMatrix model = new CorrelationMatrix(param.NamaCorrelationMatrix, param.Nilai, param.CreateBy, param.CreateDate);
                _correlationMatrixRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Update(int id, CorrelationMatrixParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(param.NamaCorrelationMatrix, "Nama Correlation Matrix is required.");
            Validate.NotNull(param.Nilai, "Nilai is required.");

            IsExistOnEditing(id, param.NamaCorrelationMatrix);
            using (_unitOfWork)
            {
                model.Update(param.NamaCorrelationMatrix, param.Nilai, param.UpdateBy, param.UpdateDate);
                _correlationMatrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;

        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Correlation Matrix is not found.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _correlationMatrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;

        }
        #endregion Manipulation
    }
}
