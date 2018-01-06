using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class ProjectRiskRegistrasiDTO
    {
        public int Id { get; set; }
        public int ProjectId{ get; set; }
        public RiskRegistrasiDTO RiskRegistrasi{ get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ProjectRiskRegistrasiDTO(ProjectRiskRegistrasi model)
        {
            if (model == null) return;

            this.ProjectId = model.ProjectId;
            this.RiskRegistrasi = RiskRegistrasiDTO.From(model.RiskRegistrasi);
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static ProjectRiskRegistrasiDTO From(ProjectRiskRegistrasi model)
        {
            return new ProjectRiskRegistrasiDTO(model);
        }

        public static IList<ProjectRiskRegistrasiDTO> From(IList<ProjectRiskRegistrasi> collection)
        {
            IList<ProjectRiskRegistrasiDTO> colls = new List<ProjectRiskRegistrasiDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ProjectRiskRegistrasiDTO(item));
            }
            return colls;
        }
    }
}
