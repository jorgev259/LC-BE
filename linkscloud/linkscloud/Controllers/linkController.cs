using System;
using System.Web;
using System.Web.Http;
using linkscloud.Models;
using System.Web.Script.Serialization;

namespace linkscloud.Controllers
{
    public class linkController : ApiController
    {
        [HttpPost]
        public String Post()
        {
            var Request = HttpContext.Current.Request;
            var response = "";

            switch (Request["proc"])
            {
                case "new":
                    link res = new link(Convert.ToInt32(Request["id_owner"]), Request["title"], Request["url"], Request["desc"]);
                    link.new_link(res);
                    response = new JavaScriptSerializer().Serialize(res);
                    break;

                default:
                    response = "Unknown Request";
                    break;
            }

            return response;
        }
    }
}