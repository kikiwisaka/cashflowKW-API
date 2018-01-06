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
    public class SektorService : ISektorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISektorRepository _sektorRepository;

        public SektorService(IUnitOfWork uow, ISektorRepository sektorRepository)
        {
            _unitOfWork = uow;
            _sektorRepository = sektorRepository;
        }

        #region Query
        public IEnumerable<Sektor> GetAll()
        {
            return _sektorRepository.GetAll();
        }

        public Sektor Get(int id)
        {
            return _sektorRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string namaSektor, decimal minimum, decimal maximum, string definisi)
        {
            if (_sektorRepository.IsExist(id, namaSektor))
            {
                throw new ApplicationException(string.Format("Nama Sektor {0} sudah ada.", namaSektor));
            }
        }

        public void isExistOnAdding(string namaSektor)
        {
            if (_sektorRepository.IsExist(namaSektor))
            {
                throw new ApplicationException(string.Format("Nama Sektor {0} sudah ada.", namaSektor));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(SektorParam param)
        {
            int id;
            Validate.NotNull(param.NamaSektor, "Nama Sektor wajib diisi.");

            isExistOnAdding(param.NamaSektor);
            using (_unitOfWork)
            {
                Sektor model = new Sektor(param.NamaSektor, param.Minimum, param.Maximum, param.Definisi, param.CreateBy, param.CreateDate);
                _sektorRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, SektorParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Sektor tidak ditemukan.");

            IsExistOnEditing(id, param.NamaSektor, param.Minimum, param.Maximum, param.Definisi);
            using (_unitOfWork)
            {
                model.Update(param.NamaSektor, param.Minimum, param.Maximum, param.Definisi, param.UpdateBy, param.UpdateDate);
                _sektorRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Tahapan tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _sektorRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
