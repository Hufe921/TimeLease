using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rita.System.UnitOfWork;

namespace TimeLease.Domain.Entities
{
    public class BaseConfig<T> : EntityTypeConfiguration<T>
         where T : BaseEntity, IStored
    {
        public BaseConfig()
        {
            HasKey(prop => prop.ID)
                .Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(prop => prop.Version).IsConcurrencyToken(true).IsRowVersion();
        }
    }
}
