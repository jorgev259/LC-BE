using System;
using System.Web;
using System.Web.Http;
using linkscloud.Models;
using System.Web.Script.Serialization;

namespace linkscloud.Controllers
{
    public class linkController : Controller
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
                    res.new_link();
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