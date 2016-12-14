using System;
using System.Web;
using System.Web.Http;
using linkscloud.Models;
using System.Web.Script.Serialization;

namespace linkscloud.Controllers
{
    public class userController : ApiController
    {
        [HttpPost]
        public String Post()
        {
            var Request = HttpContext.Current.Request;
            var response = "";

            switch (Request["proc"])
            {
                case "register":
                    response = user.add_user(Request["username"], Request["email"], Request["passkey"]);
                    break;

                case "info":
                    user result = user.info_user(Request["criteria"], Request["identifier"]);
                    result._password = null;
                    response = new JavaScriptSerializer().Serialize(result);
                    break;

                case "login":
                    //user data = user.info_user("username", Request["username"]);
                    //response= new JavaScriptSerializer().Serialize(data);

                    //if (Request["password"] == data._password)
                    //{ 
                    //    response.Insert(0,"true");
                    //}else
                    //{
                    //    response.Insert(0, "false");

                    response = user.login(Request["username"],Request["pass"]);

                    break;

                default:
                    response = "Unknown Request";
                    break;
            }

            return response;
        }
    }
}
