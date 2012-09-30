using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NiCris.Web.Extensions
{
    public class JsonParameterAttribute : ActionFilterAttribute
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public Type DataType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string json = filterContext.HttpContext.Request.Params[this.Input].ToString();
            filterContext.ActionParameters[this.Output] = new JavaScriptSerializer().Deserialize(json, this.DataType);
        }
    }

    public class JsonParameter2Attribute : ActionFilterAttribute
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public Type DataType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string json = filterContext.HttpContext.Request.Params[this.Input].ToString();
            filterContext.ActionParameters[this.Output] = new JavaScriptSerializer().Deserialize(json, this.DataType);
        }
    }
}