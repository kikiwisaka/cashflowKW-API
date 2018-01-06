using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Linq;

namespace KW.Application
{
    public class ScenarioService : IScenarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IScenarioDetailRepository _scenarioDetailRepository;
        private readonly ILikehoodRepository _likehoodRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRiskMatrixRepository _riskMatrixRepository;
        private readonly ISektorRepository _sektorRepository;
        private readonly ICorrelationRiskAntarSektorRepository _correlationRiskAntarSektorRepository;
        private readonly ICorrelationRiskAntarProjectRepository _correlationRiskAntarProjectRepository;
        private readonly ICorrelatedSektorRepository _correlatedSectorRepository;
        private readonly IRiskMatrixProjectRepository _riskMatrixProjectRepository;
        private readonly ICorrelatedProjectRepository _correlatedProjectRepository;

        public ScenarioService(IUnitOfWork unitOfWork, IScenarioRepository scenarioRepository, IScenarioDetailRepository scenarioDetailRepository,
            ILikehoodRepository likehoodRepository, IProjectRepository projectRepository, IRiskMatrixRepository riskMatrixRepository,
            ISektorRepository sektorRepository, ICorrelationRiskAntarSektorRepository correlationRiskAntarSektorRepository,
            ICorrelationRiskAntarProjectRepository correlationRiskAntarProjectRepository, ICorrelatedSektorRepository correlatedSectorRepository,
            IRiskMatrixProjectRepository riskMatrixProjectRepository, ICorrelatedProjectRepository correlatedProjectRepository)
        {
            _unitOfWork = unitOfWork;
            _scenarioRepository = scenarioRepository;
            _scenarioDetailRepository = scenarioDetailRepository;
            _likehoodRepository = likehoodRepository;
            _projectRepository = projectRepository;
            _riskMatrixRepository = riskMatrixRepository;
            _sektorRepository = sektorRepository;
            _correlationRiskAntarSektorRepository = correlationRiskAntarSektorRepository;
            _correlationRiskAntarProjectRepository = correlationRiskAntarProjectRepository;
            _correlatedSectorRepository = correlatedSectorRepository;
            _riskMatrixProjectRepository = riskMatrixProjectRepository;
            _correlatedProjectRepository = correlatedProjectRepository;
        }

        #region Query
        public IEnumerable<Scenario> GetAll()
        {
            return _scenarioRepository.GetAll();
        }

        public Scenario Get(int id)
        {
            return _scenarioRepository.Get(id);
        }

        public Scenario GetDefault()
        {
            return _scenarioRepository.GetDefault();
        }

        public int SetDefault(int id, int? updateBy, DateTime? updateDate)
        {
            int modelId;
            var modelNewDefault = this.Get(id);
            Validate.NotNull(modelNewDefault, "Scenario is not found.");
            var modelOldDefault = this.GetDefault();

            var riskMatrix = _riskMatrixRepository.GetByScenarioId(id);
            Validate.NotNull(riskMatrix, "Risk Matrix is not found.");

            using (_unitOfWork)
            {
                if (modelOldDefault != null)
                {
                    //Remove current default
                    modelOldDefault.RemoveDefault(updateBy, updateDate);
                    _scenarioRepository.Update(modelOldDefault);
                }

                //set new default
                modelNewDefault.SetDefault(updateBy, updateDate);

                //set status risk matrix
                SetStatusRiskMatrix(modelNewDefault, modelOldDefault, updateBy, updateDate);
                //riskMatrix.Delete(updateBy, updateDate);

                //setRiskMatrixProject
                SetRiskMatrixProject(modelNewDefault, modelOldDefault, riskMatrix, updateBy, updateDate);

                //set scenarioId in CorrelatedSektor
                SetScenarioInCorrelatedSektor(modelNewDefault, updateBy, updateDate);

                //generate correlatedProject
                GenerateCorrelatedProject(modelNewDefault, updateBy, updateDate);

                _unitOfWork.Commit();
            }
            modelId = modelNewDefault.Id;

            return modelId;
        }

