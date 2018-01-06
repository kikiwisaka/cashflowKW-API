using KW.Application.Params;
using KW.Domain;

namespace KW.Application
{
    public interface ICorrelationRiskAntarProjectService
    {
        CorrelationRiskAntarProjectParam GetByCorrelationRiskAntarSektorId(int correlationRiskAntarSektorId);
        int Add(CorrelationRiskAntarProjectParam param);
    }
}
