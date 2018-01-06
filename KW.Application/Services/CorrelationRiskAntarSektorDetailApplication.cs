//using KW.Common.Validation;
//using System;
//using System.Collections.Generic;
//using KW.Application.Params;
//using KW.Core;
//using KW.Domain;

//namespace KW.Application
//{
//    public class CorrelationRiskAntarProjectService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly ICorrelationRiskAntarSektorRepository _correlationRiskAntarSektorRepository;
//        private readonly ICorrelationRiskAntarProjectRepository _correlationRiskAntarProjectRepository;
//        private readonly IProjectRepository _projectRepository;
//        private readonly ISektorRepository _sektorRepository;

//        public CorrelationRiskAntarProjectService(IUnitOfWork unitOfWork, ICorrelationRiskAntarSektorRepository correlationRiskAntarSektorRepository,
//            ICorrelationRiskAntarProjectRepository correlationRiskAntarProjectRepository, IProjectRepository projectRepository, ISektorRepository sektorRepository)
//        {
//            _unitOfWork = unitOfWork;
//            _correlationRiskAntarSektorRepository = correlationRiskAntarSektorRepository;
//            _correlationRiskAntarProjectRepository = correlationRiskAntarProjectRepository;
//            _projectRepository = projectRepository;
//            _sektorRepository = sektorRepository;
//        }

//        #region Query
//        public IEnumerable<CorrelationRiskAntarProject> GetAll()
//        {
//            return _correlationRiskAntarProjectRepository.GetAll();
//        }

//        public IEnumerable<CorrelationRiskAntarProject> GetByScenarioDefaultId(int scenarioId)
//        {
//            return _correlationRiskAntarProjectRepository.GetByCorrelationRiskAntarSectorId(scenarioId);
//        }

//        public IEnumerable<CorrelationRiskAntarProject> GetByCorrelationAntarSektoId(int correlationAntarSektorId)
//        {
//            return _correlationRiskAntarProjectRepository.GetByCorrelationRiskAntarSectorId(correlationAntarSektorId);
//        }

//        public CorrelationRiskAntarProject Get(int id)
//        {
//            return _correlationRiskAntarProjectRepository.Get(id);
//        }
//        #endregion Query

//        #region Manipulation
//        public int Addx(CorrelationRiskAntarProjectParam param)
//        {
//            //int id;

//            //CorrelationRiskAntarSector correlationSector = _correlationRiskAntarSectorRepository.Get(param.CorrelationRiskAntarSectorId);
//            //Validate.NotNull(correlationSector, "Correlation Risk Antar Sector is required.");

//            //Project project = _projectRepository.Get(param.ProjectId);
//            //Validate.NotNull(project, "Project is required.");

//            //Sektor sektor = _sektorRepository.Get(param.SektorId);
//            //Validate.NotNull(sektor, "Sektor is required.");

//            //using (_unitOfWork)
//            //{

//            //}
//            throw new NotImplementedException();

//        }

//        public int Update(int id, CorrelationRiskAntarProjectParam param)
//        {
//            throw new NotImplementedException();
//        }

//        public int Delete(int id, int deleteBy, DateTime deleteDate)
//        {
//            throw new NotImplementedException();
//        }

//        public CorrelationRiskAntarProjectParam GetByCorrelationRiskAntarSektorId(int correlationRiskAntarSektorId)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion Manipulation
//    }
//}
