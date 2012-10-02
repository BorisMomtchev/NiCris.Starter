using System;
using SignalR.Client.Hubs;
using System.Net;

namespace NiCris.Web.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConn = new HubConnection("http://localhost:7458/");
            hubConn.Credentials = CredentialCache.DefaultNetworkCredentials;

            var msg = new NiCris.Web.ConsoleTester.Models.Message();
            msg.Content = "Moo";
            msg.Duration = 500;
            msg.Title = "Hello Title";

            var hub = hubConn.CreateProxy("messenger");
            hubConn.Start().Wait();
            
            Object[] myData = { (Object)msg, "sourcing" };
            hub.Invoke("broadCastMessage", myData);
        }
    }
}

// http://blogs.msdn.com/b/webdev/archive/2012/08/22/announcing-the-release-of-signalr-0-5-3.aspx
// https://github.com/SignalR/SignalR/wiki

// http://stackoverflow.com/questions/9722983/signalr-secure-connection-between-net-client-and-the-server
// http://www.hanselman.com/blog/AsynchronousScalableWebApplicationsWithRealtimePersistentLongrunningConnectionsWithSignalR.aspx
// http://www.entechsolutions.com/browser-alerts-with-asp-net-4-5-and-signalr

// http://www.iwantmymvc.com/mvc-3-signalr-knockout-real-time-notifications