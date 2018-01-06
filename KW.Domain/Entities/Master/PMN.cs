using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class PMN : Entity
    {
        public int PMNToModalDasarCap { get; private set; }
        public decimal RecourseDelay { get; private set; }
        public decimal DelayYears { get; private set; }
        public decimal OpexGrowth { get; private set; }
        public decimal Opex { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public bool? Status { get; private set; }
        public decimal ValuePMNToModalDasarCap { get; private set; }

        public PMN()
        { }

        public PMN(int pmnToModalDasarCap, decimal recourseDelay, decimal delayYears, decimal opexGrowth, decimal opex, int? createBy, DateTime? createDate, bool? status, decimal valuePMNToModalDasarCap)
        {
            
            this.PMNToModalDasarCap = pmnToModalDasarCap;
            this.RecourseDelay = recourseDelay;
            this.DelayYears = delayYears;
            this.OpexGrowth = opexGrowth;
            this.Opex = opex;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.Status = status;
            this.ValuePMNToModalDasarCap = valuePMNToModalDasarCap;
        }

        public virtual void Update(int pmnToModalDasarCap, decimal recourseDelay, decimal delayYears, decimal opexGrowth, decimal opex, int? updateBy, DateTime? updateDate, bool? status, decimal valuePMNToModalDasarCap)
        {
            this.PMNToModalDasarCap = pmnToModalDasarCap;
            this.RecourseDelay = recourseDelay;
            this.DelayYears = delayYears;
            this.OpexGrowth = opexGrowth;
            this.Opex = opex;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
            this.Status = status;
            this.ValuePMNToModalDasarCap = valuePMNToModalDasarCap;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
