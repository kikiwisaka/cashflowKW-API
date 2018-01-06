using KW.Domain;

namespace KW.Application.Params
{
    public class UndiversifiedResult
    {
        public UndiversifiedRiskCapitalProjectCollection[] UndiversifiedRiskCapitalProjectCollection { get; set; }
        public UndiversifiedResult() { }
    }
    public class UndiversifiedRiskCapitalProjectCollection
    {
        public int ProjectId { get; set; }
        public string NamaProject { get; set; }
        public int SektorId { get; set; }
        public string NamaSektor { get; set; }
        public UndiversifiedYearCollection[] UndiversifiedYearCollection { get; set; }
        public RiskRegistrasi[] RiskRegistrasi { get; set; }

        public UndiversifiedRiskCapitalProjectCollection() { }
    }

    public class UndiversifiedYearCollection
    {
        public int Year { get; set; }
        public YearValue[] YearValue { get; set; }

        public UndiversifiedYearCollection() { }
    }

    public class YearValue
    {
        public int ScenarioId { get; set; }
        public int RiskRegistrasiId { get; set; }
        public int LikehoodId { get; set; }
        public decimal? ValueUndiversified { get; set; }

        public YearValue() { }
    }
}
