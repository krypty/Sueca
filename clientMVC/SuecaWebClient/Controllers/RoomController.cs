using SuecaWebClient.Helper;
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
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();

            //serviceHelper.

            return Content("It works " + roomId + " | " + playerToken);
        }


        public ActionResult Join(string roomId, string password)
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            serviceHelper.joinRoom(roomId, password);
            return View();
        }

        public ActionResult Join(string roomId)
        {
            return Join(roomId, "");
        }

    }
}
