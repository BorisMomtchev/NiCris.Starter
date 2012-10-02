using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiCris.Web.Hubs
{
    public class Message
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Duration { get; set; }
    }
}