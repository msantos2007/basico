using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basico.Entities;
using RestSharp;

namespace Basico.Services
{
    public interface ICommunicationService
    {
        string GetRandomHexNumber(int digits);
        //bool SendEmailGoogle(_billetmail billetmail);
        //IRestResponse SendSMSGateway(_billetsmsgateway billetsmsgateway);

        //Byte[] PDFSharpConverter(string html);

        void LogarErro();

        bool jobEmailSet();        
        bool jobEmailGet();
        bool jobEmailPst();
        bool jobEmailDel(); 
    }
}
