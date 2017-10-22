using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Basico.Data;
using Basico.Data.Extensions;
using Basico.Data.Infrastructure;
using Basico.Data.Repositories;
using Basico.Entities;
using System.Security.Principal;

using System.Net.Mail; 


namespace Basico.Services
{
    //?? 1. Registrar em AutofacWebapiConfig
    public class CommunicationService : ICommunicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityBaseRepository<Error> _repo_error;

        public CommunicationService(IUnitOfWork unitOfWork, IEntityBaseRepository<Error> repo_error)
        {
            _repo_error = repo_error;
            _unitOfWork = unitOfWork;
        }

        public void LogarErro()
        {
            Error newError = new Error();

            newError.Message = "Quartz: Gravando Info";
            newError.StackTrace = "Execute";
            newError.DateCreated = DateTime.UtcNow;
            _repo_error.Add(newError);
            _unitOfWork.Commit();
        }


        protected Random random = new Random();

        public string GetRandomHexNumber(int digits)
        {
            //Random random = new Random();
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        //public IRestResponse SendSMSGateway(_billetsmsgateway billetsmsgateway)
        //{
        //    IRestResponse response = null;
        //    //?? IMPLEMENT - Checar se o device está pronto pra enviar sms:  "http:/ /smsgateway.me/api/v3/devices/view/[id]"
        //    if (billetsmsgateway.destinations.Count() == 0)
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        var client = new RestClient("http://smsgateway.me/api/v3/");
        //        var request = new RestRequest("messages/send", Method.POST);
        //        string recipients = "";

        //        request.AddParameter("email", billetsmsgateway.login_svc_user);
        //        request.AddParameter("password", billetsmsgateway.login_svc_pwrd);
        //        request.AddParameter("device", billetsmsgateway.login_svc_device);
        //        foreach (_billetsmsgateway.smsdestination destination in billetsmsgateway.destinations)
        //        {
        //            recipients = recipients + "," + "+55" + destination.dest_phone;
        //        }
        //        recipients = recipients.Substring(1).ToString();
        //        request.AddParameter("number", recipients);
        //        request.AddParameter("message", billetsmsgateway.message);
        //        response = client.Execute(request);
        //        return response;
        //    }
        //}

        //public bool SendEmailGoogle(_billetmail billetmail) //(string login_email, string login_pw, string from_email, string replyTo_email, string[] to_email, string[] cc_email, string[] cco_email, string subject, string message)
        //{
        //    MailMessage msg = new MailMessage();

        //    msg.From = new MailAddress(billetmail.from_email.address, billetmail.from_email.sender);
        //    msg.ReplyToList.Add(new MailAddress(billetmail.replyTo.address, billetmail.replyTo.sender));
        //    foreach (_billetmail.email m in billetmail.to_email) { msg.To.Add(new MailAddress(m.address, m.sender)); }
        //    foreach (_billetmail.email m in billetmail.cc_email) { msg.CC.Add(new MailAddress(m.address, m.sender)); }
        //    foreach (_billetmail.email m in billetmail.cco_email) { msg.Bcc.Add(new MailAddress(m.address, m.sender)); }
        //    msg.Subject = billetmail.subject;
        //    msg.Body = billetmail.message;
        //    msg.SubjectEncoding = System.Text.Encoding.UTF8;
        //    msg.BodyEncoding = System.Text.Encoding.UTF8;

        //    //msg.IsBodyHtml = (!billetmail.isHTML ? false : billetmail.isHTML);
        //    foreach (AlternateView alt in billetmail.messageAlternateViewCollection) { alt.ContentType.CharSet = Encoding.UTF8.WebName; msg.AlternateViews.Add(alt); }

        //    SmtpClient client = new SmtpClient();
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    client.EnableSsl = true;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;

        //    client.Credentials = new System.Net.NetworkCredential(billetmail.login_svc_user, billetmail.login_svc_pwrd);
        //    client.Send(msg);

        //    return true;
        //}
        
        //public Byte[] PDFSharpConverter(string html)
        //{
        //    Byte[] res = null;
        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //        pdf.Save(ms);
        //        res = ms.ToArray();
        //    }
        //    return res;
        //}

        public bool jobEmailSet()
        {
            bool response = false;
            return response;
        }
        public bool jobEmailGet()
        {
            bool response = false;




            return response;
        }
        public bool jobEmailPst()
        {
            bool response = false;




            return response;
        }
        public bool jobEmailDel()
        {
            bool response = false;




            return response;
        }

    }
}
