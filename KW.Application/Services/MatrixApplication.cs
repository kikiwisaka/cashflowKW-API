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
    public class MatrixService : IMatrixService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatrixRepository _matrixRepository;

        public MatrixService(IUnitOfWork uow, IMatrixRepository matrixRepository)
        {
            _unitOfWork = uow;
            _matrixRepository = matrixRepository;
        }

        #region Query
        public IEnumerable<Matrix> GetAll()
        {
            return _matrixRepository.GetAll();
        }

        public Matrix Get(int id)
        {
            return _matrixRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string namaMatrix, string namaFormula)
        {
            if (_matrixRepository.IsExist(id, namaMatrix))
            {
                throw new ApplicationException(string.Format("Nama Matrix {0} sudah ada.", namaMatrix));
            }
        }

        public void isExistOnAdding(string namaMatrix)
        {
            if (_matrixRepository.IsExist(namaMatrix))
            {
                throw new ApplicationException(string.Format("Nama Matrix {0} sudah ada.", namaMatrix));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(MatrixParam param)
        {
            int id;
            Validate.NotNull(param.NamaMatrix, "Nama Matrix wajib diisi.");

            isExistOnAdding(param.NamaMatrix);
            using (_unitOfWork)
            {
                Matrix model = new Matrix(param.NamaMatrix, param.NamaFormula, param.CreateBy, param.CreateDate);
                _matrixRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, MatrixParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Matrix tidak ditemukan.");

            IsExistOnEditing(id, param.NamaMatrix, param.NamaFormula);
            using (_unitOfWork)
            {
                model.Update(param.NamaMatrix, param.NamaFormula, param.UpdateBy, param.UpdateDate);
                _matrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Matrix tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _matrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
