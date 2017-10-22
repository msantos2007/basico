using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;


namespace Basico.Services
{
    public interface IJobUsingService  
    {
        void pegaUsuario(int id);
    }

    public class JobUsingService : IJobUsingService
    {

        private int integer;

        public JobUsingService(int _integer)
        {
            integer = _integer;
        }

        public void pegaUsuario(int id)
        {
            //var result = someService.GetUser(0);
            int x = 0;
            x++;
        }
    }
}
