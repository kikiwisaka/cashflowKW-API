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
    public class StageTahunRiskMatrixService : IStageTahunRiskMatrixService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStageTahunRiskMatrixRepository _stageTahunRiskMatrixRepository;
        private readonly IRiskMatrixProjectRepository _riskMatrixProjectRepository;
        private readonly IStageRepository _stageRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILikehoodDetailRepository _likehoodDetailRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IStageTahunRiskMatrixDetailRepository _stageTahunRiskMatrixDetailRepository;

        public StageTahunRiskMatrixService(IUnitOfWork uow, IStageTahunRiskMatrixRepository stageTahunRiskMatrixRepository, IRiskMatrixProjectRepository riskMatrixProjectRepository, IStageRepository stageRepository, 
            ILikehoodDetailRepository likehoodDetailRepository, IProjectRepository projectRepository, IScenarioRepository scenarioRepository, IStageTahunRiskMatrixDetailRepository stageTahunRiskMatrixDetailRepository)
        {
            _unitOfWork = uow;
            _stageTahunRiskMatrixRepository = stageTahunRiskMatrixRepository;
            _riskMatrixProjectRepository = riskMatrixProjectRepository;
            _stageRepository = stageRepository;
            _likehoodDetailRepository = likehoodDetailRepository;
            _projectRepository = projectRepository;
            _scenarioRepository = scenarioRepository;
            _stageTahunRiskMatrixDetailRepository = stageTahunRiskMatrixDetailRepository;
        }

        #region Query
        public IEnumerable<StageTahunRiskMatrix> GetAll()
        {
            return _stageTahunRiskMatrixRepository.GetAll();
        }

        public StageTahunRiskMatrix Get(int id)
        {
            return _stageTahunRiskMatrixRepository.Get(id);
        }

        public StageTahunRiskMatrixParam GetByRiskMatrixProjectId(int riskMatrixProjectId)
        {
            StageTahunRiskMatrixParam stageTahunRiskMatrixParam = new StageTahunRiskMatrixParam();
            IList<StageValue> stageValues = new List<StageValue>();

            var data = _stageTahunRiskMatrixRepository.GetByRiskMatrixProjectId(riskMatrixProjectId);
            var stages = data.GroupBy(y => y.StageId).ToList();

            if (stages.Count > 0)
            {
                for (int r = 0; r < stages.Count(); r++)
                {
                    StageValue stageValue = new StageValue(); //temporary

                    List<int> values = new List<int>();
                    
                    stageValue.StageId = stages[r].Key;
                    List<int> years = new List<int>();

                    for (int s = 0; s < stages[r].Count(); s++)
                    {
                        
                        var staVal = stages[r].ToList();

                        var year = staVal[s].Tahun;
                        years.Add(year);
                    }
                    var thnMulai = years.Min();
                    var thnSelesai = years.Max();
                    values.Add(thnMulai);
                    values.Add(thnSelesai);
                    int[] valueExLi = values.ToArray();

                    stageValue.Values = valueExLi;
                    stageValues.Add(stageValue);
                }
                stageTahunRiskMatrixParam.StageValue = stageValues.ToArray();
            }
            return stageTahunRiskMatrixParam;            
        }

        public void IsExistOnEditing(int id, int riskMatrixProjectId)
        {
            if (_stageTahunRiskMatrixRepository.IsExist(id, riskMatrixProjectId))
            {
                throw new ApplicationException(string.Format("StageTahunRiskMatrix {0} Sudah Ada.", riskMatrixProjectId));
            }
        }

        public void isExistOnAdding(int riskMatrixProjectId)
        {
            if (_stageTahunRiskMatrixRepository.IsExist(riskMatrixProjectId))
            {
                throw new ApplicationException(string.Format("StageTahunRiskMatrix {0} Sudah Ada.", riskMatrixProjectId));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(StageTahunRiskMatrixParam param)
        {
            var id = 0;
            var riskMatrixProject = _riskMatrixProjectRepository.Get(param.RiskMatrixProjectId);
            var project = _projectRepository.Get(riskMatrixProject.Project.Id);
            IList<StageTahunRiskMatrix> stageTahun = new List<StageTahunRiskMatrix>();
            
            isExistOnAdding(param.RiskMatrixProjectId);
            using (_unitOfWork)
            {
                IList<int> generateYears = GenerateYear(param);

                for (int i = 0; i < generateYears.Count; i++)
                {

                    IList<int> generateStages = GenerateStage(param);
                    var stage = _stageRepository.Get(generateStages[i]);

                    StageTahunRiskMatrix model = new StageTahunRiskMatrix(riskMatrixProject, stage, generateYears[i], param.CreateBy, param.CreateDate);
                    _stageTahunRiskMatrixRepository.Insert(model);
                    stageTahun.Add(model);
                }
                var st = _riskMatrixProjectRepository.Get(riskMatrixProject.Id);
                st.AddStageTahun(stageTahun);

                _unitOfWork.Commit();
            }
            return id;
        }

        public int Update(int id, StageTahunRiskMatrixParam param)
        {
            var modelId = 0;
            var riskMatrixProject = _riskMatrixProjectRepository.Get(param.RiskMatrixProjectId);
            var project = _projectRepository.Get(riskMatrixProject.Project.Id);
            
            using (_unitOfWork)
            {
                //get old data
                var oldStageTahun = _stageTahunRiskMatrixRepository.GetByRiskMatrixProjectId(param.RiskMatrixProjectId).ToList();

                SetOldStageAndNewStage(oldStageTahun, param, riskMatrixProject);
                _unitOfWork.Commit();
            }
            return modelId;
        }
        
       
        #endregion Manipulation
        public  IList<int> GenerateYear (StageTahunRiskMatrixParam param)
        {
            IList<int> years = new List<int>();
            int thnAwal = param.StartProject.Year;
            int thnAkhir = param.EndProject.Year;
            int interval = thnAkhir - thnAwal;
            int[] tahun = new int[interval + 1];
            for (int i = 0; i <= interval; i++)
            {
                tahun[i] = i + thnAwal;
                var year = tahun[i];
                years.Add(year);
            }
            return years;
        }

        public IList<int> GenerateStage(StageTahunRiskMatrixParam param)
        {
            IList<int> contentStages = new List<int>();
            foreach (var item in param.StageValue)
                {
                    var stage = _stageRepository.Get(item.StageId);
                    Validate.NotNull(stage, "Stage is not found.");

                    var startStage = item.Values[0];
                    Validate.NotNull(startStage, "Start Stage is not found.");

                    var endStage = item.Values[1];
                    Validate.NotNull(endStage, "End Stage is not found.");

                    int intervalStage = endStage - startStage;
                    int[] content = new int[intervalStage + 1];
                    for (int j = 0; j <= intervalStage; j++)
                    {
                        content[j] = item.StageId;
                        var contents = content[j];
                        contentStages.Add(contents);
                    }
                }
            return contentStages;
        }
        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }

        private void SetOldStageAndNewStage(IList<StageTahunRiskMatrix> oldData, StageTahunRiskMatrixParam param, RiskMatrixProject riskMatrixProject)
        {
            Dictionary<int, dynamic> oldStage = new Dictionary<int, dynamic>();

            var newStage = GetNewStageTahun(param);

            if(oldData.Count > 0)
            {
                foreach (var item in oldData)
                {
                    oldStage.Add(item.Tahun, item.StageId);
                }
            }

            if(oldData.Count == newStage.Count)
            {
                List<StageTahunRiskMatrix> stageTahunRiskMatrix = new List<StageTahunRiskMatrix>();

                var differences = newStage.Except(oldStage);
                if (differences != null || differences.Count() > 0)
                {
                    foreach (var item in differences)
                    {
                        //update stage tahun risk matrix
                        var stage = _stageRepository.Get(item.Value);
                        var stageTahun = _stageTahunRiskMatrixRepository.GetByRiskMatrixProjectIdYear(riskMatrixProject.Id, item.Key);
                        stageTahun.Update(riskMatrixProject, stage, item.Key, param.UpdateBy, param.UpdateDate);
                        _stageTahunRiskMatrixRepository.Update(stageTahun);

                        //remove old stage tahun in risk matrix project
                        var riskMatPro = _riskMatrixProjectRepository.Get(riskMatrixProject.Id);
                        //RemoveOldStageInRiskMatrixProject(riskMatPro);

                        //insert new stage tahun in risk matrix project
                        stageTahunRiskMatrix.Add(stageTahun);
                        //riskMatPro.AddStageTahun(stageTahunRiskMatrix);

                        //update stage tahun risk matrix detail
                        var stageTahunDetail = _stageTahunRiskMatrixDetailRepository.GetByStageTahunRiskMatrixId(stageTahun.Id);
                        if(stageTahunDetail != null)
                        {
                            foreach (var detailValue in stageTahunDetail)
                            {
                                detailValue.UpdateStageTahun(stageTahun.Id, param.UpdateBy, param.UpdateDate);
                                _stageTahunRiskMatrixDetailRepository.Update(detailValue);
                            }
                        }
                    }
                }
            }
        }

        private Dictionary<int, dynamic> GetNewStageTahun(StageTahunRiskMatrixParam param)
        {
            Dictionary<int, dynamic> newStage = new Dictionary<int, dynamic>();
            IList<int> generateYears = GenerateYear(param);
            for (int i = 0; i < generateYears.Count; i++)
            {
                IList<int> generateStages = GenerateStage(param);
                newStage.Add(generateYears[i], generateStages[i]);
            }

            return newStage;
        }

        private void RemoveOldStageInRiskMatrixProject(RiskMatrixProject riskMatrixProject)
        {
            if(riskMatrixProject.StageTahunRiskMatrix != null)
            {
                foreach (var item in riskMatrixProject.StageTahunRiskMatrix)
                {
                    riskMatrixProject.RemoveStageTahun(item);
                }
            }
        }
    }
}
