using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basico.Entities;
using Basico.Data.Repositories;

namespace Basico.Data.Extensions
{
    public static class identityUserExtensions
    {
        public static identityUser GetSingleByUsername(this IEntityBaseRepository<identityUser> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }

        public static identityUser GetSingleByEmail(this IEntityBaseRepository<identityUser> userRepository, string email)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Email == email);
        }
    }
}
