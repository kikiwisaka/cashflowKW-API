using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelatedProjectDetailRepository
    {
        CorrelatedProjectDetail Get(int id);
        IEnumerable<CorrelatedProjectDetail> GetByCorrelatedProjectId(int correlatedProjectId);
        void Insert(CorrelatedProjectDetail model);
        CorrelatedProjectDetail IsExisitOnAdding(int correlataedProjectId, int projectIdRow, int projectIdCol);
        void Update(CorrelatedProjectDetail model);
    }
}
