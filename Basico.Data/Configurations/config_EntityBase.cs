using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class config_EntityBase<T> : EntityTypeConfiguration<T> where T : class, IEntityBase
    {
        public config_EntityBase()
        {
            HasKey(e => e.ID);
        }
    }
}
