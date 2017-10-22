using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class config_identityRole : config_EntityBase<identityRole>
    {
        public config_identityRole()
        {
            Property(ur => ur.Name).IsRequired().HasMaxLength(50);

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }
}
