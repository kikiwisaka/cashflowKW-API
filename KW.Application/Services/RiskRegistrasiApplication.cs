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
    public class RiskRegistrasiService : IRiskRegistrasiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;
        private readonly IProjectRiskStatusRepository _projectRiskStatusRepository;

        public RiskRegistrasiService(IUnitOfWork uow, IRiskRegistrasiRepository riskRegistrasiRepository, IProjectRiskStatusRepository projectRiskStatusRepository)
        {
            _unitOfWork = uow;
            _riskRegistrasiRepository = riskRegistrasiRepository;
            _projectRiskStatusRepository = projectRiskStatusRepository;
        }

        #region Query
        public IEnumerable<RiskRegistrasi> GetAll()
        {
            return _riskRegistrasiRepository.GetAll();
        }

        public RiskRegistrasi Get(int id)
        {
            return _riskRegistrasiRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string kodeMRisk, string namaCategoryRisk, string definisi)
        {
            if (_riskRegistrasiRepository.IsExist(id, namaCategoryRisk))
            {
                throw new ApplicationException(string.Format("Kode Risk {0} already exist.", namaCategoryRisk));
            }
        }

        public void isExistOnAdding(string kodeMRisk)
        {
            if (_riskRegistrasiRepository.IsExist(kodeMRisk))
            {
                throw new ApplicationException(string.Format("Kode Risk {0} already exist.", kodeMRisk));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(RiskRegistrasiParam param)
        {
            int id;
            Validate.NotNull(param.NamaCategoryRisk, "Nama Categori Risk wajib diisi.");

            isExistOnAdding(param.KodeMRisk);
            using (_unitOfWork)
            {
                RiskRegistrasi model = new RiskRegistrasi(param.KodeMRisk, param.NamaCategoryRisk, param.Definisi, param.CreateBy, param.CreateDate);
                _riskRegistrasiRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, RiskRegistrasiParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama RiskRegistrasi tidak ditemukan.");

            IsExistOnEditing(id, param.KodeMRisk, param.NamaCategoryRisk, param.Definisi);
            using (_unitOfWork)
            {
                model.Update(param.KodeMRisk, param.NamaCategoryRisk, param.Definisi, param.UpdateBy, param.UpdateDate);
                _riskRegistrasiRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        
        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Category Risk tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _riskRegistrasiRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation

        private void UpdateProjectRiskStatus(RiskRegistrasi model)
        {
            var currentProjectRiskStatusList = _projectRiskStatusRepository.GetAll();
            if(currentProjectRiskStatusList != null)
            {
                var currentProject = currentProjectRiskStatusList.GroupBy(x => x.ProjectId).ToList();
                if(currentProject.Count > 0)
                {
                    foreach (var item in currentProject)
                    {
                    }
                }
            }
        }
    }
}
