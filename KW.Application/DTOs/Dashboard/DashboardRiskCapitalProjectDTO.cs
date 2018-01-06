using System.Collections.Generic;
using KW.Domain;

namespace KW.Application.DTO
{
    public class DashboardRiskCapitalProjectDTO
    {
        public int Year { get; set; }
        public int Total { get; set; }
        public string ProjectName { get; set; }

        public DashboardRiskCapitalProjectDTO(DashboardRiskCapitalProject data)
        {
            if (data == null) return;

            this.ProjectName = data.ProjectName;
            this.Year = data.Year;
            this.Total = data.Total;
        }
        
        public static IList<DashboardRiskCapitalProjectDTO> From(IList<DashboardRiskCapitalProject> collection)
        {
            IList<DashboardRiskCapitalProjectDTO> dtos = new List<DashboardRiskCapitalProjectDTO>();
            foreach (var item in collection)
            {
                dtos.Add(new DashboardRiskCapitalProjectDTO(item));
            }
            return dtos;
        }
    }
}
