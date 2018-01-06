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
    public class CorrelatedProjectDetailService : ICorrelatedProjectDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelatedProjectDetailRepository _correlatedProjectDetailRepository;
        private readonly ICorrelatedProjectRepository _correlatedProjectRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICorrelationMatrixRepository _correlationMatrixRepository;

        public CorrelatedProjectDetailService(IUnitOfWork unitOfWork, ICorrelatedProjectDetailRepository correlatedProjectDetailRepository, ICorrelatedProjectRepository correlatedProjectRepository, IProjectRepository projectRepository, ICorrelationMatrixRepository correlationMatrixRepository)
        {
            _unitOfWork = unitOfWork;
            _correlatedProjectDetailRepository = correlatedProjectDetailRepository;
            _correlatedProjectRepository = correlatedProjectRepository;
            _projectRepository = projectRepository;
            _correlationMatrixRepository = correlationMatrixRepository;
        }

        public int Add(CorrelatedProjectDetailCollectionParam param)
        {

            int id = 0;

            CorrelatedProject correlatedProject = _correlatedProjectRepository.Get(param.CorrelatedProjectId);
            Validate.NotNull(correlatedProject, "CorrelatedProject tidak ditemukan.");

            using (_unitOfWork)
            {
                foreach (var itemMain in param.CorrelatedProjectDetailCollection)
                {
                    foreach (var itemDetail in itemMain.CorrelatedProjectMatrixValues)
                    {
                        CorrelationMatrix correlationMatrix = _correlationMatrixRepository.Get(itemDetail.CorrelationMatrixId);
                        Validate.NotNull(correlationMatrix, "Correlation Matrix tidak ditemukan.");

                        Project projectIdRow = _projectRepository.Get(itemDetail.ProjectIdRow);
                        Validate.NotNull(projectIdRow, "Project (row) tidak ditemukan.");

                        Project projectIdCol = _projectRepository.Get(itemDetail.ProjectIdCol);
                        Validate.NotNull(projectIdCol, "Project (col) tidak ditemukan.");

                        var data = _correlatedProjectDetailRepository.IsExisitOnAdding(correlatedProject.Id, projectIdRow.Id, projectIdCol.Id);
                        if (data == null)
                        {
                            CorrelatedProjectDetail model = new CorrelatedProjectDetail(correlatedProject, projectIdRow.Id, projectIdCol.Id, correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlatedProjectDetailRepository.Insert(model);
                        }
                        else
                        {
                            data.Update(correlationMatrix, param.CreateBy, param.CreateDate);
                            _correlatedProjectDetailRepository.Update(data);
                        }
                    }
                }
                _unitOfWork.Commit();
            }

            return id;
        }

        public CorrelatedProjectDetailCollectionParam GetByCorrelatedProjectId(int correlatedProjectId)
        {
            CorrelatedProjectDetailCollectionParam param = new CorrelatedProjectDetailCollectionParam();
            IList<CorrelatedProjectDetailCollection> dataCollection = new List<CorrelatedProjectDetailCollection>();

            var data = _correlatedProjectDetailRepository.GetByCorrelatedProjectId(correlatedProjectId).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    CorrelatedProjectDetailCollection detail = new CorrelatedProjectDetailCollection();
                    IList<CorrelatedProjectMatrixValues> detailList = new List<CorrelatedProjectMatrixValues>();

                    var dataDetail = data.Where(x => x.ProjectIdRow == item.ProjectIdRow).ToList();
                    foreach (var itemDetail in dataDetail)
                    {
                        CorrelatedProjectMatrixValues detailValue = new CorrelatedProjectMatrixValues();
                        detailValue.ProjectIdRow = item.ProjectIdRow;
                        detailValue.ProjectIdCol = item.ProjectIdCol;
                        detailValue.CorrelationMatrixId = item.CorrelationMatrixId;

                        detailList.Add(detailValue);
                    }

                    detail.ProjectId = item.ProjectIdRow;
                    detail.CorrelatedProjectMatrixValues = detailList.ToArray();

                    dataCollection.Add(detail);
                }
                param.CorrelatedProjectId = data[0].CorrelatedProjectId;
                param.CorrelatedProjectDetailCollection  = dataCollection.ToArray();
                param.CreateBy = data[0].CreateBy;
                param.CreateDate = data[0].CreateDate;
                param.UpdateBy = data[0].UpdateBy;
                param.UpdateDate = data[0].UpdateDate;
                param.IsDelete = data[0].IsDelete;
            }
            return param;
        }
    }
}
