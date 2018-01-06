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
    public class CorrelatedSektorDetailService : ICorrelatedSektorDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelatedSektorDetailRepository _correlatedSektorDetailRepository;
        private readonly ICorrelatedSektorRepository _correlatedSektorRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;
        private readonly ICorrelationMatrixRepository _correlationMatrixRepository;

        public CorrelatedSektorDetailService(IUnitOfWork unitOfWork, ICorrelatedSektorDetailRepository correlatedSektorDetailRepository, ICorrelatedSektorRepository correlatedSektorRepository, 
            IRiskRegistrasiRepository riskRegistrasiRepository, ICorrelationMatrixRepository correlationMatrixRepository)
        {
            _unitOfWork = unitOfWork;
            _correlatedSektorDetailRepository = correlatedSektorDetailRepository;
            _correlatedSektorRepository = correlatedSektorRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
            _correlationMatrixRepository = correlationMatrixRepository;
        }

        public CorrelatedSektorDetailCollectionParam GetByCorrelatedSektorId(int correlatedSektorId)
        {
            CorrelatedSektorDetailCollectionParam param = new CorrelatedSektorDetailCollectionParam();
            IList<CorrelatedSektorDetailCollection> dataCollection = new List<CorrelatedSektorDetailCollection>();

            var data = _correlatedSektorDetailRepository.GetByCorrelatedSektorId(correlatedSektorId).ToList();
            if(data.Count > 0)
            {
                var riskRegistrasi = _riskRegistrasiRepository.GetAll().ToList();

                foreach (var itemRisk in riskRegistrasi)
                {
                    CorrelatedSektorDetailCollection detail = new CorrelatedSektorDetailCollection();
                    IList<RiskRegistrasiValues> detailList = new List<RiskRegistrasiValues>();

                    var dataDetail = data.Where(x => x.RiskRegistrasiIdRow == itemRisk.Id).ToList();

                    foreach (var item in dataDetail)
                    {
                        RiskRegistrasiValues detailValue = new RiskRegistrasiValues();
                        detailValue.RiskRegistrasiIdRow = item.RiskRegistrasiIdRow;
                        detailValue.RiskRegistrasiIdCol = item.RiskRegistrasiIdCol;
                        detailValue.CorrelationMatrixId = item.CorrelationMatrixId;

                        detailList.Add(detailValue);
                    }
                    detail.RiskRegistrasiId = itemRisk.Id;
                    detail.RiskRegistrasiValues = detailList.ToArray();

                    dataCollection.Add(detail);
                }

                param.CorrelatedSektorId = data[0].CorrelatedSektorId;
                param.CorrelatedSektorDetailCollection = dataCollection.ToArray();
                param.CreateBy = data[0].CreateBy;
                param.CreateDate = data[0].CreateDate;
                param.UpdateBy = data[0].UpdateBy;
                param.UpdateDate = data[0].UpdateDate;
                param.IsDelete = data[0].IsDelete;
            }

            return param;
        }

        public void IsExistOnAdding(int correlataedSektor, int riskRegistrasiIdRow, int riskRegistrasiIdCol)
        {

        }

        public int Add(CorrelatedSektorDetailCollectionParam param)
        {
            int id = 0;
            CorrelatedSektor correlatedSektor = _correlatedSektorRepository.Get(param.CorrelatedSektorId);
            Validate.NotNull(correlatedSektor, "Correlated Sektor tidak ditemukan.");

            using (_unitOfWork)
            {
                foreach (var riskItem in param.CorrelatedSektorDetailCollection)
                {
                    RiskRegistrasi riskRegistrasi = _riskRegistrasiRepository.Get(riskItem.RiskRegistrasiId);
                    Validate.NotNull(riskRegistrasi, "Risk Kategori tidak ditemukan.");

                    foreach (var item in riskItem.RiskRegistrasiValues)
                    {
                        CorrelationMatrix correlationMatrix = _correlationMatrixRepository.Get(item.CorrelationMatrixId);
                        Validate.NotNull(correlationMatrix, "Correlation Matrix tidak ditemukan.");

                        RiskRegistrasi riskRegistrasiRow = _riskRegistrasiRepository.Get(item.RiskRegistrasiIdRow);
                        Validate.NotNull(riskRegistrasiRow, "Risk Kategori (row) tidak ditemukan.");

                        RiskRegistrasi riskRegistrasiCol = _riskRegistrasiRepository.Get(item.RiskRegistrasiIdCol);
                        Validate.NotNull(riskRegistrasiCol, "Risk Kategori (col) tidak ditemukan.");

                        var isExist = _correlatedSektorDetailRepository.IsExisitOnAdding(correlatedSektor.Id, riskRegistrasiRow.Id, riskRegistrasiCol.Id);

                        if (isExist == null)
                        {
                            CorrelatedSektorDetail model = new CorrelatedSektorDetail(correlatedSektor, riskRegistrasiRow.Id, riskRegistrasiCol.Id, correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlatedSektorDetailRepository.Insert(model);
                        }
                        else
                        {
                            isExist.Update(correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlatedSektorDetailRepository.Update(isExist);
                        }
                    }
                }
                _unitOfWork.Commit();
                //id = model.Id;
            }
            return id;
        }
    }
}
