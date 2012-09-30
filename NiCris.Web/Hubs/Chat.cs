using Newtonsoft.Json;
using NiCris.BusinessObjects;
using SignalR;
using SignalR.Hubs;

namespace NiCris.Web.Hubs
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message);
        }
    }

    public class Notifier
    {
        public static void Send(BizMsg bizMsg)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<Chat>();
            string json = JsonConvert.SerializeObject(bizMsg);
            context.Clients.addMessageEx(json);
        }
    }
}