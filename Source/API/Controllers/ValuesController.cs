using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ValuesController : ApiController
    {
       private HeatingDbEntities db = new HeatingDbEntities();

       


        private bool AddData(int brdId, decimal td)
        {
            value myvalue = new value();
            {
                myvalue.temperature = td;
                myvalue.board_id = brdId;
                
            }
            try
            {
                db.values.Add(myvalue);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception information: {0}", e);
                return false;
            }
        }


        private bool VerifyCurrentTemp()
        {


            var tv = db.values.OrderByDescending(u => u.temperature).Last();
            var set = db.SetTemperatures.OrderByDescending(u => u.setTemperature1).Last();
         
            if (tv.temperature >= set.setTemperature1)
            {
                return false;
            }

            else return true;
            

           
        }
        private bool SetTemperature(decimal setTemp)
        {
            SetTemperature set = new SetTemperature();

            try
            {
                set.setTemperature1 = setTemp;



                db.SetTemperatures.Add(set);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception information: {0}", e);
                return false;
            }
        }

        public IHttpActionResult Values(int brdId, decimal temperature)
        {
            bool addRes = AddData(brdId, temperature);
            bool setRes = SetTemperature(temperature);
            bool verRes = VerifyCurrentTemp();
            if (addRes == true && setRes == true && verRes == true)

                return Ok("1");

            else if (addRes == true && setRes == true && verRes == false)
            {
                

                return Ok("0");
            }

            return Ok("2");
        }
    }
}
