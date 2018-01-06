using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class AssetDataDTO
    {
        public int Id { get; set; }
        public string AssetClass { get; set; }
        public int TermAwal { get; set; }
        public int TermAkhir { get; set; }
        public decimal AssumentReturn { get; set; }
        public int OutstandingStartYears { get; set; }
        public int OutstandingEndYears { get; set; }
        public decimal AssetValue { get; set; }
        public decimal Porpotion { get; set; }
        public decimal AssumedReturnPercentage { get; set; }
        public decimal AssumedReturn { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }

        public AssetDataDTO(AssetData model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.AssetClass = model.AssetClass;
            this.TermAwal = model.TermAwal;
            this.TermAkhir = model.TermAkhir;
            this.AssumentReturn = model.AssumentReturn;
            this.OutstandingStartYears = model.OutstandingStartYears;
            this.OutstandingEndYears = model.OutstandingEndYears;
            this.AssetValue = model.AssetValue;
            this.Porpotion = model.Porpotion;
            this.AssumedReturnPercentage = model.AssumedReturnPercentage;
            this.AssumedReturn = model.AssumedReturn;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
        }

        public static AssetDataDTO From(AssetData model)
        {
            return new AssetDataDTO(model);
        }

        public static IList<AssetDataDTO> From(IList<AssetData> collection)
        {
            IList<AssetDataDTO> colls = new List<AssetDataDTO>();
            foreach (var item in collection)
            {
                colls.Add(new AssetDataDTO(item));
            }
            return colls;
        }
    }
}
