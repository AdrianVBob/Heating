using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class CreateController : ApiController
    {
        private HeatingDbEntities db = new HeatingDbEntities();


        public int Create(string user, string pass, int brdId)
        {
            var brdIdVer = db.BoardIds.Where(u => u.board_id == brdId);

            if (brdIdVer.ToString().Equals(brdId.ToString()))
            {

                User us = new User();
                {
                    us.username = user;
                    us.password = pass;
                    us.brd_id = brdId;

                }

                db.Users.Add(us);
                db.SaveChanges();

                return 1;
            }
            else return 0;
        }

       



    
}
}
