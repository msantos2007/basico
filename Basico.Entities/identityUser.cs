using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basico.Entities
{
    public class identityUser : IEntityBase
    {
        public identityUser()
        {
            identityUserRoles = new List<identityUserRole>();           
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<identityUserRole> identityUserRoles { get; set; }
        

        //for every table
        public DateTime? dt_criacao { get; set; }
        public string usr_criacao { get; set; }
        public DateTime? dt_alteracao { get; set; }
        public string usr_alteracao { get; set; }
    }
}
