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
    public class AssetDataService : IAssetDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssetDataRepository _assetDataRepository;

        public AssetDataService(IUnitOfWork uow, IAssetDataRepository assetDataRepository)
        {
            _unitOfWork = uow;
            _assetDataRepository = assetDataRepository;
        }

        #region Query
        public IEnumerable<AssetData> GetAll()
        {
            return _assetDataRepository.GetAll();
        }

        public AssetData Get(int id)
        {
            return _assetDataRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string assetClass, int termAwal, int termAkhir, decimal assumentReturn, int outstandingStartYears, int outstandingEndYears, decimal assetValue, decimal porpotion, decimal assumedReturnPercentage, decimal assumedReturn, bool? status)
        {
            if (_assetDataRepository.IsExist(id, assetClass))
            {
                throw new ApplicationException(string.Format("Asset Class {0} sudah ada.", assetClass));
            }
        }

        public void isExistOnAdding(string assetClass)
        {
            if (_assetDataRepository.IsExist(assetClass))
            {
                throw new ApplicationException(string.Format("Asset Class {0} sudah ada.", assetClass));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(AssetDataParam param)
        {
            int id;
            Validate.NotNull(param.AssetClass, "Asset Class wajib diisi.");

            isExistOnAdding(param.AssetClass);
            using (_unitOfWork)
            {
                AssetData model = new AssetData(param.AssetClass, param.TermAwal, param.TermAkhir, param.AssumentReturn, param.OutstandingStartYears, param.OutstandingEndYears, param.AssetValue, param.Porpotion, param.AssumedReturnPercentage, param.AssumedReturn, param.CreateBy, param.CreateDate, param.Status);
                _assetDataRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, AssetDataParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Asset Class tidak ditemukan.");

            IsExistOnEditing(id, param.AssetClass, param.TermAwal, param.TermAkhir, param.AssumentReturn, param.OutstandingStartYears, param.OutstandingEndYears, param.AssetValue, param.Porpotion, param.AssumedReturnPercentage, param.AssumedReturn, param.Status);
            using (_unitOfWork)
            {
                model.Update(param.AssetClass, param.TermAwal, param.TermAkhir, param.AssumentReturn, param.OutstandingStartYears, param.OutstandingEndYears, param.AssetValue, param.Porpotion, param.AssumedReturnPercentage, param.AssumedReturn, param.UpdateBy, param.UpdateDate, param.Status);
                _assetDataRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Asset Class tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _assetDataRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
