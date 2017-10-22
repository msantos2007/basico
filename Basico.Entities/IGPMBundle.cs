using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basico.Entities
{
    public class IGPMPrincipal : IEntityBase
    {
        public int ID { get; set; }
        public bool ativo { get; set; }
        public DateTime? dt_ultima_execucao { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }

    public class IGPMIndices : IEntityBase
    {
        public int ID { get; set; }
        public bool ativo { get; set; }
        public DateTime dt_referencia { get; set; }
        //public DateTime dt_ini { get; set; }
        //public DateTime dt_fim { get; set; }
        public double indice { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }
}
