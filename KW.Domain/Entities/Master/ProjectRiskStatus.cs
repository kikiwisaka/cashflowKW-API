using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class ProjectRiskStatus : Entity
    {
        public int ProjectId { get; private set; }
        public int RiskRegistrasiId { get; private set; }
        public string KodeMRisk { get; private set; }
        public string NamaCategoryRisk { get; private set; }
        public string Definisi { get; private set; }
        public bool IsProjectUsed { get; private set; }

        public ProjectRiskStatus() { }

        public ProjectRiskStatus(Project project, int riskRegistrasiId, string kodeMRisk, string namaCategoryRisk, string definisi, bool isProjectUsed)
        {
            this.ProjectId = project.Id;
            this.RiskRegistrasiId = riskRegistrasiId;
            this.KodeMRisk = kodeMRisk;
            this.NamaCategoryRisk = namaCategoryRisk;
            this.Definisi = definisi;
            this.IsProjectUsed = isProjectUsed;
        }

        public virtual void Update(Project project, int riskRegistrasiId, string kodeMRisk, string namaCategoryRisk, string definisi, bool isProjectUsed)
        {
            this.ProjectId = project.Id;
            this.RiskRegistrasiId = riskRegistrasiId;
            this.KodeMRisk = kodeMRisk;
            this.NamaCategoryRisk = namaCategoryRisk;
            this.Definisi = definisi;
            this.IsProjectUsed = isProjectUsed;
        }
    }
}
