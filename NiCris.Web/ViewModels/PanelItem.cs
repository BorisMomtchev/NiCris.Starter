using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace NiCris.Web.ViewModels
{
    public class PanelItem
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
    }

    public class PanelItemEx
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
    }
}