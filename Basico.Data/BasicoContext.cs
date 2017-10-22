using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Basico.Entities;
using Basico.Data.Configurations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Basico.Data
{
    public class BasicoContext : DbContext
    {
        public BasicoContext() : base ("Basico")
        {
            //Database.SetInitializer<BasicoContext>(null);
            Database.SetInitializer<BasicoContext>(new BasicoInitializer());
        }
        public IDbSet<Error> set_Error { get; set; }
        public IDbSet<identityRole> set_identityRole { get; set; }
        public IDbSet<identityUser> set_identityUser { get; set; }
        public IDbSet<identityUserRole> set_identityUserRole { get; set; }

        public IDbSet<IGPMPrincipal> IGPMPrincipalSet { get; set; }
        public IDbSet<IGPMIndices> IGPMIndicesSet { get; set; }

        //public IDbSet<JobEmail> set_JobEmail { get; set; }
        //public IDbSet<JobEmailMain> set_JobEmailMain { get; set; }
        //public IDbSet<JobEmailMainDest> set_JobEmailMainDest { get; set; }
        //public IDbSet<JobEmailMainDestLog> set_JobEmailMainDestLog { get; set; }
        //public IDbSet<JobEmailMainDestParam> set_JobEmailMainDestParam { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new config_identityRole());
            modelBuilder.Configurations.Add(new config_identityUser());
            modelBuilder.Configurations.Add(new config_identityUserRole());

            modelBuilder.Configurations.Add(new IGPMPrincipalConfiguration());
            modelBuilder.Configurations.Add(new IGPMIndicesConfiguration());

            //modelBuilder.Configurations.Add(new JobEmailConfiguration());
            //modelBuilder.Configurations.Add(new JobEmailMainConfiguration());
            //modelBuilder.Configurations.Add(new JobEmailMainDestConfiguration());            
            //modelBuilder.Configurations.Add(new JobEmailMainDestLogConfiguration());
            //modelBuilder.Configurations.Add(new JobEmailMainDestParamConfiguration());

        }
    }
}
