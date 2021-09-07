using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace MUNVoter.Hubs
{
    [HubName("SessionHub")]
    public class SessionHub:Hub
    {
        static SessionHub()
        {

        }
       public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SessionHub>();
            context.Clients.All.refreshSessionPage();
           
            
        }

        public static void BroadCastDataSpesfic(int sessionId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SessionHub>();
            context.Clients.All.refreshSessionPage(sessionId);

            //List<string> test = new List<string>();
            //context.Clients.Clients(test).refreshSessionPage(sessionId);
            SendMessage(sessionId.ToString());
        }


        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public static void SendMessage(string user)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SessionHub>();
            context.Clients.Group(user).refreshSessionPage(user);
        }

    }
}