using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class LoginController : ApiController
    {

       private HeatingDbEntities db = new HeatingDbEntities();

        
        
        public IHttpActionResult Login(string user, string pass)
        {
            
            if (check(user, pass) == true)
            {
                return Ok("1");
            }
            return Ok("0");
        }

        private bool check(string user, string password)
        {
            var dataFromUser = db.Users.Where(u => u.username == user && u.password == u.password).FirstOrDefault();

            if (dataFromUser != null && user == dataFromUser.username && dataFromUser.password== password)
            {

                return true;
            }
            else
            {

                return false;
            }
        }
        



    }
}
