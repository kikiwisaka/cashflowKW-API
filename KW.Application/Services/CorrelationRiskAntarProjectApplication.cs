using KW.Common.Validation;
using System;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Collections.Generic;
using System.Linq;

namespace KW.Application
{
    public class CorrelationRiskAntarProjectService : ICorrelationRiskAntarProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelationRiskAntarProjectRepository _correlationRiskAntarProjectRepository;
        private readonly ICorrelationRiskAntarSektorRepository _correlationRiskAntarSektorRepository;
        private readonly ICorrelationMatrixRepository _correlationMatrixRepository;
        private readonly IProjectRepository _projectRepository;

        public CorrelationRiskAntarProjectService(IUnitOfWork unitOfWork, ICorrelationRiskAntarProjectRepository correlationRiskAntarProjectRepository, ICorrelationRiskAntarSektorRepository correlationRiskAntarSektorRepository,
            ICorrelationMatrixRepository correlationMatrixRepository, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork;
            _correlationRiskAntarProjectRepository = correlationRiskAntarProjectRepository;
            _correlationRiskAntarSektorRepository = correlationRiskAntarSektorRepository;
            _correlationMatrixRepository = correlationMatrixRepository;
            _projectRepository = projectRepository;
        }

        public CorrelationRiskAntarProjectParam GetByCorrelationRiskAntarSektorId(int correlationRiskAntarSektorId)
        {
            CorrelationRiskAntarProjectParam param = new CorrelationRiskAntarProjectParam();
            IList<CorrelationRiskAntarProjectColletion> dataCollection = new List<CorrelationRiskAntarProjectColletion>();

            var data = _correlationRiskAntarProjectRepository.GetByCorrelationRiskAntarSektorId(correlationRiskAntarSektorId).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    CorrelationRiskAntarProjectColletion detail = new CorrelationRiskAntarProjectColletion();
                    IList<ProjectCorrelationMatrixValues> detailList = new List<ProjectCorrelationMatrixValues>();

                    var dataDetail = data.Where(x => x.ProjectIdRow == item.ProjectIdRow).ToList();
                    foreach (var itemDetail in dataDetail)
                    {
                        ProjectCorrelationMatrixValues detailValue = new ProjectCorrelationMatrixValues();
                        detailValue.ProjectIdRow = item.ProjectIdRow;
                        detailValue.ProjectIdCol = item.ProjectIdCol;
                        detailValue.CorrelationMatrixId = item.CorrelationMatrixId;

                        detailList.Add(detailValue);
                    }

                    detail.ProjectId = item.ProjectIdRow;
                    detail.ProjectCorrelationMatrixValues = detailList.ToArray();

                    dataCollection.Add(detail);
                }
                param.CorrelationRiskAntarSektorId = data[0].CorrelationRiskAntarSektorId;
                param.CorrelationRiskAntarProjectColletion = dataCollection.ToArray();
                param.CreateBy = data[0].CreateBy;
                param.CreateDate = data[0].CreateDate;
                param.UpdateBy = data[0].UpdateBy;
                param.UpdateDate = data[0].UpdateDate;
                param.IsDelete = data[0].IsDelete;
            }
            return param;
        }

        public int Add(CorrelationRiskAntarProjectParam param)
        {
            int id = 0;

            CorrelationRiskAntarSektor correlationRiskAntarSektor = _correlationRiskAntarSektorRepository.Get(param.CorrelationRiskAntarSektorId);
            Validate.NotNull(correlationRiskAntarSektor, "Correlation Risk Antar Sektor tidak ditemukan.");

            using (_unitOfWork)
            {
                foreach (var itemMain in param.CorrelationRiskAntarProjectColletion)
                {
                    foreach (var itemDetail in itemMain.ProjectCorrelationMatrixValues)
                    {
                        CorrelationMatrix correlationMatrix = _correlationMatrixRepository.Get(itemDetail.CorrelationMatrixId);
                        Validate.NotNull(correlationMatrix, "Correlation Matrix tidak ditemukan.");

                        Project projectIdRow = _projectRepository.Get(itemDetail.ProjectIdRow);
                        Validate.NotNull(projectIdRow, "Project (row) tidak ditemukan.");

                        Project projectIdCol = _projectRepository.Get(itemDetail.ProjectIdCol);
                        Validate.NotNull(projectIdCol, "Project (col) tidak ditemukan.");

                        var data = _correlationRiskAntarProjectRepository.isExistOnAdding(correlationRiskAntarSektor.Id, projectIdRow.Id, projectIdCol.Id);
                        if(data == null)
                        {
                            CorrelationRiskAntarProject model = new CorrelationRiskAntarProject(correlationRiskAntarSektor, projectIdRow.Id, projectIdCol.Id, correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlationRiskAntarProjectRepository.Insert(model);
                        }
                        else
                        {
                            data.Update(correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlationRiskAntarProjectRepository.Update(data);
                        }
                    }
                }
                _unitOfWork.Commit();
            }

            return id;
        }
    }
}
