using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class IGPMPrincipalConfiguration : config_EntityBase<IGPMPrincipal> 
    {
        public IGPMPrincipalConfiguration()
        {
            Property(ur => ur.ativo).IsRequired();
            Property(ur => ur.dt_ultima_execucao).IsOptional(); 

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }

    public class IGPMIndicesConfiguration : config_EntityBase<IGPMIndices>
    {
        public IGPMIndicesConfiguration()
        {
            Property(ur => ur.ativo).IsRequired();
            Property(ur => ur.indice).IsRequired();
            Property(ur => ur.dt_referencia).IsRequired();
            //Property(ur => ur.dt_ini).IsRequired();
            //Property(ur => ur.dt_fim).IsRequired();

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }
}
