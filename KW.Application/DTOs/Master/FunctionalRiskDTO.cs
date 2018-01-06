using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class FunctionalRiskDTO
    {
        public int Id { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public int? MatrixId { get; set; }
        public string NamaMatrix { get; set; }
        public string NamaFormula { get; set; }

        public int? ColorCommentId { get; set; }
        public string Warna { get; set; }

        public int? ScenarioId { get; set; }
        public string NamaScenario { get; set; }

        public MatrixDTO Matrix { get; set; }
        public ColorCommentDTO ColorComment { get; set; }
        public ScenarioDTO Scenario { get; set; }

        public FunctionalRiskDTO(FunctionalRisk model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.MatrixId = model.MatrixId;
            this.ScenarioId = model.ScenarioId;
            this.ColorCommentId = model.ColorCommentId;
            this.Definisi = model.Definisi;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.Matrix != null)
            {
                MatrixDTO matrixDTO = MatrixDTO.From(model.Matrix);
                this.Matrix = matrixDTO;
                this.MatrixId = matrixDTO.Id;
                this.NamaMatrix = matrixDTO.NamaMatrix;
                this.NamaFormula = matrixDTO.NamaFormula;
            }

            if (model.ColorComment != null)
            {
                ColorCommentDTO colorCommentDTO = ColorCommentDTO.From(model.ColorComment);
                this.ColorComment = colorCommentDTO;
                this.ColorCommentId = colorCommentDTO.Id;
                this.Warna = colorCommentDTO.Warna;
            }

            if (model.Scenario != null)
            {
                ScenarioDTO scenarioDTO = ScenarioDTO.From(model.Scenario);
                this.Scenario = scenarioDTO;
                this.ScenarioId = scenarioDTO.Id;
                this.NamaScenario = scenarioDTO.NamaScenario;
            }
        }
        
        public static FunctionalRiskDTO From(FunctionalRisk model)
        {
            return new FunctionalRiskDTO(model);
        }

        public static IList<FunctionalRiskDTO> From(IList<FunctionalRisk> collection)
        {
            IList<FunctionalRiskDTO> colls = new List<FunctionalRiskDTO>();
            foreach (var item in collection)
            {
                colls.Add(new FunctionalRiskDTO(item));
            }
            return colls;
        }
    }
}
