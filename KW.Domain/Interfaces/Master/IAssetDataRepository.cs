using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IAssetDataRepository
    {
        AssetData Get(int id);
        IEnumerable<AssetData> GetAll();
        void Insert(AssetData model);
        void Update(AssetData model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string assetClass);
        bool IsExist(string assetClass);
    }
}
