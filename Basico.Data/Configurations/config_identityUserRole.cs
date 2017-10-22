using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class config_identityUserRole : config_EntityBase<identityUserRole>
    {
        public config_identityUserRole()
        {
            Property(ur => ur.identityUserID).IsRequired();
            Property(ur => ur.identityRoleID).IsRequired();

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }
}
