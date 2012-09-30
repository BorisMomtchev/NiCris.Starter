using SignalR.Hubs;

namespace NiCris.Web.Hubs
{
    public class TaskHub : Hub
    {
        public void SendMessage(string clientName, string message)
        {
            Clients.GetMessage(clientName, message);
        }
    }
}