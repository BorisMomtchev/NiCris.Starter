using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiCris.Web.ViewModels
{
    public class PanelData
    {
        public string text { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public IEnumerable<PanelData> items { get; set; }
    }
}