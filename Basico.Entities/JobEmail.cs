
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basico.Entities
{
    public class JobEmail : IEntityBase
    {
        public int ID { get; set; }
        public bool bloqueio { get; set; }
        public DateTime? dt_bloqueio { get; set; }
        public DateTime? dt_desbloqueio { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }

    public class JobEmailMain : IEntityBase
    {
        public int ID { get; set; }

        public DateTime job_dt_agenda { get; set; }
        public DateTime job_dt_expiracao { get; set; }
        public bool job_concluido { get; set; }
        public bool job_juntar_userID { get; set; } //se vai colocar tudo no mesmo documento email quando userID for igual        
        public bool job_juntar_todos { get; set; }  //se vai colocar tudo no mesmo documento email independente userID
        public byte job_juntar_limite { get; set; } //quantos destinatários por email (4)

        public int? job_conta_interna { get; set; } //novo
        public int? job_conta_cliente { get; set; } //novo

        public string job_template_file { get; set; }
        public string job_template_html { get; set; }

        public ICollection<JobEmailMainDest> destinatarios { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }

    public class JobEmailMainDest : IEntityBase
    {
        public int ID { get; set; }
        public int JobEmailMainID { get; set; }
        public int dest_userID { get; set; }
        public string dest_email { get; set; }

        public DateTime dest_dt_agenda { get; set; }
        public DateTime dest_dt_expiracao { get; set; }
        public bool dest_concluido { get; set; }
        public bool dest_bloqueado { get; set; }
        public byte dest_tentativas { get; set; }

        public ICollection<JobEmailMainDestParam> parametros { get; set; }
        public ICollection<JobEmailMainDestLog> logs { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }

    public class JobEmailMainDestParam : IEntityBase
    {
        public int ID { get; set; }
        public int JobEmailMainDestID { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }

    public class JobEmailMainDestLog : IEntityBase
    {
        public int ID { get; set; }
        public int JobEmailMainDestID { get; set; }
        public int log_ID { get; set; }
        public string log_descricao { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }
}
