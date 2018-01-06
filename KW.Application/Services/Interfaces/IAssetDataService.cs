using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IAssetDataService
    {
        IEnumerable<AssetData> GetAll();
        AssetData Get(int id);
        int Add(AssetDataParam param);
        int Update(int id, AssetDataParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
