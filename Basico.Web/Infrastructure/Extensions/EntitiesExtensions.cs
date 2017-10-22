using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Basico.Entities;
using Basico.Web.Models;

using System.Reflection;
namespace Basico.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {

        public static void UpdateUser(this identityUser user, UserEditViewModel userVm)
        {
            user.Username = userVm.Username;
            user.Firstname = userVm.Firstname;
            user.Email = userVm.Email;
            user.HashedPassword = userVm.HashedPassword;
            user.Salt = userVm.Salt;

            //// Every table
            //DateTime now = DateTime.UtcNow;
            //user.usr_criacao = userVm.usr_criacao;
            //user.dt_criacao = (userVm.dt_criacao == null ? now : userVm.dt_criacao);
            //user.usr_alteracao = userVm.usr_alteracao;
            //user.dt_alteracao = ((userVm.usr_alteracao == "" || userVm.usr_alteracao == null) ? user.dt_alteracao : now);

        }
        
        //Example
        //public static void UpdateSomeThing(this Cond entity, CondominioViewModel viewmodel)
        //{

        //    // Every table
        //    DateTime now = DateTime.UtcNow;
        //    entity.usr_criacao = viewmodel.cond.usr_criacao;
        //    entity.dt_criacao = (viewmodel.cond.dt_criacao == null ? now : viewmodel.cond.dt_criacao);
        //    entity.usr_alteracao = viewmodel.cond.usr_alteracao;
        //    entity.dt_alteracao = ((viewmodel.cond.usr_alteracao == "" || viewmodel.cond.usr_alteracao == null) ? entity.dt_alteracao : now);
        //}
    }
}