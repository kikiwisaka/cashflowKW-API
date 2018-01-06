using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KW.Presentation.WebAPI.Controllers
{
    //[Authorize]
    public class BaseAPIController : ApiController
    {
        public string UserId
        {
            get
            {
                var owinContext = HttpContext.Current.GetOwinContext();

                var userId = owinContext.Authentication.User.Claims.Where(x => x.Type == "userId").FirstOrDefault();
                if (userId != null)
                    return userId.Value;
                return string.Empty;
            }
        }
        public string UserName
        {
            get
            {
                var owinContext = HttpContext.Current.GetOwinContext();

                var username = owinContext.Authentication.User.Claims.Where(x => x.Type == "username").FirstOrDefault();
                if (username != null)
                    return username.Value;
                return string.Empty;
            }
        }
    }
}