        public void RemoveDefault(int updateBy, DateTime updateDate)
        {
            throw new NotImplementedException();
        }

        public void IsExistOnEditing(int id, string namaScenario)
        {
            if (_scenarioRepository.IsExist(id, namaScenario))
            {
                throw new ApplicationException(string.Format("Nama Scenario {0} already exist.", namaScenario));
            }
        }

        public void isExistOnAdding(string namaScenario)
        {
            if (_scenarioRepository.IsExist(namaScenario))
            {
                throw new ApplicationException(string.Format("Nama Scenario {0} already exist.", namaScenario));
            }
        }
        #endregion Query

        #region Manipulation
        public int Add(ScenarioParam param)
        {
            int id;
            Validate.NotNull(param.NamaScenario, "Nama Scenario is required.");

            var likelihood = _likehoodRepository.Get(param.LikehoodId);
            Validate.NotNull(likelihood, "Likelihood is not found.");

            isExistOnAdding(param.NamaScenario);
            using (_unitOfWork)
            {
                Scenario model = new Scenario(likelihood, param.NamaScenario, param.CreateBy, param.CreateDate);
                _scenarioRepository.Insert(model);

                //insert scenario detail
                IList<ScenarioDetail> scenarioDetails = GenerateScenarioDetail(param, model);
                _scenarioDetailRepository.Insert(scenarioDetails);

                //insert into risk matrix
                RiskMatrix modelRiskMatrix = new RiskMatrix(model, param.CreateBy, param.CreateDate);
                _riskMatrixRepository.Insert(modelRiskMatrix);

                //insert into correlation risk antar sector
                var correlationRiskAntarSektor = GenereateCorrelationRiskAntarSektor(model, scenarioDetails, param.CreateBy, param.CreateDate);

               

                //insert into correlation risk antar secter detail
                //IList<CorrelationRiskAntarSektorDetail> correlationSektorDetails = GenerateCorrelationRiskAntarSektorDetail(modelCorrelationSektor, scenarioDetails, param.CreateBy, param.CreateDate);
                //_correlationRiskAntarSektorDetailRepository.Insert(correlationSektorDetails);

                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, ScenarioParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Scenario is not found.");

            var likelihood = _likehoodRepository.Get(param.LikehoodId);
            Validate.NotNull(likelihood, "Likelihood is not found.");

            IsExistOnEditing(id, param.NamaScenario);
            using (_unitOfWork)
            {
                model.Update(likelihood, param.NamaScenario, param.UpdateBy, param.UpdateDate);
                _scenarioRepository.Update(model);

                //delete old scenario detail
                RemoveOldScenarioDetail(id, model, param.UpdateBy, param.UpdateDate);

                //insert new scenario detail
                IList<ScenarioDetail> scenarioDetails = GenerateScenarioDetail(param, model);
                _scenarioDetailRepository.Insert(scenarioDetails);

                //insert into risk matrix project
                if (model.IsDefault)
                {
                    //Remove current risk matrix project
                    //RemoveCurrentRiskMatrixProject(model, param.UpdateBy, param.UpdateDate);

                    //insert into risk matrix project
                    var riskMatrix = _riskMatrixRepository.GetByScenarioId(id);
                    GenerateRiskMatrixProject(model, riskMatrix, param.UpdateBy, param.UpdateDate);
                }

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Scenario is not found.");

            RiskMatrix modelRiskMatrix = _riskMatrixRepository.GetByScenarioId(model.Id);
            Validate.NotNull(modelRiskMatrix, "Risk Matrix is not found.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _scenarioRepository.Update(model);

                //delete old project risk 
                RemoveOldScenarioDetail(id, model, deleteBy, deleteDate);

                //delete risk matrix
                modelRiskMatrix.Delete(deleteBy, deleteDate);

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation

        #region Private
        private IList<ScenarioDetail> GenerateScenarioDetail(ScenarioParam param, Scenario model)
        {
            IList<ScenarioDetail> scenarioDetails = new List<ScenarioDetail>();
            List<int> projectIds = new List<int>();

            if (param.ProjectId != null)
            {
                for (int i = 0; i < param.ProjectId.Length; i++)
                {
                    int n;
                    if (Int32.TryParse(param.ProjectId[i], out n))
                    {
                        if (!projectIds.Contains(n))
                            projectIds.Add(n);
                    }
                }

                foreach (var item in projectIds)
                {
                    var project = _projectRepository.Get(item);
                    Validate.NotNull(project, "Project " + project.NamaProject + " is not found.");

                    ScenarioDetail scenarioDetail = new ScenarioDetail(project, model.Id, param.CreateBy, param.CreateDate);
                    scenarioDetails.Add(scenarioDetail);
                }
                model.AddScenarioDetail(scenarioDetails);
            }
            return scenarioDetails;
        }

        private void RemoveOldScenarioDetail(int id, Scenario model, int? deleteBy, DateTime? deleteDate)
        {
            var scenarioDetail = _scenarioDetailRepository.GetByScenarioId(id);
            if (scenarioDetail != null)
            {
                foreach (var item in scenarioDetail)
                {
                    model.RemoveScenarioDetail(item);
                    _scenarioDetailRepository.Delete(item.Id);
                }
            }
        }

        private IList<CorrelationRiskAntarSektor> GenereateCorrelationRiskAntarSektor(Scenario scenario, IList<ScenarioDetail> scenarioDetails, int? createBy, DateTime? createDate)
        {
            IList<CorrelationRiskAntarSektor> collectionCorrelationSektor = new List<CorrelationRiskAntarSektor>();
            if(scenarioDetails.Count > 0)
            {
                foreach (var item in scenarioDetails)
                {
                    //Use for list of Correlation Risk Antar Project
                    CorrelationRiskAntarSektor modelCorrelationSektor = new CorrelationRiskAntarSektor(scenario, item.Project.Sektor, item.Project, createBy, createDate);
                    _correlationRiskAntarSektorRepository.Insert(modelCorrelationSektor);
                    collectionCorrelationSektor.Add(modelCorrelationSektor);
                }
            }
            //insert into correlated sector
            IList<CorrelatedSektor> correlatedSektors = GenerateCorrelatedSector(scenarioDetails, scenario, createBy, createDate);
            _correlatedSectorRepository.Insert(correlatedSektors);

            return collectionCorrelationSektor;
        }

        //private IList<CorrelationRiskAntarProject> GenerateCorrelationRiskAntarSektorDetail(CorrelationRiskAntarSektor correlationSektor, IList<ScenarioDetail> scenarioDetail, int? createBy, DateTime? createDate)
        //{
        //    Validate.NotNull(correlationSektor, "Correlation Risk Antar Sector is not found.");

        //    IList<CorrelationRiskAntarProject> correlationSektors = new List<CorrelationRiskAntarProject>();
        //    List<int> projectIds = new List<int>();

        //    foreach (var item in scenarioDetail)
        //    {
        //        projectIds.Add(item.Project.Id);
        //    }

        //    foreach (var item in projectIds)
        //    {
        //        var project = _projectRepository.Get(item);
        //        Validate.NotNull(project, "Project " + project.NamaProject + " is not found.");

        //        CorrelationRiskAntarProject correlationSektorDetail = new CorrelationRiskAntarProject(correlationSektor, project, project.Sektor, createBy, createDate);
        //        correlationSektors.Add(correlationSektorDetail);
        //    }
        //    return correlationSektors;
        //}

        private IList<CorrelatedSektor> GenerateCorrelatedSector(IList<ScenarioDetail> scenarioDetails, Scenario scenario, int? createBy, DateTime? createDate)
        {
            var sektors = _sektorRepository.GetAll();
            IList<Sektor> currentSektor = new List<Sektor>();
            IList<CorrelatedSektor> currentCorrelatedSektor = new List<CorrelatedSektor>();

            if(scenarioDetails != null)
            {
                foreach (var item in scenarioDetails)
                {
                    var sektor = item.Project.Sektor;
                    if (!currentSektor.Contains(sektor))
                    {
                        currentSektor.Add(sektor);
                        CorrelatedSektor correlatedSector = new CorrelatedSektor(sektor.NamaSektor, scenario, createBy, createDate);
                        currentCorrelatedSektor.Add(correlatedSector);
                    }
                }
            }
            
            return currentCorrelatedSektor;
        }

        private void GenerateRiskMatrixProject(Scenario scenario, RiskMatrix riskMatrix, int? createBy, DateTime? createDate)
        {
            
            var getScenarioDetail = scenario.ScenarioDetail;

            IList<int> oldProjects = new List<int>();
            IList<int> newProjects = new List<int>();
            IList<int> removeProjects = new List<int>();
            IList<int> addProjects = new List<int>();

            var currentProject = _riskMatrixProjectRepository.GetByScenarioId(scenario.Id);
            foreach (var pro in currentProject)
            {
                oldProjects.Add(pro.ProjectId);
            }

            foreach (var item in getScenarioDetail)
            {
                var newProject = item.Project;
                newProjects.Add(newProject.Id);
            }

            if(newProjects.Count > 0)
            {
                foreach (var item in newProjects)
                {
                    var temData = _riskMatrixProjectRepository.GetByProjectId(item);
                    if (temData == null)
                        addProjects.Add(item);
                }

            }

            if(oldProjects.Count > 0)
            {
                var tempNeedToRemove = oldProjects.Except(newProjects);
                if(tempNeedToRemove.Count() > 0)
                {
                    foreach (var item in tempNeedToRemove)
                    {
                        removeProjects.Add(item);
                    }
                }
            }
            
            //add new project
            if(addProjects.Count > 0)
            {
                IList<RiskMatrixProject> addRiskMatrixProjects = new List<RiskMatrixProject>();

                foreach (var item in addProjects)
                {
                    var project = _projectRepository.Get(item);
                    if (project != null)
                    {
                        RiskMatrixProject riskMatrixProject = new RiskMatrixProject(project, riskMatrix, scenario, createBy, createDate);
                        addRiskMatrixProjects.Add(riskMatrixProject);
                    }
                }
                _riskMatrixProjectRepository.Insert(addRiskMatrixProjects);
            }

            //remove project
            if(removeProjects.Count > 0)
            {
                foreach (var item in removeProjects)
                {
                    var project = _riskMatrixProjectRepository.GetByProjectId(item);
                    if (project != null)
                    {
                        _riskMatrixProjectRepository.Delete(project.Id);
                    }
                }
            }
        }

        private void RemoveCurrentRiskMatrixProject(Scenario scenarioDefault, int? updateBy, DateTime? updateDate)
        {
            var riskMatrixProject = _riskMatrixProjectRepository.GetByScenarioId(scenarioDefault.Id);
            foreach (var item in riskMatrixProject)
            {
                item.Delete(updateBy, updateDate);
                _riskMatrixProjectRepository.Update(item);
            }
        }

        private void SetStatusRiskMatrix(Scenario scenarioNewDefault, Scenario scenarioOldDefault, int? createBy, DateTime? createDate)
        {
            RiskMatrix oldRiskMatrix = null;
            RiskMatrix newRiskMatrix = null;

            if (scenarioOldDefault != null)
            {
                oldRiskMatrix = _riskMatrixRepository.GetByScenarioId(scenarioOldDefault.Id);
            }

            if (scenarioNewDefault != null)
            {
                newRiskMatrix = _riskMatrixRepository.GetByScenarioId(scenarioNewDefault.Id);
            }

            if (oldRiskMatrix != null)
            {
                oldRiskMatrix.Delete(createBy, createDate);
                _riskMatrixRepository.Update(oldRiskMatrix);
            }

            if(newRiskMatrix != null)
            {
                newRiskMatrix.SetActive(createBy, createDate);
                _riskMatrixRepository.Update(newRiskMatrix);
            }
        }

        private void SetRiskMatrixProject(Scenario scenarioNewDefault, Scenario scenarioOldDefault, RiskMatrix riskMatrix, int? createBy, DateTime? createDate)
        {
            IList<RiskMatrixProject> oldRiskMatrixProject = null;
            IList<RiskMatrixProject> newRiskMatrixProject = null;
            var projects = _scenarioDetailRepository.GetByScenarioId(scenarioNewDefault.Id).ToList();

            if (scenarioOldDefault != null)
            {
                oldRiskMatrixProject = _riskMatrixProjectRepository.GetByScenarioId(scenarioOldDefault.Id).ToList();
            }

            if(scenarioNewDefault != null)
            {
                newRiskMatrixProject = _riskMatrixProjectRepository.GetAllByScenarioId(scenarioNewDefault.Id).ToList();
            }

            if (oldRiskMatrixProject != null)
            {
                foreach (var item in oldRiskMatrixProject)
                {
                    item.Delete(createBy, createDate);
                    _riskMatrixProjectRepository.Update(item);
                }
            } 
            else
            {
                if(projects.Count > 0)
                {
                    foreach (var item in projects)
                    {
                        RiskMatrixProject model = new RiskMatrixProject(item.Project, riskMatrix, scenarioNewDefault, createBy, createDate);
                        _riskMatrixProjectRepository.Insert(model);
                    }
                }
            }

            if(newRiskMatrixProject.Count > 0)
            {
                foreach (var item in newRiskMatrixProject)
                {
                    item.SetActive(createBy, createDate);
                    _riskMatrixProjectRepository.Update(item);

                    //add risk matrix project into Correlation Risk Antra Sektor
                    var correlationRiskAntarSektor = _correlationRiskAntarSektorRepository.GetByProjectIdScenarioId(item.ProjectId, item.ScenarioId);
                    correlationRiskAntarSektor.AddRiskMatrixProject(item.Id, createBy, createDate);
                    _correlationRiskAntarSektorRepository.Update(correlationRiskAntarSektor);
                }
            }
            else
            {
                if (projects.Count > 0)
                {
                    foreach (var item in projects)
                    {
                        RiskMatrixProject model = new RiskMatrixProject(item.Project, riskMatrix, scenarioNewDefault, createBy, createDate);
                        _riskMatrixProjectRepository.Insert(model);

                        //add risk matrix project into Correlation Risk Antra Sektor
                        var correlationRiskAntarSektor = _correlationRiskAntarSektorRepository.GetByProjectIdScenarioId(model.ProjectId, model.ScenarioId);
                        correlationRiskAntarSektor.AddRiskMatrixProject(model.Id, createBy, createDate);
                        _correlationRiskAntarSektorRepository.Update(correlationRiskAntarSektor);
                    }
                }
            }
        }

        private void SetScenarioInCorrelatedSektor(Scenario scenarioNewDefault, int? updateBy, DateTime? updateDate)
        {
            var correlatedSektor = _correlatedSectorRepository.GetByScenarioIdIsZero().ToList();
            if(correlatedSektor.Count > 0)
            {
                foreach (var item in correlatedSektor)
                {
                    item.Update(scenarioNewDefault, updateBy, updateDate);
                    _correlatedSectorRepository.Update(item);
                }
            }
        }

        private void GenerateCorrelatedProject(Scenario scenarioNewDefault, int? updateBy, DateTime? updateDate)
        {
            var correlationRiskAntarSektor = _correlationRiskAntarSektorRepository.GetByScenarioDefault(scenarioNewDefault.Id).ToList();
            if(correlationRiskAntarSektor.Count > 0)
            {
                var correlatedProject = _correlatedProjectRepository.GetByScenarioId(scenarioNewDefault.Id).ToList();
                if(correlatedProject.Count > 0)
                {
                    foreach (var item in correlatedProject)
                    {
                        _correlatedProjectRepository.Delete(item.Id);
                    }
                }

                foreach (var item in correlationRiskAntarSektor)
                {
                    CorrelatedProject model = new CorrelatedProject(scenarioNewDefault, item.ProjectId, item.SektorId, updateBy, updateDate);
                    _correlatedProjectRepository.Insert(model);
                }
            }
        }

        #endregion Private
    }
}