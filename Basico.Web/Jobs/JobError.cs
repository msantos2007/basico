using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Basico.Entities;
using Basico.Data;
using System.Threading;

using Basico.Web.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


using Quartz.Spi;
using Ninject.Syntax;
using Ninject;
using Basico.Services;
using Quartz.Simpl;

namespace Basico.Web
{

    public class NinjectJobFactory : IJobFactory
    {
        private readonly IResolutionRoot resolutionRoot;

        public NinjectJobFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)this.resolutionRoot.Get(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            this.resolutionRoot.Release(job);
        }
    }



    public class JobUsingServiceJob : IJob
    {
        private readonly IJobUsingService _JobUsingService; 

        public JobUsingServiceJob(IJobUsingService JobUsingService )
        {
            _JobUsingService = JobUsingService;
           // _member = member;
        }

        public void Execute(IJobExecutionContext context)
        {
            int bna = 0;
            _JobUsingService.pegaUsuario(0);
            bna++;
            
        }
    }




    public class JobScheduler
    {

        public static IKernel InitializeNinjectKernel()
        {
            var kernel = new StandardKernel();

            // setup Quartz scheduler that uses our NinjectJobFactory
            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler();
                sched.JobFactory = new NinjectJobFactory(kernel);
                return sched;
            });

            // add our bindings as we normally would (these are the bindings that our jobs require)
            kernel.Bind<IJobUsingService>().To<JobUsingService>(); 
            // etc.

            return kernel;
        }

        public static void Start()
        { 
            var kernel = InitializeNinjectKernel();
            var scheduler = kernel.Get<IScheduler>(); 

            scheduler.ScheduleJob(JobBuilder.Create<JobUsingServiceJob>().Build(), TriggerBuilder.Create().StartNow().WithSimpleSchedule(s => s.WithIntervalInSeconds(60).WithMisfireHandlingInstructionNextWithRemainingCount().RepeatForever()).Build());

            // start scheduler
            scheduler.Start();
        }
    }















    //Funciona
    //Job com SignalR
    //public abstract class IJobWithHub<THub> : IJob where THub : IHub
    //{
    //    Lazy<IHubContext> hub = new Lazy<IHubContext>(
    //        () => GlobalHost.ConnectionManager.GetHubContext<THub>()
    //    );

    //    public abstract void Execute(IJobExecutionContext context);

    //    protected IHubContext Hub
    //    {
    //        get { return hub.Value; }
    //    }
    //}

    //public class EmailJob<THub> : IJob where THub : IHub
    //{
    //    Lazy<IHubContext> hub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<THub>());

    //    protected IHubContext Hub
    //    {
    //        get { return hub.Value; }
    //    }

    //    public void Execute(IJobExecutionContext context)
    //    {
    //        Basico.Data.BasicoContext ctx = new Data.BasicoContext();

    //        Error newError = new Error();

    //        int contador = ctx.set_Error.Where(r => r.StackTrace == "Execute").Count();

    //        if (contador > 20)
    //        {
    //            ctx.Database.ExecuteSqlCommand("DELETE FROM Error WHERE StackTrace = 'Execute'");

    //            newError = new Error();
    //            newError.Message = "Quartz: Gravando Info";
    //            newError.StackTrace = "Limpou";
    //            newError.DateCreated = DateTime.UtcNow;
    //            ctx.set_Error.Add(newError);

    //            ctx.SaveChanges();

    //            //SignalR
    //            Hub.Clients.All.mostarToast("Limpando");
    //            contador = 0;
    //        }

    //        newError = new Error();
    //        newError.Message = "Quartz: Gravando Info";
    //        newError.StackTrace = "Execute";
    //        newError.DateCreated = DateTime.UtcNow;


    //        ctx.set_Error.Add(newError);
    //        ctx.SaveChanges();

    //        //SignalR
    //        Hub.Clients.All.mostarToast("Gravando " + (contador + 1));


    //        int zero = 0;
    //        int div = 5 / zero;
    //    }
    //}

    //public class JobScheduler
    //{
    //    public static void Start()
    //    {
    //        Basico.Data.BasicoContext ctx = new Data.BasicoContext();
    //        Error newError = new Error();
    //        newError.Message = "Quartz: Iniciou Job";
    //        newError.StackTrace = "Start";
    //        newError.DateCreated = DateTime.UtcNow;
    //        ctx.set_Error.Add(newError);
    //        ctx.SaveChanges();


    //        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
    //        scheduler.Start();

    //        IJobDetail job = JobBuilder.Create<EmailJob<BasicoHub>>().Build();

    //        ITrigger trigger = TriggerBuilder.Create().StartNow()
    //                                                  .WithSimpleSchedule(x => x.WithIntervalInSeconds(3600)
    //                                                                            .WithMisfireHandlingInstructionNextWithRemainingCount()
    //                                                  .RepeatForever())
    //                                                  .Build();

    //        scheduler.ScheduleJob(job, trigger);
    //    }
    //}



















    //:: stackoverflow.com/questions/3246277/handle-jobexecutionexception-in-quartz-net
    //using Basico.Data.Repositories;
    //using Basico.Data.Infrastructure;
    //using Basico.Web.Infrastructure.Core;
    //Não funciona repositórios

    //public abstract class JobBase : IJob
    //{
    //    protected readonly IDataRepositoryFactory _dataRepositoryFactory;
    //    protected readonly IEntityBaseRepository<Error> _errorsRepository;
    //    protected readonly IUnitOfWork _unitOfWork;

    //    //protected JobBase(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork, IEntityBaseRepository<Error> errorsRepository)
    //    //{
    //    //    _dataRepositoryFactory = dataRepositoryFactory;
    //    //    _unitOfWork = unitOfWork;
    //    //    _errorsRepository = errorsRepository;
    //    //}

    //    protected JobBase()
    //    {

    //    }

    //    public abstract void ExecuteJob(IJobExecutionContext context);

    //    public void Execute(IJobExecutionContext context)
    //    {
    //        try
    //        {
    //            ExecuteJob(context);
    //        }
    //        catch (Exception e)
    //        {
    //            // Log exception
    //        }
    //    }
    //}

    //public class SomeJob : JobBase
    //{
    //    protected readonly new IDataRepositoryFactory _dataRepositoryFactory;
    //    protected readonly new IEntityBaseRepository<Error> _errorsRepository;
    //    protected readonly new IUnitOfWork _unitOfWork;

    //    public SomeJob(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork, IEntityBaseRepository<Error> errorsRepository) //: base (dataRepositoryFactory, unitOfWork, errorsRepository)
    //    {
    //        _dataRepositoryFactory = dataRepositoryFactory;
    //        _unitOfWork = unitOfWork;
    //        _errorsRepository = errorsRepository;
    //    }

    //    public override void ExecuteJob(IJobExecutionContext context)
    //    {
    //        // Do the actual job here
    //        Error newError = new Error();

    //        newError = new Error();
    //        newError.Message = "Quartz: Gravando Info";
    //        newError.StackTrace = "Limpou";
    //        newError.DateCreated = DateTime.UtcNow;

    //        _errorsRepository.Add(newError);
    //        _unitOfWork.Commit();
    //    }
    //}

    //public class JobScheduler_Teste
    //{
    //    public static void Start()
    //    {

    //        Basico.Data.BasicoContext ctx = new Data.BasicoContext();

    //        Error newError = new Error();


    //        newError.Message = "Quartz: Iniciou Job";
    //        newError.StackTrace = "Start";
    //        newError.DateCreated = DateTime.UtcNow;


    //        ctx.set_Error.Add(newError);
    //        ctx.SaveChanges();

    //        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
    //        scheduler.Start();

    //        IJobDetail job = JobBuilder.Create<SomeJob>().Build();

    //        ITrigger trigger = TriggerBuilder.Create().StartNow()
    //                                                  .WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
    //                                                                            .WithMisfireHandlingInstructionNextWithRemainingCount()
    //                                                  .RepeatForever())
    //                                                  .Build();

    //        scheduler.ScheduleJob(job, trigger);
    //    }
    //}




}
