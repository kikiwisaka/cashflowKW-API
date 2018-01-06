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
    public class TahapanService : ITahapanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITahapanRepository _tahapanRepository;

        public TahapanService(IUnitOfWork uow, ITahapanRepository tahapanRepository)
        {
            _unitOfWork = uow;
            _tahapanRepository = tahapanRepository;
        }

        #region Query
        public IEnumerable<Tahapan> GetAll()
        {
            return _tahapanRepository.GetAll();
        }

        public Tahapan Get(int id)
        {
            return _tahapanRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string namaTahapan, string keterangan)
        {
            if(_tahapanRepository.IsExist(id, namaTahapan))
            {
                throw new ApplicationException(string.Format("Nama Tahapan {0} sudah ada.", namaTahapan));
            }
        }

        public void isExistOnAdding(string namaTahapan)
        {
            if(_tahapanRepository.IsExist(namaTahapan))
            {
                throw new ApplicationException(string.Format("Nama Tahapan {0} sudah ada.", namaTahapan));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(TahapanParam param)
        {
            int id;
            Validate.NotNull(param.NamaTahapan, "Nama Tahapan wajib diisi.");

            isExistOnAdding(param.NamaTahapan);
            using (_unitOfWork)
            {
                Tahapan model = new Tahapan(param.NamaTahapan, param.Keterangan, param.CreateBy, param.CreateDate);
                _tahapanRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, TahapanParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Tahapan tidak ditemukan.");

            IsExistOnEditing(id, param.NamaTahapan, param.Keterangan);
            using (_unitOfWork)
            {
                model.Update(param.NamaTahapan, param.Keterangan, param.UpdateBy, param.UpdateDate);
                _tahapanRepository.Update(model);
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
                _tahapanRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
