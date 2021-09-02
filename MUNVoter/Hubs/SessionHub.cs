using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MUNVoter.Hubs
{
    [HubName("SessionHub")]
    public class SessionHub:Hub
    {
        public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SessionHub>();
            context.Clients.All.refreshSessionPage();
        }
    }
}