using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class SubRiskRegistrasiConfig : EntityTypeConfiguration<SubRiskRegistrasi>
    {
        public SubRiskRegistrasiConfig()
        {
            //table
            ToTable("tblSubRiskRegistrasis");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.KodeRisk).HasColumnName("kodeRisk");
            Property(x => x.RiskEvenClaim).HasColumnName("riskEvenClaim");
            Property(x => x.DescriptionRiskEvenClaim).HasColumnName("descriptionRiskEvenClaim");
            Property(x => x.SugestionMigration).HasColumnName("sugestionMigration");
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //foreign key
            Property(x => x.RiskRegistrasiId).HasColumnName("riskRegistrasiId");
        }
    }
}
