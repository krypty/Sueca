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
        public JsonResult GetGameState(string roomId, string playerToken)
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            //RoomInfoModel values = new RoomInfoModel(roomId, playerToken);

            GameInfoModel model = serviceHelper.GetGameState(roomId, playerToken);
            serviceHelper.close();
            return Json(model);
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
        public void PlayCard(string color, string value, string playerToken, string roomId)
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            serviceHelper.playCard(playerToken, color, value);
            serviceHelper.close();

        }


        [HttpPost]
        [CustomHandleErrorAttribute]
        public ActionResult GetRoomState(string roomId = "", string playerToken = "")
        {
            SuecaServiceHelper serviceHelper = new SuecaServiceHelper();
            if (roomId != "" && playerToken != "")
            {
                RoomInfoModel model = serviceHelper.getRoomState(roomId, playerToken);
                serviceHelper.close();
                return Json(model);    
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
                serviceHelper.close();
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
