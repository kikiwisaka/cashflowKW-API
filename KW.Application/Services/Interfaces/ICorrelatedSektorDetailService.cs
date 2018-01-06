using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;


namespace KW.Application
{
    public interface ICorrelatedSektorDetailService
    {
        CorrelatedSektorDetailCollectionParam GetByCorrelatedSektorId(int correlatedSektorId);
        int Add(CorrelatedSektorDetailCollectionParam param);
    }
}
