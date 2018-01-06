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
    public class StageTahunRiskMatrixDetailService : IStageTahunRiskMatrixDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStageTahunRiskMatrixRepository _stageTahunRiskMatrixRepository;
        private readonly IStageTahunRiskMatrixDetailRepository _stageTahunRiskMatrixDetailRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;
        private readonly ILikehoodRepository _likehoodRepository;
        private readonly ILikehoodDetailRepository _likehoodDetailRepository;

        public StageTahunRiskMatrixDetailService(IUnitOfWork unitOfWork, IStageTahunRiskMatrixRepository stageTahunRiskMatrixRepository, IStageTahunRiskMatrixDetailRepository stageTahunRiskMatrixDetailRepository,
            IRiskRegistrasiRepository riskRegistrasiRepository, ILikehoodRepository likehoodRepository, ILikehoodDetailRepository likehoodDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _stageTahunRiskMatrixRepository = stageTahunRiskMatrixRepository;
            _stageTahunRiskMatrixDetailRepository = stageTahunRiskMatrixDetailRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
            _likehoodRepository = likehoodRepository;
            _likehoodDetailRepository = likehoodDetailRepository;
        }

        public void IsExist(int riskMatrixProjectId)
        {
            if (_stageTahunRiskMatrixDetailRepository.IsExist(riskMatrixProjectId))
            {
                throw new ApplicationException(string.Format("Risk Matriks project sudah ada."));
            }
        }

        public IEnumerable<StageTahunRiskMatrixDetail> GetByStageTahunRiskMatrixId(int stageTahunRiskMatrixId)
        {
            return _stageTahunRiskMatrixDetailRepository.GetByStageTahunRiskMatrixId(stageTahunRiskMatrixId);
        }

        public RiskMatrixCollectionParameter GetByRiskMatrixProjectId(int riskMatrixProjectId)
        {
            RiskMatrixCollectionParameter collection = new RiskMatrixCollectionParameter();
            IList<RiskMatrixCollection> matrixCollection = new List<RiskMatrixCollection>();

            var data = _stageTahunRiskMatrixDetailRepository.GetByRiskMatrixProjectId(riskMatrixProjectId);
            if (data.Count() > 0)
            {
                var cred = data.ToList();
                var years = data.GroupBy(y => y.StageTahunRiskMatrixId).ToList();

                collection.CreateBy = cred[0].CreateBy;
                collection.CreateDate = cred[0].CreateDate;
                if (years.Count > 0)
                {
                    for (int i = 0; i < years.Count; i++)
                    {
                        RiskMatrixCollection matrixCollectionByYear = new RiskMatrixCollection();
                        IList<RiskMatrixValue> matrixValues = new List<RiskMatrixValue>();

                        var yearId = years[i].Key;
                        matrixCollectionByYear.StageTahunRiskMatrixId = yearId;

                        for (int r = 0; r < years[i].Count(); r++)
                        {
                            RiskMatrixValue matrixValue = new RiskMatrixValue(); //temporary

                            List<string> values = new List<string>();

                            var matVal = years[i].ToList();
                            matrixValue.RiskRegistrasiId = matVal[r].RiskRegistrasiId;

                            var exposure = matVal[r].NilaiExpose;
                            var likehoodDetail = matVal[r].LikehoodDetailId;
                            values.Add(exposure.ToString());
                            values.Add(likehoodDetail.ToString());
                            string[] valueExLi = values.ToArray();

                            matrixValue.Values = valueExLi;
                            matrixValues.Add(matrixValue);
                        }
                        matrixCollectionByYear.RiskMatrixValue = matrixValues;
                        matrixCollection.Add(matrixCollectionByYear);
                    }
                    collection.RiskMatrixCollection = matrixCollection.ToArray();
                }
            }
            return collection;
        }

        public int Add(RiskMatrixCollectionParameter param)
        {
            int id = 0;
            var riskMatrixProject = _stageTahunRiskMatrixRepository.Get(param.RiskMatrixCollection[0].StageTahunRiskMatrixId);
            Validate.NotNull(riskMatrixProject, "Risk Matrix Project ID tidak boleh kosong.");

            IsExist(riskMatrixProject.RiskMatrixProjectId);
            using (_unitOfWork)
            {
                for (int i = 0; i < param.RiskMatrixCollection.Length; i++)
                {
                    var stageTahunRiskMatrix = _stageTahunRiskMatrixRepository.Get(param.RiskMatrixCollection[i].StageTahunRiskMatrixId);
                    Validate.NotNull(stageTahunRiskMatrix, "Stage Tahun Risk Matrix tidak boleh kosong.");

                    foreach (var item in param.RiskMatrixCollection[i].RiskMatrixValue)
                    {
                        var riskRegistrasi = _riskRegistrasiRepository.Get(item.RiskRegistrasiId);
                        Validate.NotNull(riskRegistrasi, "Risk Regsitrasi tidak boleh kosong.");

                        int likehoodDetailId = 0;
                        if (item.Values[1] != "0")
                        {
                            var likehoodDetail = _likehoodDetailRepository.Get(Int32.Parse(item.Values[1]));
                            Validate.NotNull(likehoodDetail, "Definisi Likelihood tidak boleh kosong.");

                            likehoodDetailId = likehoodDetail.Id;
                        }

                        var exposure = item.Values[0];
                        decimal nilaiExposure = decimal.Parse(exposure);
                        StageTahunRiskMatrixDetail model = new StageTahunRiskMatrixDetail(stageTahunRiskMatrix, riskRegistrasi, stageTahunRiskMatrix.RiskMatrixProject.Id, likehoodDetailId, nilaiExposure, param.CreateBy, param.CreateDate);
                        _stageTahunRiskMatrixDetailRepository.Insert(model);
                        id = model.Id;
                    }
                    _unitOfWork.Commit();
                }
            }
            return id;
        }

        public int Update(int id, RiskMatrixCollectionParameter param)
        {
            int modelId = 0;

            using (_unitOfWork)
            {
                //delete current data
                RemoveCurrentData(param);

                //insert new data
                for (int i = 0; i < param.RiskMatrixCollection.Length; i++)
                {
                    var stageTahunRiskMatrix = _stageTahunRiskMatrixRepository.Get(param.RiskMatrixCollection[i].StageTahunRiskMatrixId);
                    Validate.NotNull(stageTahunRiskMatrix, "Stage Tahun Risk Matrix tidak boleh kosong.");

                    foreach (var item in param.RiskMatrixCollection[i].RiskMatrixValue)
                    {
                        var riskRegistrasi = _riskRegistrasiRepository.Get(item.RiskRegistrasiId);
                        Validate.NotNull(riskRegistrasi, "Risk Regsitrasi tidak boleh kosong.");

                        int likehoodDetailId = 0;
                        if (item.Values[1] != "0")
                        {
                            var likehoodDetail = _likehoodDetailRepository.Get(Int32.Parse(item.Values[1]));
                            Validate.NotNull(likehoodDetail, "Definisi Likelihood tidak boleh kosong.");

                            likehoodDetailId = likehoodDetail.Id;
                        }

                        var exposure = item.Values[0];
                        decimal nilaiExposure = decimal.Parse(exposure);
                        StageTahunRiskMatrixDetail model = new StageTahunRiskMatrixDetail(stageTahunRiskMatrix, riskRegistrasi, stageTahunRiskMatrix.RiskMatrixProject.Id, likehoodDetailId, nilaiExposure, param.CreateBy, param.CreateDate);
                        _stageTahunRiskMatrixDetailRepository.Insert(model);
                        modelId = model.Id;
                    }
                    _unitOfWork.Commit();
                }
            }
            return modelId;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }

        private void RemoveCurrentData(RiskMatrixCollectionParameter param)
        {
            var stageTahunRiskMatrix = _stageTahunRiskMatrixRepository.Get(param.RiskMatrixCollection[0].StageTahunRiskMatrixId);
            _stageTahunRiskMatrixDetailRepository.Delete(stageTahunRiskMatrix.RiskMatrixProjectId);
        }
    }
}
