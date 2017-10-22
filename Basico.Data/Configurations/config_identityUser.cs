using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class config_identityUser : config_EntityBase<identityUser>
    {
        public config_identityUser()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(100);
            Property(u => u.Firstname).IsOptional().HasMaxLength(100);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.IsLocked).IsRequired();
            Property(u => u.DateCreated);
            
            Property(u => u.dt_criacao).IsOptional();
            Property(u => u.usr_criacao).IsOptional().HasMaxLength(50);
            Property(u => u.dt_alteracao).IsOptional();
            Property(u => u.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }
}
