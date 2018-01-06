using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class AssetDataConfig : EntityTypeConfiguration<AssetData>
    {
        public AssetDataConfig()
        {
            //table
            ToTable("tblAssetDatas");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AssetClass).HasColumnName("assetClass");
            Property(x => x.TermAwal).HasColumnName("termAwal");
            Property(x => x.TermAkhir).HasColumnName("termAkhir");
            Property(x => x.AssumentReturn).HasColumnName("assumentReturn");
            Property(x => x.OutstandingStartYears).HasColumnName("outstandingStartYears");
            Property(x => x.OutstandingEndYears).HasColumnName("outstandingEndYears");
            Property(x => x.AssetValue).HasColumnName("assetValue");
            Property(x => x.Porpotion).HasColumnName("porpotion");
            Property(x => x.AssumedReturnPercentage).HasColumnName("assumedReturnPercentage");
            Property(x => x.AssumedReturn).HasColumnName("assumedReturn");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");
            Property(x => x.Status).HasColumnName("status");
        }
    }
}
