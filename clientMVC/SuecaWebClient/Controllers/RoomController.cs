using SuecaWebClient.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
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
        public bool SendReady(string playerToken, bool state)
        {
            try
            {
                SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
                serviceHelper.sendClientReady(playerToken, state);
                return true;
            }
            catch (Exception e)
            {
            }

            return false;


        }


        [HttpPost]
        [CustomHandleErrorAttribute]
        public ActionResult GetRoomState(string roomId = "", string playerToken = "")
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            if (roomId != "" && playerToken != "")
            {
                return Json(serviceHelper.getRoomState(roomId, playerToken));    
            }
            //throw new HttpException(404, "Bad Room");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content("Pas de room", MediaTypeNames.Text.Plain); ;
        }
        public class CustomHandleErrorAttribute : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext filterContext)
            {
                var exception = filterContext.Exception;
                var statusCode = new HttpException(null, exception).GetHttpCode();

                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet, //Not necessary for this example
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = statusCode;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }

        public ActionResult Join(string roomId = "", string password = "")
        {
            if(roomId != "")
            { 
                SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
                ViewData["playerTokenServer"] = serviceHelper.joinRoom(roomId, password);
                ViewData["connected"] = true;
                //ViewData["playerTokenServer"] = roomInfo.playerToken;
            }
            else
            {
                ViewData["playerTokenServer"] = "";
                ViewData["connected"] = false;
            }
            return View();
        }

    }
}
