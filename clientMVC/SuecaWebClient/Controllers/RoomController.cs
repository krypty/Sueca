using SuecaWebClient.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuecaWebClient.Models;

namespace SuecaWebClient.Controllers
{
    public class RoomController : Controller
    {
        //
        // GET: /Room/

        [HttpPost]
        public ActionResult GetGameState(string roomId, string playerToken)
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            //RoomInfoModel values = new RoomInfoModel(roomId, playerToken);
            //serviceHelper.

            return Content("It works " + roomId + " | " + playerToken);
        }

        [HttpPost]
        public ActionResult GetRoomState(string roomId, string playerToken)
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();

            
            return Json(serviceHelper.getRoomState(roomId,playerToken));
        }


        public ActionResult Join(string roomId, string password = "")
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            ViewData["playerTokenServer"] = serviceHelper.joinRoom(roomId, password);
            //ViewData["playerTokenServer"] = roomInfo.playerToken;
            return View();
        }

    }
}
