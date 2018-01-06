using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class FunctionalRisk : Entity
    {
        public string Definisi { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int? MatrixId { get; private set; }
        public int? ScenarioId { get; private set; }
        public int? ColorCommentId { get; private set; }

        //Navigation
        public virtual Matrix Matrix { get; set; }
        public virtual ColorComment ColorComment { get; set; }
        public virtual Scenario Scenario { get; set; }


        public FunctionalRisk()
        {

        }

        public FunctionalRisk(Matrix Matrix, ColorComment ColorComment, Scenario Scenario, string definisi, int? createBy, DateTime? createDate)
        {
            this.MatrixId = Matrix.Id;
            this.ColorCommentId = ColorComment.Id;
            this.ScenarioId = Scenario.Id;
            this.Definisi = definisi;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Matrix Matrix, ColorComment ColorComment, Scenario Scenario, string definisi, int? updateBy, DateTime? updateDate)
        {
            this.MatrixId = Matrix.Id;
            this.ColorCommentId = ColorComment.Id;
            this.ScenarioId = Scenario.Id;
            this.Definisi = definisi;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
