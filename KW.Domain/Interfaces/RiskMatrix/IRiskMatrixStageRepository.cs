using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IRiskMatrixStageRepository
    {
        RiskMatrixStage Get(int id);
        IEnumerable<RiskMatrixStage> GetAll();
        void Insert(RiskMatrixStage model);
        void Update(RiskMatrixStage model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, int stageId);
        bool IsExist(int stageId);
    }
}
