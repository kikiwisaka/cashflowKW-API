using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IStageTahunRiskMatrixRepository
    {
        StageTahunRiskMatrix Get(int id);
        IEnumerable<StageTahunRiskMatrix> GetByRiskMatrixProjectId(int riskMatrixProjectId);
        StageTahunRiskMatrix GetByRiskMatrixProjectIdYear(int riskMatrixProjectId, int year);
        IEnumerable<StageTahunRiskMatrix> GetAll();
        void Insert(StageTahunRiskMatrix model);
        void Update(StageTahunRiskMatrix model);
        void Delete(int riskMatrixProjectId);
        //void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, int riskMatrixProjectId);
        bool IsExist(int riskMatrixProjectId);
    }
}
