using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Basico.Data.Configurations
{
    public class JobEmailConfiguration : config_EntityBase<JobEmail>
    {
        public JobEmailConfiguration()
        {
            Property(ur => ur.bloqueio).IsRequired();
            Property(ur => ur.dt_bloqueio).IsOptional();
            Property(ur => ur.dt_desbloqueio).IsOptional();

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }

    public class JobEmailMainConfiguration : config_EntityBase<JobEmailMain>
    {
        public JobEmailMainConfiguration()
        {
            Property(ur => ur.job_dt_agenda).IsRequired();
            Property(ur => ur.job_dt_expiracao).IsRequired();
            Property(ur => ur.job_juntar_userID).IsRequired();
            Property(ur => ur.job_juntar_todos).IsRequired();
            Property(ur => ur.job_juntar_limite).IsRequired();
            Property(ur => ur.job_conta_interna).IsOptional();
            Property(ur => ur.job_conta_cliente).IsOptional();
            Property(ur => ur.job_template_file).IsOptional().HasMaxLength(500);
            Property(ur => ur.job_template_html).IsOptional().HasMaxLength(8000);

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }

    public class JobEmailMainDestConfiguration : config_EntityBase<JobEmailMainDest>
    {
        public JobEmailMainDestConfiguration()
        {
            Property(ur => ur.JobEmailMainID).IsRequired();
            Property(ur => ur.dest_userID).IsRequired();
            Property(ur => ur.dest_email).IsRequired();

            Property(ur => ur.dest_dt_agenda).IsRequired();
            Property(ur => ur.dest_dt_expiracao).IsRequired();
            Property(ur => ur.dest_concluido).IsRequired();
            Property(ur => ur.dest_bloqueado).IsRequired();
            Property(ur => ur.dest_tentativas).IsRequired();


            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }

    public class JobEmailMainDestLogConfiguration : config_EntityBase<JobEmailMainDestLog>
    {
        public JobEmailMainDestLogConfiguration()
        {
            Property(ur => ur.JobEmailMainDestID).IsRequired();
            Property(ur => ur.log_ID).IsRequired();
            Property(ur => ur.log_descricao).IsRequired();

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }

    public class JobEmailMainDestParamConfiguration : config_EntityBase<JobEmailMainDestParam>
    {
        public JobEmailMainDestParamConfiguration()
        {
            Property(ur => ur.JobEmailMainDestID).IsRequired();
            Property(ur => ur.param_name).IsRequired();
            Property(ur => ur.param_value).IsRequired();

            Property(ur => ur.dt_criacao).IsOptional();
            Property(ur => ur.usr_criacao).IsOptional().HasMaxLength(50);
            Property(ur => ur.dt_alteracao).IsOptional();
            Property(ur => ur.usr_alteracao).IsOptional().HasMaxLength(50);
        }
    }


}
