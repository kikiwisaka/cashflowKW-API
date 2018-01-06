using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelatedSektorDetailRepository
    {
        CorrelatedSektorDetail Get(int id);
        IEnumerable<CorrelatedSektorDetail> GetByCorrelatedSektorId(int correlatedSektorId);
        void Insert(CorrelatedSektorDetail model);
        void Insert(IList<CorrelatedSektorDetail> collection);
        CorrelatedSektorDetail IsExisitOnAdding(int correlataedSektor, int riskRegistrasiIdRow, int riskRegistrasiIdCol);
        void Update(CorrelatedSektorDetail model);

    }
}
