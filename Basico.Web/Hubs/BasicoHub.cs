using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using SignalR;
//using SignalR.Hubs;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Basico.Web.Hubs
{
    [HubName("BasicoHub")]
    public class BasicoHub : Hub //, IConnected, IDisconnect
    {
        public void SendMessage(string name, string message)
        {
            Clients.All.broadcastMessage(Context.ConnectionId, message);
            //System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
        }

        public void SendMostar(string toast_text)
        {
            Clients.All.mostarToast(toast_text);
        }
    }
}
