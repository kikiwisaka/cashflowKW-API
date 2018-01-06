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
    public class RiskMatrixStageService : IRiskMatrixStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRiskMatrixStageRepository _riskMatrixStageRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRiskMatrixRepository _riskMatrixRepository;
        private readonly IStageRepository _stageRepository;

        public RiskMatrixStageService(IUnitOfWork uow, IRiskMatrixStageRepository riskMatrixStageRepository, IProjectRepository projectRepository, IRiskMatrixRepository riskMatrixRepository, IStageRepository stageRepository)
        {
            _unitOfWork = uow;
            _riskMatrixStageRepository = riskMatrixStageRepository;
            _projectRepository = projectRepository;
            _riskMatrixRepository = riskMatrixRepository;
            _stageRepository = stageRepository;

        }

        #region Query
        public IEnumerable<RiskMatrixStage> GetAll()
        {
            return _riskMatrixStageRepository.GetAll();
        }

        public RiskMatrixStage Get(int id)
        {
            return _riskMatrixStageRepository.Get(id);
        }

        public void IsExistOnEditing(int id, int stageId)
        {
            if (_riskMatrixStageRepository.IsExist(id, stageId))
            {
                throw new ApplicationException(string.Format("Stage already exist.", stageId));
            }
        }

        public void isExistOnAdding(int stageId)
        {
            if (_riskMatrixStageRepository.IsExist(stageId))
            {
                throw new ApplicationException(string.Format("Stage {0} already exist.", stageId));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(RiskMatrixStageParam param)
        {
            int id;
            var project = _projectRepository.Get(param.ProjectId);
            var riskMatrix = _riskMatrixRepository.Get(param.RiskMatrixId);
            var stage = _stageRepository.Get(param.StageId);
            Validate.NotNull(param.StageId, "Stage wajib diisi.");

            isExistOnAdding(param.StageId);
            using (_unitOfWork)
            {
                RiskMatrixStage model = new RiskMatrixStage(project, riskMatrix, stage, param.CreateBy, param.CreateDate);
                _riskMatrixStageRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, RiskMatrixStageParam param)
        {
            var model = this.Get(id);
            var project = _projectRepository.Get(param.ProjectId);
            var riskMatrix = _riskMatrixRepository.Get(param.RiskMatrixId);
            var stage = _stageRepository.Get(param.StageId);
            Validate.NotNull(model, "Stage tidak ditemukan.");

            IsExistOnEditing(id, param.StageId);
            using (_unitOfWork)
            {
                model.Update(project, riskMatrix, stage, param.UpdateBy, param.UpdateDate);
                _riskMatrixStageRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        
        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Category Risk tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _riskMatrixStageRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
