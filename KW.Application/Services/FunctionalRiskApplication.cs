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
    public class FunctionalRiskService : IFunctionalRiskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFunctionalRiskRepository _functionalRiskRepository;
        private readonly IMatrixRepository _matrixRepository;
        private readonly IColorCommentRepository _colorCommentRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public FunctionalRiskService(IUnitOfWork uow, IFunctionalRiskRepository functionalRiskRepository, IMatrixRepository matrixRepository, IColorCommentRepository colorCommentRepository, IScenarioRepository scenarioRepository)
        {
            _unitOfWork = uow;
            _functionalRiskRepository = functionalRiskRepository;
            _matrixRepository = matrixRepository;
            _colorCommentRepository = colorCommentRepository;
            _scenarioRepository = scenarioRepository;
        }

        #region Query
        public IEnumerable<FunctionalRisk> GetAll()
        {
            return _functionalRiskRepository.GetAll();
        }

        public FunctionalRisk Get(int id)
        {
            return _functionalRiskRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string definisi)
        {
            if (_functionalRiskRepository.IsExist(id, definisi))
            {
                throw new ApplicationException(string.Format("Functional Risk {0} sudah ada.", definisi));
            }
        }

        public void isExistOnAdding(string definisi)
        {
            if (_functionalRiskRepository.IsExist(definisi))
            {
                throw new ApplicationException(string.Format("Functional Risk {0} sudah ada.", definisi));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(FunctionalRiskParam param)
        {
            int id = 0;
            var matrixAll = _matrixRepository.GetAll().ToList();
            var colorCommentAll = _colorCommentRepository.GetAll().ToList();

            using (_unitOfWork)
            {
                for (int i = 0; i < matrixAll.Count; i++)
                {
                    for (int j = 0; j < colorCommentAll.Count; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            var matrix = _matrixRepository.Get(matrixAll[i].Id);
                            var colorComment = _colorCommentRepository.Get(colorCommentAll[j].Id);
                            var scenario = _scenarioRepository.Get(param.Scenarios[k]);
                            FunctionalRisk model = new FunctionalRisk(matrix, colorComment, scenario, param.Definisi, param.CreateBy, param.CreateDate);
                            _functionalRiskRepository.Insert(model);
                            _unitOfWork.Commit();
                        }
                    }
                }
            }
            return id;
        }

        public int Update(int id, FunctionalRiskParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Functional Risk tidak ditemukan.");

            var matrix = _matrixRepository.Get(param.MatrixId);
            var colorComment = _colorCommentRepository.Get(param.ColorCommentId);
            var scenario = _scenarioRepository.Get(param.ScenarioId);

            IsExistOnEditing(id, param.Definisi);
            using (_unitOfWork)
            {
                model.Update(matrix, colorComment, scenario, param.Definisi, param.UpdateBy, param.UpdateDate);
                _functionalRiskRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Functional Risk tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _functionalRiskRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
