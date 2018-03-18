using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private HeatingDbEntities db = new HeatingDbEntities();

        [ActionName("Login")]
        [HttpGet]
        public IHttpActionResult Login(string username, string password)
        {
            return db.Users.FirstOrDefault(u => u.UserName.Equals(username) && u.Password.Equals(password)) != null ? Ok("0") : Ok("1");
        }
    }
}
