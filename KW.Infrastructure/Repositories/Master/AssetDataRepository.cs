using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class AssetDataRepository : IAssetDataRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public AssetDataRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public AssetData Get(int id)
        {
            return _databaseContext.AssetDatas.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<AssetData> GetAll()
        {
            //return _databaseContext.AssetDatas.AsQueryable();
            return _databaseContext.AssetDatas.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(AssetData model)
        {
            _databaseContext.AssetDatas.Add(model);
        }

        public bool IsExist(int id, string assetClass)
        {
            var results = _databaseContext.AssetDatas.Where(x => x.AssetClass.ToLower() == assetClass.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string assetClass)
        {
            var results = _databaseContext.AssetDatas.Where(x => x.AssetClass.ToLower() == assetClass.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(AssetData model)
        {
            _databaseContext.AssetDatas.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
