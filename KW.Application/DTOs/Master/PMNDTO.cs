using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class PMNDTO
    {
        public int Id { get; set; }
        public int PMNToModalDasarCap { get; set; }
        public decimal RecourseDelay { get; set; }
        public decimal DelayYears { get; set; }
        public decimal OpexGrowth { get; set; }
        public decimal Opex { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }
        public decimal ValuePMNToModalDasarCap { get; set; }

        public PMNDTO(PMN model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.PMNToModalDasarCap = model.PMNToModalDasarCap;
            this.RecourseDelay = model.RecourseDelay;
            this.DelayYears = model.DelayYears;
            this.OpexGrowth = model.OpexGrowth;
            this.Opex = model.Opex;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
            this.ValuePMNToModalDasarCap = model.ValuePMNToModalDasarCap;
        }

        public static PMNDTO From(PMN model)
        {
            return new PMNDTO(model);
        }

        public static IList<PMNDTO> From(IList<PMN> collection)
        {
            IList<PMNDTO> colls = new List<PMNDTO>();
            foreach (var item in collection)
            {
                colls.Add(new PMNDTO(item));
            }
            return colls;
        }
    }
}
