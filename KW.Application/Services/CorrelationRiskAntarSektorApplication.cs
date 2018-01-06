using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Linq;

namespace KW.Application
{
    public class CorrelationRiskAntarSektorService : ICorrelationRiskAntarSektorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelationRiskAntarSektorRepository _correlationRiskAntarSektorRepository;
        private readonly ICorrelationRiskAntarProjectRepository _correlationRiskAntarProjectRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISektorRepository _sektorRepository;

        public CorrelationRiskAntarSektorService(IUnitOfWork unitOfWork, ICorrelationRiskAntarSektorRepository correlationRiskAntarSektorRepository,
            ICorrelationRiskAntarProjectRepository correlationRiskAntarProjectRepository, IScenarioRepository scenarioRepository,
            IProjectRepository projectRepository, ISektorRepository sektorRepository)
        {
            _unitOfWork = unitOfWork;
            _correlationRiskAntarSektorRepository = correlationRiskAntarSektorRepository;
            _correlationRiskAntarProjectRepository = correlationRiskAntarProjectRepository;
            _scenarioRepository = scenarioRepository;
            _projectRepository = projectRepository;
            _sektorRepository = sektorRepository;
        }

        #region Query
        public IEnumerable<CorrelationRiskAntarSektor> GetAll()
        {
            IList<CorrelationRiskAntarSektor> filtered = new List<CorrelationRiskAntarSektor>();
            IList<int> sektors = new List<int>();
            var allData = _correlationRiskAntarSektorRepository.GetAll();

            if(allData != null)
            {
                foreach (var item in allData)
                {
                    if(sektors.Count < 1)
                    {
                        sektors.Add(item.SektorId);
                    }
                    foreach (var sektorId in sektors)
                    {
                    }
                }
            }
            return _correlationRiskAntarSektorRepository.GetAll();
        }

        public CorrelationRiskAntarSektor Get(int id)
        {
            return _correlationRiskAntarSektorRepository.Get(id);
        }

        public CorrelationRiskAntarSektor GetByScenarioId(int scenarioId)
        {
            return _correlationRiskAntarSektorRepository.GetByScenarioId(scenarioId);
        }

        public IEnumerable<CorrelationRiskAntarSektor> GetByScenarioDefault(int scenarioId)
        {
            return _correlationRiskAntarSektorRepository.GetByScenarioDefault(scenarioId);
        }

        public void IsExist(int scenarioId)
        {
            if(_correlationRiskAntarSektorRepository.IsExist(scenarioId))
            {
                throw new ApplicationException(string.Format("The Scenario {0} already exist.", scenarioId));
            }
        }

        IList<Sektor> ICorrelationRiskAntarSektorService.GetSektorList()
        {
            var correlationSectors = this.GetAll().ToList();
            var sektors = _sektorRepository.GetAll();
            IList<Sektor> currentSektor = new List<Sektor>();
            IList<Project> projects = new List<Project>();

            for (int i = 0; i < correlationSectors.Count(); i++)
            {
                foreach (var item in correlationSectors[i].Scenario.ScenarioDetail)
                {
                    var sector = item.Project.Sektor;
                    if (!currentSektor.Contains(sector))
                        currentSektor.Add(sector);
                }
            }
            return currentSektor;
        }
        #endregion Query

        #region Manipulation
        public int Add(CorrelationRiskAntarSektorParam param)
        {
            int id = 0;

            Scenario scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(scenario, "Scenario is required.");

            //Project project = _projectRepository.Get(param.ProjectId);
            //Validate.NotNull(project, "Project is required.");

            //Sektor sektor = _sektorRepository.Get(param.SektorId);
            //Validate.NotNull(sektor, "Sektor is required.");

            IsExist(param.ScenarioId);
            using (_unitOfWork)
            {
                //CorrelationRiskAntarSektor model = new CorrelationRiskAntarSektor(scenario, param.CreateBy, param.CreateDate);
                //_correlationRiskAntarSektorRepository.Insert(model);

                _unitOfWork.Commit();
                //id = model.Id;
            }
            return id;
        }

        public int Update(int id, CorrelationRiskAntarSektorParam param)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Correlation Risk Antar Sector is not found.");

            Scenario scenario = _scenarioRepository.Get(model.ScenarioId);
            Validate.NotNull(scenario, "Scenario is required.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _correlationRiskAntarSektorRepository.Update(model);

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public IEnumerable<Project> GetProjectByScenarioDefault(int scenarioId)
        {
            var data = _correlationRiskAntarSektorRepository.GetByScenarioDefault(scenarioId).ToList();
            IList<Project> projects = new List<Project>();

            if(data.Count > 0)
            {
                foreach (var item in data)
                {
                    var project = item.Project;
                    projects.Add(project);
                }
            }

            return projects;
        }
        #endregion Manipulation
    }
}
