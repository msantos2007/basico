using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Basico.Data;
using Basico.Data.Extensions;
using Basico.Data.Infrastructure;
using Basico.Data.Repositories;
using Basico.Entities;
using System.Security.Principal;

namespace Basico.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IEntityBaseRepository<identityUser> _repo_identityUser;
        private readonly IEntityBaseRepository<identityRole> _repo_identityRole;
        private readonly IEntityBaseRepository<identityUserRole> _repo_identityUserRole;        
        
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public MembershipService(IEncryptionService encryptionService
                               , IUnitOfWork unitOfWork
                               , IEntityBaseRepository<identityUser> repo_identityUser
                               , IEntityBaseRepository<identityRole> repo_identityRole
                               , IEntityBaseRepository<identityUserRole> repo_identityUserRole)
        {
            _repo_identityUser = repo_identityUser;
            _repo_identityRole = repo_identityRole;
            _repo_identityUserRole = repo_identityUserRole;

            _encryptionService = encryptionService;            
            _unitOfWork = unitOfWork;
        }

        #region Helper methods
        private void addUserToRole(identityUser user, int roleId)
        {
            var role = _repo_identityRole.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist.");

            var userRole = new identityUserRole()
            {
                identityRoleID = role.ID,
                identityUserID = user.ID
            };
            _repo_identityUserRole.Add(userRole);
        }

        private bool isPasswordValid(identityUser user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(identityUser user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
        #endregion
        
        public identityUser CreateUser(string username, string firstname, string email, string password, int[] roles, string usuario = "sistema")
        {
            usuario = (usuario == null || usuario == "" ? "sistema" : usuario);
            var existingUser = _repo_identityUser.GetSingleByUsername(username);

            if (existingUser != null)
            {
                throw new Exception("Usuário já existe!");
            }

            var existingEmail = _repo_identityUser.GetSingleByEmail(email);
            if (existingEmail != null)
            {
                throw new Exception("Email já em uso!");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new identityUser()
            {
                Username = username,
                Firstname = firstname,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.UtcNow,

                usr_criacao = usuario,
                dt_criacao = DateTime.UtcNow
            };

            _repo_identityUser.Add(user);

            _unitOfWork.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }

            _unitOfWork.Commit();

            return user;
        }
        public List<identityRole> GetUserRoles(string username)
        {
            List<identityRole> _result = new List<identityRole>();

            var existingUser = _repo_identityUser.GetSingleByUsername(username);

            if (existingUser != null)
            {
                //?? add order by 
                foreach (var userRole in existingUser.identityUserRoles.OrderBy(z => z.identityRoleID).ToList())
                {
                    _result.Add(userRole.identityRole);
                }
            }

            return _result.Distinct().ToList();
        }
        public identityUser GetUser(int userId)
        {
            return _repo_identityUser.GetSingle(userId);
        }
        public identityUser GetUserUsername(string username)
        {
            return _repo_identityUser.GetSingleByUsername(username);
        }
        public List<identityUser> GetAllUsers()
        {
                return _repo_identityUser.GetAll().ToList();
        }        
        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _repo_identityUser.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(identity, userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }
        public identityUser UpdateUser(int userID, string newusername, string newfirstname, string newemail, string oldpassword, string newpassword, int[] roles, string usuario)
        {
            var existingUserDB = _repo_identityUser.GetSingle(userID);

            if (existingUserDB == null)
            {
                throw new Exception("Usuário não existe");
            }

            existingUserDB.IsLocked = false;
            existingUserDB.Username = newusername;
            existingUserDB.Firstname = newfirstname;
            existingUserDB.Email = newemail;
            existingUserDB.usr_alteracao = usuario;
            existingUserDB.dt_alteracao = DateTime.UtcNow;

            //?? verificar senha antiga
            if (!String.IsNullOrEmpty(newpassword))
            {
                var passwordSalt = _encryptionService.CreateSalt();
                existingUserDB.HashedPassword = _encryptionService.EncryptPassword(newpassword, passwordSalt);
                existingUserDB.Salt = passwordSalt;
            }

            _repo_identityUser.Edit(existingUserDB);
            _unitOfWork.Commit();

            //if (roles != null || roles.Length > 0)
            //{
            //    foreach (var role in roles)
            //    {
            //        addUserToRole(user, role);
            //    }
            //}

            //_unitOfWork.Commit();

            return existingUserDB;
        }
        public List<identityRole> GetAllRoles()
        {
            return _repo_identityRole.GetAll().ToList();
        }
        public bool VerificarExistente(string tipo, string valor)
        {

            if (tipo == "username")
            { 
                var existingUser = _repo_identityUser.GetSingleByUsername(valor);
                if (existingUser != null)            
                    return true;
            }

            if (tipo == "email")
            { 
                var existingEmail = _repo_identityUser.GetSingleByEmail(valor);
                if (existingEmail != null)
                    return true;
            }

            return false;
        }
    }
}
