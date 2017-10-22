using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Basico.Data;
using Basico.Entities;
using Basico.Services;
using Basico.Data.Infrastructure;
using Basico.Data.Repositories;
using Autofac;
using System.Web.Http;
using System.Reflection;
using System.Data.Entity;
using Autofac.Integration.WebApi;
using Basico.Web.Infrastructure.Core;



namespace Basico.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF BasicoContext
            builder.RegisterType<BasicoContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            // Services
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            //?? Registrar esta merda para as extensoes
            builder.RegisterType<DataRepositoryFactory>()
                .As<IDataRepositoryFactory>().InstancePerRequest();
				
            builder.RegisterType<CommunicationService>()
                .As<ICommunicationService>()
                .InstancePerRequest();
            

            Container = builder.Build();

            return Container;
        }
    }
}