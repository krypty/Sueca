﻿using System;
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
            List<RoomViewModel> roomViewList = new List<RoomViewModel>();
            serviceHelper = new SuecaServiceHelper();
            
            Room[] roomList = serviceHelper.getRoomsArray();
            if (roomList != null)
            {
                foreach (Room r in roomList)
                {
                    roomViewList.Add(new RoomViewModel(r.Name, r.Password != ""));
                }
            }
            else
            {
               
            }

            return View(roomViewList.AsEnumerable<RoomViewModel>());
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
