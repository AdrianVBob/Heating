using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace API.Controllers
{
    public class SetMaxTemperatureController : ApiController
    {
        // GET: SetMaxTemperature

        private HeatingDbEntities db = new HeatingDbEntities();

        [ActionName("SetMaxTemperature")]
        [HttpGet]

        public IHttpActionResult SetMaxTemperature(int userId, float temperature)
        {
            try
            {
                db.SetMaxTemperatures.Add(new SetMaxTemperature
                {
                    UserId = userId,
                    MaxTemperature = temperature
                });
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Erroare server."); // Inseamna ca ceva sa intamplat pe server
            }
            return Ok(1);
        }        
    }
}