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
    public class StageService : IStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStageRepository _stageRepository;

        public StageService(IUnitOfWork uow, IStageRepository stageRepository)
        {
            _unitOfWork = uow;
            _stageRepository = stageRepository;
        }

        #region Query
        public Stage Get(int id)
        {
            return _stageRepository.Get(id);
        }

        public IEnumerable<Stage> GetAll()
        {
            return _stageRepository.GetAll();
        }

        public void IsExistOnEditing(int id, string namaStage)
        {
            if (_stageRepository.IsExist(id, namaStage))
            {
                throw new ApplicationException(string.Format("Nama Stage {0} sudah ada.", namaStage));
            }
        }

        public void isExistOnAdding(string namaStage)
        {
            if (_stageRepository.IsExist(namaStage))
            {
                throw new ApplicationException(string.Format("Nama Stage {0} sudah ada.", namaStage));
            }
        }
        #endregion Query

        #region Manipulation
        public int Add(StageParam param)
        {
            int id;
            Validate.NotNull(param.NamaStage, "Nama Stage wajib diisi.");
            Validate.NotNull(param.Keterangan, "Keterangan wajib diisi.");

            isExistOnAdding(param.NamaStage);
            using (_unitOfWork)
            {
                Stage model = new Stage(param.NamaStage, param.Keterangan, param.CreateBy, param.CreateDate);
                _stageRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, StageParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Stage tidak ditemukan.");

            Validate.NotNull(param.NamaStage, "Nama Stage wajib diisi.");
            Validate.NotNull(param.Keterangan, "Keterangan wajib diisi.");

            IsExistOnEditing(id, param.NamaStage);
            using (_unitOfWork)
            {
                model.Update(param.NamaStage, param.Keterangan, param.UpdateBy, param.UpdateDate);
                _stageRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Stage tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _stageRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
