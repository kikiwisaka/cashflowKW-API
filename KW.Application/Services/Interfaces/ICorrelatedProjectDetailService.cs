using KW.Application.Params;

namespace KW.Application
{
    public interface ICorrelatedProjectDetailService
    {
        CorrelatedProjectDetailCollectionParam GetByCorrelatedProjectId(int correlatedProjectId);
        int Add(CorrelatedProjectDetailCollectionParam param); 
    }
}
