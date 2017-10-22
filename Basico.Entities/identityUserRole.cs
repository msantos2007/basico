using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basico.Entities
{
    public class identityUserRole : IEntityBase
    {
        public int ID { get; set; }
        public int identityUserID { get; set; }
        public int identityRoleID { get; set; }
        public virtual identityRole identityRole { get; set; }

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }
}
