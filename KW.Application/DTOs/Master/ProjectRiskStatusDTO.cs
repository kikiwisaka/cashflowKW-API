using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class ProjectRiskStatusDTO
    {
        public int Id { get; set; }
        public int RiskRegistrasiId { get; set; }
        public string KodeMRisk { get; set; }
        public string NamaCategoryRisk { get; set; }
        public string Definisi { get; set; }
        public bool IsProjectUsed{ get; set; }

        public ProjectRiskStatusDTO(ProjectRiskStatus model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.RiskRegistrasiId = model.RiskRegistrasiId;
            this.KodeMRisk = model.KodeMRisk;
            this.NamaCategoryRisk = model.NamaCategoryRisk;
            this.Definisi = model.Definisi;
            this.IsProjectUsed = model.IsProjectUsed;
        }

        public static ProjectRiskStatusDTO From(ProjectRiskStatus model)
        {
            return new ProjectRiskStatusDTO(model);
        }

        public static IList<ProjectRiskStatusDTO> From(IList<ProjectRiskStatus> collection)
        {
            IList<ProjectRiskStatusDTO> colls = new List<ProjectRiskStatusDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ProjectRiskStatusDTO(item));
            }
            return colls;
        }
    }
}
