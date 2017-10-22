using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basico.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        BasicoContext dbContext;

        public BasicoContext Init()
        {
            return dbContext ?? (dbContext = new BasicoContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
