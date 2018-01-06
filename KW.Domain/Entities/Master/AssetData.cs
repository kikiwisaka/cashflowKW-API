using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class AssetData : Entity
    {
        public string AssetClass { get; private set; }
        public int TermAwal { get; private set; }
        public int TermAkhir { get; private set; }
        public decimal AssumentReturn { get; private set; }
        public int OutstandingStartYears { get; private set; }
        public int OutstandingEndYears { get; private set; }
        public decimal AssetValue { get; private set; }
        public decimal Porpotion { get; private set; }
        public decimal AssumedReturnPercentage { get; private set; }
        public decimal AssumedReturn { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public bool? Status { get; private set; }

        public AssetData()
        { }

        public AssetData(string assetClass, int termAwal, int termAkhir, decimal assumentReturn, int outstandingStartYears, int outstandingEndYears, decimal assetValue, decimal porpotion, decimal assumedReturnPercentage, decimal assumedReturn, int? createBy, DateTime? createDate, bool? status)
        {
            this.AssetClass = assetClass;
            this.TermAwal = termAwal;
            this.TermAkhir = termAkhir;
            this.AssumentReturn = assumentReturn;
            this.OutstandingStartYears = outstandingStartYears;
            this.OutstandingEndYears = outstandingEndYears;
            this.AssetValue = assetValue;
            this.Porpotion = porpotion;
            this.AssumedReturnPercentage = assumedReturnPercentage;
            this.AssumedReturn = assumedReturn;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.Status = status;
        }

        public virtual void Update(string assetClass, int termAwal, int termAkhir, decimal assumentReturn, int outstandingStartYears, int outstandingEndYears, decimal assetValue, decimal porpotion, decimal assumedReturnPercentage, decimal assumedReturn, int? updateBy, DateTime? updateDate, bool? status)
        {
            this.AssetClass = assetClass;
            this.TermAwal = termAwal;
            this.TermAkhir = termAkhir;
            this.AssumentReturn = assumentReturn;
            this.OutstandingStartYears = outstandingStartYears;
            this.OutstandingEndYears = outstandingEndYears;
            this.AssetValue = assetValue;
            this.Porpotion = porpotion;
            this.AssumedReturnPercentage = assumedReturnPercentage;
            this.AssumedReturn = assumedReturn;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
            this.Status = status;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
