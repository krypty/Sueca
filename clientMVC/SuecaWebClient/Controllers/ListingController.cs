using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SuecaWebClient.Models;
using SuecaWebClient.SuecaService;
using SuecaWebClient.Helper;

namespace SuecaWebClient.Controllers
{


    


    public class ListingController : Controller
    {


       
        SuecaServiceHelper serviceHelper;




        //
        // GET: /Listing/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRoom()
        {
            return View();
        }

        [HttpGet]  
        public ActionResult RoomList()
        {
            List<RoomListViewModel> roomViewList = new List<RoomListViewModel>();
            serviceHelper = new SuecaServiceHelper();
            
            Room[] roomList = serviceHelper.getRoomsArray();
            if (roomList != null)
            {
                foreach (Room r in roomList)
                {
                    
                    roomViewList.Add(new RoomListViewModel(r.Name, true));
                }
            }
            else
            {
               
            }

            return View(roomViewList.AsEnumerable<RoomListViewModel>());
        }


        //For ajax request
        [HttpGet]  
        public ActionResult getNewRoom()
        {
            try
            {
                serviceHelper = new SuecaServiceHelper();
                string roomId = serviceHelper.createRoom();
                
                return Json(roomId, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(401, "Custom Error Message 1" + e);
            }
            

            
        }

        

    }

    
    
}
