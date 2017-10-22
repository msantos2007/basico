using Basico.Data.Infrastructure;
using Basico.Data.Repositories;
using Basico.Entities;
using Basico.Web.Infrastructure.Extensions;

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using System.Threading.Tasks;




namespace Basico.Web.Infrastructure.Core
{
    public class ApiControllerBaseExtended : ApiController
    {
        protected List<Type> _requiredRepositories;

        protected readonly IDataRepositoryFactory _dataRepositoryFactory;

        // 20 - 3
        protected IEntityBaseRepository<Error> _errorsRepository;

        //protected IEntityBaseRepository<JobEmail> _jobEmailRepository;
        //protected IEntityBaseRepository<JobEmailMain> _jobEmailMainRepository;
        //protected IEntityBaseRepository<JobEmailMainDest> _jobEmailMainDestRepository;
        //protected IEntityBaseRepository<JobEmailMainDestLog> _jobEmailMainDestLogRepository;
        //protected IEntityBaseRepository<JobEmailMainDestParam> _jobEmailMainDestParamRepository;

        protected IEntityBaseRepository<IGPMPrincipal> _IGPMPrincipalRepository;
        protected IEntityBaseRepository<IGPMIndices> _IGPMIndicesRepository;

        protected IUnitOfWork _unitOfWork;

        private HttpRequestMessage RequestMessage;

        public ApiControllerBaseExtended(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            _unitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, List<Type> repos, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                RequestMessage = request;
                InitRepositories(repos);
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
        protected async Task<HttpResponseMessage> CreateHttpResponse(HttpRequestMessage request, List<Type> repos, Func<Task<HttpResponseMessage>> function)
        {
            HttpResponseMessage response = null;

            try
            {
                RequestMessage = request;
                InitRepositories(repos);
                response = await function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        private void InitRepositories(List<Type> entities)
        {
            _errorsRepository = _dataRepositoryFactory.GetDataRepository<Error>(RequestMessage);


            if (entities.Any(e => e.FullName == typeof(IGPMPrincipal).FullName))
                _IGPMPrincipalRepository = _dataRepositoryFactory.GetDataRepository<IGPMPrincipal>(RequestMessage);
            if (entities.Any(e => e.FullName == typeof(IGPMIndices).FullName))
                _IGPMIndicesRepository = _dataRepositoryFactory.GetDataRepository<IGPMIndices>(RequestMessage);


            //if (entities.Any(e => e.FullName == typeof(JobEmail).FullName))
            //    _jobEmailRepository = _dataRepositoryFactory.GetDataRepository<JobEmail>(RequestMessage);
            //if (entities.Any(e => e.FullName == typeof(JobEmailMain).FullName))
            //    _jobEmailMainRepository = _dataRepositoryFactory.GetDataRepository<JobEmailMain>(RequestMessage);
            //if (entities.Any(e => e.FullName == typeof(JobEmailMainDest).FullName))
            //    _jobEmailMainDestRepository = _dataRepositoryFactory.GetDataRepository<JobEmailMainDest>(RequestMessage);
            //if (entities.Any(e => e.FullName == typeof(JobEmailMainDestLog).FullName))
            //    _jobEmailMainDestLogRepository = _dataRepositoryFactory.GetDataRepository<JobEmailMainDestLog>(RequestMessage);
            //if (entities.Any(e => e.FullName == typeof(JobEmailMainDestParam).FullName))
            //    _jobEmailMainDestParamRepository = _dataRepositoryFactory.GetDataRepository<JobEmailMainDestParam>(RequestMessage);

        }

        private void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.UtcNow
                };

                _errorsRepository.Add(_error);
                _unitOfWork.Commit();
            }
            catch { }
        }
    }
}