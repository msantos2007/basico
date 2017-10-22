using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basico.Data;
using Basico.Entities;

namespace Basico.Services
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        identityUser CreateUser(string username, string firstname, string email, string password, int[] roles, string usuario_logado);
        identityUser GetUser(int userId);
        identityUser GetUserUsername(string username);
        List<identityUser> GetAllUsers();
        List<identityRole> GetUserRoles(string username);
        identityUser UpdateUser(int userID, string newusername, string newfirstname, string newemail, string oldpassword, string newpassword, int[] roles, string usuario);
        List<identityRole> GetAllRoles();
        Boolean VerificarExistente(string tipo, string valor);
    }
}
