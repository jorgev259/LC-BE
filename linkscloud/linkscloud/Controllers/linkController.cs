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
                    link link = new link(Convert.ToInt32(Request["id_owner"]), Request["title"], Request["url"], Request["desc"]);
                    response = link.new_link(link);
                    break;

                default:
                    response = "Unknown Request";
                    break;
            }

            return response;
        }
    }
}