using System;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class BoardController : ApiController
    {
        private HeatingDbEntities db = new HeatingDbEntities();

        public IHttpActionResult VerifyAndAddTempertureForBoard(int brdId, double temperature)
        {
            // 1.vreau sa verific daca board id-ul exista
            var board = db.Boards.FirstOrDefault(_ => _.Id.Equals(brdId));
            if (board == null)
            {
                return BadRequest("Incorrect board."); // Inseamna ca nu exista acel boardId
            }

            // 2.vreau sa adaug valoarea temperaturii in baza de date pentru boardId-ul respectiv. 
            try
            {
                db.BoardTemperatures.Add(new BoardTemperature
                {
                    IdBoard = board.Id,
                    TemperatureValue = temperature,
                    Date = DateTime.Now
                });
                db.SaveChanges();
            }
            catch
            {
                return Ok("Erroare server."); // Inseamna ca cava sa intamplat pe server
            }

            // 3.vreau sa verific daca trebuie pornita sau nu caldura
            if (temperature < board.MaxTemperature)
            {
                return Ok(1); // Inseamna ca trebuie pornita calduara
            }

            return Ok(0); // Inseamna ca nu trebuie pornita calduara
        }
    }
}
