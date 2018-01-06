using System;
using KW.Application;
using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Application.Params;

namespace KW.Application
{
    public class DashboardRiskCapitalProjectService : IDashboardRiskCapitalProjectService
    {
        private readonly IRiskMatrixProjectRepository _riskMatrixProjectRepository;
        private readonly IStageTahunRiskMatrixRepository _stageTahunRiskMatrixRepository;
        private readonly IStageTahunRiskMatrixDetailRepository _stageTahunRiskMatrixDetailRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;
        private readonly ILikehoodDetailRepository _likehoodDetailRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public DashboardRiskCapitalProjectService(IRiskMatrixProjectRepository riskMatrixProjectRepository, IStageTahunRiskMatrixRepository stageTahunRiskMatrixRepository, IStageTahunRiskMatrixDetailRepository stageTahunRiskMatrixDetailRepository,
            IRiskRegistrasiRepository riskRegistrasiRepository, ILikehoodDetailRepository likehoodDetailRepository, IScenarioRepository scenarioRepository)
        {
            _riskMatrixProjectRepository = riskMatrixProjectRepository;
            _stageTahunRiskMatrixRepository = stageTahunRiskMatrixRepository;
            _stageTahunRiskMatrixDetailRepository = stageTahunRiskMatrixDetailRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
            _likehoodDetailRepository = likehoodDetailRepository;
            _scenarioRepository = scenarioRepository;
        }

        public IList<DashboardRiskCapitalProject> GetDiversifedRiskCapitalProject()
        {
            var scenarioDefault = _scenarioRepository.GetDefault();
            var likelihoodDetail = scenarioDefault.Likehood.LikehoodDetails;
            var projects = scenarioDefault.ScenarioDetail.Where(x => x.IsDelete == false).ToList();
            var riskRegistrasi = _riskRegistrasiRepository.GetAll().ToList();

            IList<UndiversifiedResult> undiversifiedResultCollection = new List<UndiversifiedResult>();
            UndiversifiedResult undiversifiedResult = new UndiversifiedResult();
            IList<UndiversifiedRiskCapitalProjectCollection> undiversifiedRiskCapitalProjectCollection = new List<UndiversifiedRiskCapitalProjectCollection>();

            if (projects.Count > 0)
            {
                foreach (var item in projects)
                {
                    IList<UndiversifiedYearCollection> undiversifiedYearCollection = new List<UndiversifiedYearCollection>();

                    var riskMatrixProject = _riskMatrixProjectRepository.GetByScenarioIdProjectId(scenarioDefault.Id, item.ProjectId);
                    var project = item.Project;
                    var stageTahunRiskMatrix = _stageTahunRiskMatrixRepository.GetByRiskMatrixProjectId(riskMatrixProject.Id).ToList();

                    if (stageTahunRiskMatrix.Count > 0)
                    {
                        foreach (var dataStage in stageTahunRiskMatrix)
                        {
                            UndiversifiedYearCollection undiversifiedYear = new UndiversifiedYearCollection();
                            undiversifiedYear.Year = dataStage.Tahun;

                            IList<YearValue> yearValues = new List<YearValue>();

                            var stageTahunDetail = _stageTahunRiskMatrixDetailRepository.GetByStageTahunRiskMatrixId(dataStage.Id).ToList();
                            if (stageTahunDetail.Count > 0)
                            {
                                foreach (var detailItem in stageTahunDetail)
                                {
                                    decimal? val = 0;
                                    var likehoodFiltered = likelihoodDetail.Where(x => x.Id == detailItem.LikehoodDetailId).FirstOrDefault();
                                    if (likehoodFiltered != null)
                                    {
                                        var nilaiAverage = likehoodFiltered.Average / 100;
                                        var nilaiExpose = detailItem.NilaiExpose;
                                        val = nilaiExpose * nilaiAverage;
                                    }

                                    YearValue year = new YearValue();
                                    year.ScenarioId = scenarioDefault.Id;
                                    year.RiskRegistrasiId = detailItem.RiskRegistrasiId;
                                    year.LikehoodId = scenarioDefault.LikehoodId;
                                    year.ValueUndiversified = val;

                                    yearValues.Add(year);
                                }
                            }

                            undiversifiedYear.YearValue = yearValues.ToArray();
                            undiversifiedYearCollection.Add(undiversifiedYear);
                        }

                        UndiversifiedRiskCapitalProjectCollection undiversifiedRiskCapitalProject = new UndiversifiedRiskCapitalProjectCollection();
                        undiversifiedRiskCapitalProject.ProjectId = project.Id;
                        undiversifiedRiskCapitalProject.NamaProject = project.NamaProject;
                        undiversifiedRiskCapitalProject.SektorId = project.SektorId;
                        undiversifiedRiskCapitalProject.NamaSektor = project.Sektor.NamaSektor;
                        undiversifiedRiskCapitalProject.UndiversifiedYearCollection = undiversifiedYearCollection.ToArray();
                        undiversifiedRiskCapitalProject.RiskRegistrasi = riskRegistrasi.ToArray();

                        undiversifiedRiskCapitalProjectCollection.Add(undiversifiedRiskCapitalProject);
                    }
                    undiversifiedResult.UndiversifiedRiskCapitalProjectCollection = undiversifiedRiskCapitalProjectCollection.ToArray();
                    undiversifiedResultCollection.Add(undiversifiedResult);
                }
            }

            throw new NotImplementedException();
        }
    }
}
