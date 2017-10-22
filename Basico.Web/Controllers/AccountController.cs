using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Basico.Data.Infrastructure;
using Basico.Data.Repositories;
using Basico.Entities;
using Basico.Services;
using Basico.Web.Infrastructure.Core;
using Basico.Web.Models;

using System.Net;
using System.Net.Http;
using System.Web.Http;

using AutoMapper;
using Basico.Web.Hubs;

using System.Web.Http.Cors;
using Newtonsoft.Json;
using RestSharp;

using Basico.Web.BCBWevService;

using System.IO;
using System.IO.Compression;


using System.Web.Caching;


namespace Basico.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Admin, Convidado, Prestador, Demandante")]
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase<BasicoHub>
    {
        private readonly IMembershipService _membershipService;
         
        public AccountController(IMembershipService membershipService,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _membershipService = membershipService;
        }

        public class htmlHashViewModel
        {
            public string file_name { get; set; }
            public string timestamp { get; set; }
        }
        
        public List<htmlHashViewModel> buscarHTML()
        {
            System.Diagnostics.Debugger.Break();
            List<htmlHashViewModel> listaHTML = new List<htmlHashViewModel>();
            Cache context = new Cache();

            var files = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/Scripts"), "*.html", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileinfo = new System.IO.FileInfo(file.ToString());
                //.LastWriteTime.ToString("yyyyMMddhhmmss");

                var nome = fileinfo.Name;
                var data = fileinfo.LastWriteTime.ToString("yyyyMMddhhmmss");

                if (context.Get(nome) == null)
                {
                    context.Insert(nome, data, null, DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }

                //context.Get(nome) = data;

                htmlHashViewModel nova = new htmlHashViewModel();
                nova.file_name = nome;
                nova.timestamp = context.Get(nome).ToString();

                listaHTML.Add(nova);
            }
            

            return listaHTML;
        }

        [AllowAnonymous]
        [Route("htmlHash")]
        [HttpGet]
        public HttpResponseMessage htmlHash(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<htmlHashViewModel> listaHTML = new List<htmlHashViewModel>();

                listaHTML = buscarHTML();


                response = request.CreateResponse(HttpStatusCode.OK, new {  });
                return response;
            });
        }

        [AllowAnonymous]
        [Route("pegadataSQL")]
        [HttpGet]
        public HttpResponseMessage pegadataSQL(HttpRequestMessage request, DateTime data)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DateTime datat = data.ToUniversalTime();

                var ss = datat.ToUniversalTime();

                var st = ss.ToLocalTime();

                Basico.Data.BasicoContext ctx = new Data.BasicoContext();

                Error errnovo = new Error();

                errnovo.DateCreated = ss; // DateTime.Now;
                errnovo.StackTrace = "Teste Data";
                errnovo.Message = "Teste Data " + DateTime.UtcNow.ToString();

                ctx.set_Error.Add(errnovo);
                ctx.SaveChanges();



                DateTime novaDataForcada = DateTime.Parse("1996-12-01T00:00:00.0000000Z", null, System.Globalization.DateTimeStyles.RoundtripKind);


                //var resp = ctx.Database.ExecuteSqlCommand("SELECT TOP 1 FROM identityUser");

                Error rr = ctx.set_Error.OrderByDescending(r => r.ID).FirstOrDefault();

                response = request.CreateResponse(HttpStatusCode.OK, new { SQL = rr, datanow = DateTime.Now, datanowUTC = DateTime.UtcNow, novaDataForcada = novaDataForcada });
                return response;
            });
        }


        [AllowAnonymous]
        [Route("authenticateWithToken/{token}")]
        [HttpGet]
        public HttpResponseMessage LoginWithToken(HttpRequestMessage request, string token)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, new { success = true, elevador_usr = "root", elevador_pwd = "a" });
                return response;
            });
        }

        public int BuscaIGPM()
        {
            //Hub.Clients.All.mostarToast("1");
            int resposta = 0;
            try
            {
                //Hub.Clients.All.mostarToast("2");
                //Update-Database -Script || uma antes Update-Database -Script -SourceMigration:201612190500139_inicial
                Basico.Web.BCBWevService.FachadaWSSGSClient wsbc = new FachadaWSSGSClient();
                //Hub.Clients.All.mostarToast("2-1 - SQL");
                Basico.Data.BasicoContext ctx = new Data.BasicoContext();

                DateTime dt_hoje = GlobalDateTime.LocalNow().Date;
                DateTime dt_prim = GlobalDateTime.Any2Noon(new DateTime(2015, 12, 1));
                DateTime dt_ultm = GlobalDateTime.Any2Noon(new DateTime(dt_hoje.Year, dt_hoje.Month, 1));

                if (dt_hoje.Day < DateTime.DaysInMonth(dt_hoje.Year, dt_hoje.Month)) dt_ultm = dt_ultm.AddMonths(-1);
                //Hub.Clients.All.mostarToast("2-2 - SQL");

                DateTime? ultimoIndice = null;
                if (ctx.IGPMIndicesSet.Count() > 0)
                {
                    ultimoIndice = ctx.IGPMIndicesSet.Max(r => r.dt_referencia).Date; //TODO_Billet : TIPTIPTIP (DateTime?) Dentro para permitir retorno de nulo do repository.
                }

                //Hub.Clients.All.mostarToast("3");
                DateTime dt_prox = dt_prim.AddMonths(1);
                if (ultimoIndice != null)
                {
                    //Hub.Clients.All.mostarToast("4");
                    dt_prim = GlobalDateTime.Any2Noon((DateTime)ultimoIndice);
                    dt_prox = dt_prim.AddMonths(1);
                }
                
                Basico.Web.BCBWevService.WSSerieVO[] rss = null;
                long[] series = new long[] { 189 };
                while (dt_prox <= dt_ultm)
                {
                    //Hub.Clients.All.mostarToast("5");
                    string dt_ini = dt_prox.Date.ToString("dd/MM/yyyy");
                    string dt_fim = dt_prox.AddMonths(5).Date.ToString("dd/MM/yyyy");

                    rss = wsbc.getValoresSeriesVO(series, dt_ini, dt_fim);

                    if (rss.Count() > 0)
                    {
                        var serie = rss[0];
                        var valrs = serie.valores;

                        foreach (var cada in valrs)
                        {
                            IGPMIndices novoIGPM = new IGPMIndices();

                            novoIGPM.ativo = true;
                            novoIGPM.dt_criacao = GlobalDateTime.LocalNow();
                            novoIGPM.usr_criacao = "sistema";
                            novoIGPM.indice = double.Parse(cada.svalor, System.Globalization.CultureInfo.InvariantCulture);
                            novoIGPM.dt_referencia = GlobalDateTime.Any2Noon(dt_prox);
                            //novoIGPM.dt_ini = dt_prox;
                            //novoIGPM.dt_fim = dt_prox.AddMonths(1).AddDays(-1);

                            ctx.IGPMIndicesSet.Add(novoIGPM);
                            ctx.SaveChanges();

                            dt_prox = dt_prox.AddMonths(1);
                        }
                    }

                    rss = null;

                    //Tartaruga
                    System.Diagnostics.Stopwatch esperar = System.Diagnostics.Stopwatch.StartNew();
                    //Hub.Clients.All.mostarToast("6");
                    while (esperar.Elapsed.TotalSeconds < 10 && dt_prox <= dt_ultm)
                    {

                    }
                    esperar.Stop();                    
                }
                //Hub.Clients.All.mostarToast("7");
                wsbc.Close();
                ctx.Dispose();

                resposta = 1;
            }
            catch(Exception ex)
            {
                Hub.Clients.All.mostarToast(ex.Message);
                resposta = 9; 
            }
            //Hub.Clients.All.mostarToast("8");
            return resposta;
        }

        public int BuscaIGPMFunciona()
        {
            //Hub.Clients.All.mostarToast("1");
            int resposta = 0;
            try
            {
                //Hub.Clients.All.mostarToast("2");
                //Update-Database -Script || uma antes Update-Database -Script -SourceMigration:201612190500139_inicial
                Basico.Web.BCBWevService.FachadaWSSGSClient wsbc = new FachadaWSSGSClient();
                //Hub.Clients.All.mostarToast("2-1 - SQL");
                Basico.Data.BasicoContext ctx = new Data.BasicoContext();

                DateTime dt_hoje = DateTime.UtcNow.Date;
                DateTime dt_prim = new DateTime(2015, 12, 1);
                DateTime dt_ultm = new DateTime(dt_hoje.Year, dt_hoje.Month, 1);

                if (dt_hoje.Day < DateTime.DaysInMonth(dt_hoje.Year, dt_hoje.Month)) dt_ultm = dt_ultm.AddMonths(-1);
                //Hub.Clients.All.mostarToast("2-2 - SQL");

                DateTime? ultimoIndice = null;
                if (ctx.IGPMIndicesSet.Count() > 0)
                {
                    ultimoIndice = ctx.IGPMIndicesSet.Max(r => r.dt_referencia); //TODO_Billet : TIPTIPTIP (DateTime?) Dentro para permitir retorno de nulo do repository.
                }

                //Hub.Clients.All.mostarToast("3");
                DateTime dt_prox = dt_prim.AddMonths(1);
                if (ultimoIndice != null)
                {
                    //Hub.Clients.All.mostarToast("4");
                    dt_prim = (DateTime)ultimoIndice;
                    dt_prox = dt_prim.AddMonths(1);
                }

                Basico.Web.BCBWevService.WSSerieVO[] rss = null;
                long[] series = new long[] { 189 };
                while (dt_prox <= dt_ultm)
                {
                    //Hub.Clients.All.mostarToast("5");
                    string dt_ini = dt_prox.Date.ToString("dd/MM/yyyy");
                    string dt_fim = dt_prox.AddMonths(5).Date.ToString("dd/MM/yyyy");

                    rss = wsbc.getValoresSeriesVO(series, dt_ini, dt_fim);

                    if (rss.Count() > 0)
                    {
                        var serie = rss[0];
                        var valrs = serie.valores;

                        foreach (var cada in valrs)
                        {
                            IGPMIndices novoIGPM = new IGPMIndices();

                            novoIGPM.ativo = true;
                            novoIGPM.dt_criacao = DateTime.UtcNow;
                            novoIGPM.usr_criacao = "sistema";
                            novoIGPM.indice = double.Parse(cada.svalor, System.Globalization.CultureInfo.InvariantCulture);
                            novoIGPM.dt_referencia = dt_prox;
                            //novoIGPM.dt_ini = dt_prox;
                            //novoIGPM.dt_fim = dt_prox.AddMonths(1).AddDays(-1);

                            ctx.IGPMIndicesSet.Add(novoIGPM);
                            ctx.SaveChanges();

                            dt_prox = dt_prox.AddMonths(1);
                        }
                    }

                    rss = null;

                    //Tartaruga
                    System.Diagnostics.Stopwatch esperar = System.Diagnostics.Stopwatch.StartNew();
                    //Hub.Clients.All.mostarToast("6");
                    while (esperar.Elapsed.TotalSeconds < 10 && dt_prox <= dt_ultm)
                    {

                    }
                    esperar.Stop();
                }
                //Hub.Clients.All.mostarToast("7");
                wsbc.Close();
                ctx.Dispose();

                resposta = 1;
            }
            catch (Exception ex)
            {
                Hub.Clients.All.mostarToast(ex.Message);
                resposta = 9;
            }
            //Hub.Clients.All.mostarToast("8");
            return resposta;
        }


        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                BuscaIGPM();


                //Basico.Data.BasicoContext ctx = new Data.BasicoContext();

                //Error newError = new Error();


                //newError.Message = "Autenticando";
                //newError.StackTrace = "Execute";
                //newError.DateCreated = DateTime.UtcNow;


                //ctx.set_Error.Add(newError);
                //ctx.SaveChanges();
                //ctx.Commit();











                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                    if (_userContext.User != null)
                    {
                        user.Firstname = _userContext.User.Firstname;                        
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true, user });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {

                    List<Entities.identityUser> users = _membershipService.GetAllUsers();

                    int primeiroUsuario = 0;

                    if (users.Count == 0)
                        primeiroUsuario = 1;  //O primeiro será root; os demais, usuarios.
                    else
                        primeiroUsuario = 3;

                    Entities.identityUser _user = _user = _membershipService.CreateUser(user.Username, user.Firstname, user.Email, user.Password, new int[] { primeiroUsuario }, user.usuario_logado);

                    if (_user != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }

                return response;
            });
        }

        [Route("details/{username}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var repositorio = _membershipService.GetUserUsername(username);

                UserEditViewModel genericVM = Mapper.Map<identityUser, UserEditViewModel>(repositorio);
                response = request.CreateResponse<UserEditViewModel>(HttpStatusCode.OK, genericVM);

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, UserEditViewModel genericVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Entities.identityUser _genericEntity = _membershipService.UpdateUser(genericVM.ID, genericVM.Username, genericVM.Firstname, genericVM.Email, genericVM.password, genericVM.newpassword_n, null, genericVM.usuario_logado);

                    if (_genericEntity != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }
                return response;
            });
        }
        [Route("userRoles/{username}")]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userRoles = _membershipService.GetUserRoles(username);

                IEnumerable<UserRoleViewModel> genericVM = Mapper.Map<IEnumerable<identityRole>, IEnumerable<UserRoleViewModel>>(userRoles);

                response = request.CreateResponse<IEnumerable<UserRoleViewModel>>(HttpStatusCode.OK, genericVM);

                return response;

            });
        }

        [Authorize]
        [Route("roles/{username}")]
        [HttpGet]
        public HttpResponseMessage BuscaRoles(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userRoles = _membershipService.GetAllRoles();

                List<identityRole> usernameRole = _membershipService.GetUserRoles(username).ToList();

                int menorRole = usernameRole.Select(r => r.ID).Min();

                userRoles.RemoveAll(s => s.ID < menorRole);

                IEnumerable<RoleViewModel> genericVM = Mapper.Map<IEnumerable<identityRole>, IEnumerable<RoleViewModel>>(userRoles);

                response = request.CreateResponse<IEnumerable<RoleViewModel>>(HttpStatusCode.OK, genericVM);

                return response;

            });
        }

        [AllowAnonymous]
        [Route("existe")]
        [HttpGet]
        public HttpResponseMessage verificarExiste(HttpRequestMessage request, string tipo, string valor)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                bool resp = _membershipService.VerificarExistente(tipo, valor);

                if (resp)                    
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = true, tipo = tipo });                    
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false, tipo = tipo });                

                return response;
            });
        }


        [AllowAnonymous]
        [Route("followzup")]
        [HttpGet]
        public HttpResponseMessage followzup(HttpRequestMessage request, string tipo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;



                //FollowZup
                followZup fl = new followZup();
                fzup_param mensagem = new fzup_param();

                //////chck
                ////mensagem.FZUP_COMMAND = "chck";
                ////mensagem.FZUP_LASTSEQ = 0;
                ////mensagem.FZUP_USER = "zapypdn676a6";
                ////mensagem.FZUP_SUBSCODE = "19425194";

                ////smsg
                mensagem.FZUP_COMMAND = "smsg";
                mensagem.FZUP_LASTSEQ = 13;
                mensagem.FZUP_USER = "zapzhpp2max6";
                mensagem.FZUP_SUBSCODE = "93519265";
                mensagem.FZUP_HOURS = 1;
                mensagem.FZUP_MSGTEXT = "Texto para encriptar";
                mensagem.FZUP_MSGURL = "http://marcelo.linkpc.net:8188/basico/logintoken?token=token";

                string[] resposta = fl.submit(mensagem);

                //return request.CreateResponse(HttpStatusCode.BadRequest, new { modelos_vm = false });
                ////FollowZup

                response = request.CreateResponse(HttpStatusCode.OK, new { resposta = resposta });

                return response;
            });
        }
		
		
        [EnableCors(origins: "*", headers: "*", methods: "*")] //followup to somee
        [AllowAnonymous]
        [Route("redirectfrom")]
        [HttpPost]
        public HttpResponseMessage RedirectFrom(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string origin = "";
                if (Request.Headers.Contains("Origin"))
                {
                    var values = Request.Headers.GetValues("Origin");
                    origin = values.FirstOrDefault();
                }

                string request_string = request.Content.ReadAsStringAsync().Result;
                string channel_string = HttpUtility.ParseQueryString(request_string).Get("fzupidchannel");
                string encrypt_string = HttpUtility.ParseQueryString(request_string).Get("fzupresponse");

                RedirectTo(origin, channel_string, encrypt_string);

                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                return response;
            });
        }
		
		public void RedirectTo(string origin, string channel, string encrypted)
        {
            Hub.Clients.All.mostarToast("Origin: " + origin);


                //RestSharp
                IRestResponse response = null;
                var client = new RestClient("http://marcelo.linkpc.net:8188/billet/api/followzup");
                var RestSharpRequest = new RestRequest("/redirectfrom", Method.POST);
                RestSharpRequest.AddHeader("Origin", HttpContext.Current.Request.Url.Host.ToString());
                RestSharpRequest.AddParameter("fzupidchannel", channel);
                RestSharpRequest.AddParameter("fzupresponse", encrypted);
                response = client.Execute(RestSharpRequest);

        }

        [EnableCors(origins: "*", headers: "*", methods: "*")] //http://www.followzup.com
        [AllowAnonymous]
        [Route("followzupapi")]
        [HttpPost]
        public HttpResponseMessage followzupapi(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ////Response
                //string request_string = request.Content.ReadAsStringAsync().Result;
                //string channel_string = HttpUtility.ParseQueryString(request_string).Get("fzupidchannel");
                //string encrypt_string = HttpUtility.ParseQueryString(request_string).Get("fzupresponse");

                //encrypt_string = "fA%2BrXvU%2BvJDs%2BA7ip1h2gIbn%2BoJwA%2FGqDJcoJx8a3Q7zoPc1cdw3MqTytQx7%2B4sf3qYbgs8Sj6HEDsKeG5BIbYeymWjEXKgzARRSW9tWNcX5zmrxmwPYs1WVJ0rPbu%2Boh8bxmZmtxEB2THKZbrCyLLqI4jV6566GXGqULrIK4WL%2Bnjr2eR%2Bsr3oe67uaNKUFizEwZmLgVvuLDTjSden1V%2BNgXkU324DjT23wcGm8YMRobnxU6a8esoLHZHck3KpifUdbgp6OYQTPGaVXU0QeTiq4rDNMrexaDHI1HyYlNAykwRb1pk01uZJYxRjqwsZ511T33I%2BNTmCOpKtk%2BL3V5Q%3D%3D";

                ////FollowZup
                //followZup fl = new followZup();
                //string usermessage = fl.decrypt(encrypt_string);

                string usermessage = "pingou";


                Hub.Clients.All.mostarToast(usermessage);

                response = request.CreateResponse(HttpStatusCode.OK, new { success = true, resp = usermessage});

                return response;
            });
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")] //http://www.followzup.com
        [AllowAnonymous]
        [Route("followzupapiget")]
        [HttpGet]
        public HttpResponseMessage followzupapiget(HttpRequestMessage request, string fzupidchannel, string fzupresponse)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                
                string jsonContent = "Recebi GET: " + fzupidchannel + " | " + fzupresponse;

                Hub.Clients.All.mostarToast(jsonContent);


                response = request.CreateResponse(HttpStatusCode.OK, new { success = true, resp = jsonContent });

                return response;
            });
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")] //http://www.followzup.com
        [AllowAnonymous]
        [Route("decrypt")]
        [HttpPost]
        public HttpResponseMessage Decrypt(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string encrypted = "fA%2BrXvU%2BvJDs%2BA7ip1h2gIbn%2BoJwA%2FGqDJcoJx8a3Q7zoPc1cdw3MqTytQx7%2B4sf3qYbgs8Sj6HEDsKeG5BIbYeymWjEXKgzARRSW9tWNcX5zmrxmwPYs1WVJ0rPbu%2Boh8bxmZmtxEB2THKZbrCyLLqI4jV6566GXGqULrIK4WL%2Bnjr2eR%2Bsr3oe67uaNKUFizEwZmLgVvuLDTjSden1V%2BNgXkU324DjT23wcGm8YMRobnxU6a8esoLHZHck3KpifUdbgp6OYQTPGaVXU0QeTiq4rDNMrexaDHI1HyYlNAykwRb1pk01uZJYxRjqwsZ511T33I%2BNTmCOpKtk%2BL3V5Q%3D%3D";

                //FollowZup
                followZup fl = new followZup();

                string decriptado = fl.decrypt(encrypted);

                response = request.CreateResponse(HttpStatusCode.OK, new { success = true, decriptado = decriptado });
                return response;
            });
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")] //http://www.followzup.com
        [AllowAnonymous]
        [Route("pagseguronotificacoes")]
        [HttpPost]
        public HttpResponseMessage PagSeguroNotificacoes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

                try
                {
                    string request_string = request.Content.ReadAsStringAsync().Result;

                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/_pagSeguroInterface/");
                    string file = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss_fff") + "_notif.txt";
                    File.WriteAllText(path + file, request_string);

                    //request_string
                    string novo = request_string.Replace("notificationCode=", "").Replace("notificationType=", "");
                    string[] split = novo.Split('&');

                    //RestSharp
                    IRestResponse r_response = null;
                    var client = new RestClient("http://marcelo.linkpc.net:8188/billet/api/financeiro");
                    var RestSharpRequest = new RestRequest("/pagseguronotificacoes", Method.POST);
                    RestSharpRequest.AddHeader("Origin", HttpContext.Current.Request.Url.Host.ToString());
                    RestSharpRequest.AddParameter("notificationCode", split[0]);
                    RestSharpRequest.AddParameter("notificationType", split[1]);
                    r_response = client.Execute(RestSharpRequest);

                    response = request.CreateResponse(HttpStatusCode.OK, file);
                }
                catch (Exception ex)
                {
                    try
                    {
                        Basico.Data.BasicoContext ctx = new Data.BasicoContext();
                        Error newError = new Error();
                        newError = new Error();
                        newError.Message = "PagSeguroNotificacoes: " + ex.Message;
                        newError.StackTrace = ex.StackTrace;
                        newError.DateCreated = DateTime.UtcNow;
                        ctx.set_Error.Add(newError);
                        ctx.Dispose();

                        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    }
                    catch (Exception exd)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, exd.Message);
                    }
                }

                return response;
            });
        }


         
        [AllowAnonymous]
        [Route("zipfy")]
        [HttpPost]
        public HttpResponseMessage zipfy(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                try
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/_pagSeguroInterface/");
                    string path_dest = System.Web.Hosting.HostingEnvironment.MapPath("~/_inbox/");

                    string[] lista = Directory.GetFiles(path_dest);
                    foreach (string file in lista.ToList())
                    { 
                        if (File.GetCreationTimeUtc(file).AddMinutes(20) < DateTime.UtcNow)
                        {
                            File.Delete(file);
                        }
                    }


                    var zipName = "teste_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss_fff") + ".zip";
                    using (ZipArchive newFile = ZipFile.Open(path_dest + zipName, ZipArchiveMode.Create))
                    {
                        //Here are two hard-coded files that we will be adding to the zip
                        //file.  If you don't have these files in your system, this will
                        //fail.  Either create them or change the file names.
                        newFile.CreateEntryFromFile(path + "_noimage.txt", "_noimage.txt", CompressionLevel.NoCompression);
                        newFile.CreateEntryFromFile(path + "20170817_152208_158_notif.txt", "20170817_152208_158_notif.txt", CompressionLevel.NoCompression);
                    }


                    response = request.CreateResponse(HttpStatusCode.OK, zipName);
                }
                catch
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "");
                }

                return response;
            });
        }

    }
}
