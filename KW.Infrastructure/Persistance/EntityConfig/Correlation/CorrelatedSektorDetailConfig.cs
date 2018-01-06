using KW.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KW.Infrastructure.EntityConfig
{
    public class CorrelatedSektorDetailConfig : EntityTypeConfiguration<CorrelatedSektorDetail>
    {
        public CorrelatedSektorDetailConfig()
        {
            //table
            ToTable("tblCorrelatedSektorDetails");

            //key
            HasKey(x => x.Id);

            //property
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreateBy).HasColumnName("createBy");
            Property(x => x.CreateDate).HasColumnName("createDate");
            Property(x => x.UpdateBy).HasColumnName("updateBy");
            Property(x => x.UpdateDate).HasColumnName("updateDate");
            Property(x => x.IsDelete).HasColumnName("isDelete");
            Property(x => x.DeleteDate).HasColumnName("deleteDate");

            //foreign key
            Property(x => x.CorrelatedSektorId).HasColumnName("correlatedSektorId");
            Property(x => x.RiskRegistrasiIdRow).HasColumnName("riskRegistrasiIdRow");
            Property(x => x.RiskRegistrasiIdCol).HasColumnName("riskRegistrasiIdCol");
            Property(x => x.CorrelationMatrixId).HasColumnName("correlationMatrixId");
            //Property(x => x.RiskRegistrasiId).HasColumnName("riskRegistrasiId");

        }
    }
}
