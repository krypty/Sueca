using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuecaWebClient.Controllers
{
    public class RoomController : Controller
    {
        //
        // GET: /Room/

        [HttpPost]
        public ActionResult getGameState(string roomId, string playerToken)
        {
            return Content("It works " + roomId + " | " + playerToken);
        }


        public ActionResult Join(string roomId)
        {
            return View();
        }

    }
}
