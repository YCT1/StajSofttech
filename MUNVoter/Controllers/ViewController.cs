using Microsoft.AspNet.SignalR;
using MUNVoter.Hubs;
using MUNVoter.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MUNVoter.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        [Route("View/IndexOLD/{sessionID?}")]
        public ActionResult Index(int sessionID)
        {
            var db = new UnitOfWork(new DatabaseContext());
            if (!db.Sessions.isSessionExsist(sessionID))
            {
                return Content("<h2>The session does not exsist</h2>");
            }
            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            List<Motion> motions = db.Motions.GetMotionsBySessionId(sessionID).ToList();
            ViewBag.countryImg = db.CountryFlags.FindImageAddressesByMotions(motions);
            return View(motions);
        }

        [Route("View/Index/{sessionID?}")]
        public ActionResult Index2(string sessionID)
        {
            var db = new UnitOfWork(new DatabaseContext());
            int sessionIDINT;
            if (!int.TryParse(sessionID, out sessionIDINT))
            {
                return Redirect("/");
            }
            
            ViewBag.SessionID = sessionIDINT;
            Session thisSession = db.Sessions.findSessionById(sessionIDINT);
            ViewBag.ConferenceName = thisSession.ConferenceName;
            ViewBag.ComitteeName = thisSession.CommitteeName;
            ViewBag.Title ="View Only: " + thisSession.ConferenceName + ", " + thisSession.CommitteeName;
            return View();
        }
        
        public ActionResult RenderMotionListUser(int sessionID)
        {
            var db = new UnitOfWork(new DatabaseContext());
            List<Motion> motions = db.Motions.GetMotionsBySessionId(sessionID).ToList();
            if (!db.Sessions.isSessionExsist(sessionID))
            {
                return Content("<h2>The session does not exsist</h2>");
            }
            ViewBag.motionNumber = motions.Count();
            ViewBag.SessionID = sessionID;
           
            ViewBag.countryImg = db.CountryFlags.FindImageAddressesByMotions(motions);
            return PartialView("_Motions", motions);
        }

        [Route("View/Projection_old_way_to_do/{sessionID?}")]
        public ActionResult ProjectionView(int sessionID)
        {
            var db = new UnitOfWork(new DatabaseContext());
            if (!db.Sessions.isSessionExsist(sessionID))
            {
                return Content("<h2>The session does not exsist</h2>");
            }

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            List<Motion> motions = db.Motions.GetMotionsBySessionId(sessionID).ToList();
            ViewBag.countryImg = db.CountryFlags.FindImageAddressesByMotions(motions);

            // Experimental
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SessionHub>();
            
            return View(motions);
        }
        [HttpGet]
        public JsonResult GetMotions(int data)
        {
            var db = new UnitOfWork(new DatabaseContext());
            List<Motion> motions = db.Motions.GetMotionsBySessionId(data).ToList();
            return Json(motions, JsonRequestBehavior.AllowGet);
        }

        [Route("View/Projection/{sessionID?}")]
        public ActionResult ProjectionViewAjax(string sessionID)
        {
            int sessionIDINT;
            if (!int.TryParse(sessionID, out sessionIDINT))
            {
                return Redirect("/");
            }
            ViewBag.SessionID = sessionIDINT;
            ViewBag.Title = "Projection View";
            return View();
        }

        public ActionResult RenderMotionList(int sessionID)
        {
            var db = new UnitOfWork(new DatabaseContext());
            
            if (!db.Sessions.isSessionExsist(sessionID))
            {
                return Content("<h2>The session does not exsist</h2>");
            }

            List<Motion> motions = db.Motions.GetMotionsBySessionId(sessionID).ToList();
            ViewBag.motionNumber = motions.Count();
            ViewBag.SessionID = sessionID;

            Session thisSession = db.Sessions.findSessionById(sessionID);
            ViewBag.ConferenceName = thisSession.ConferenceName;
            ViewBag.ComitteeName = thisSession.CommitteeName;
            ViewBag.countryImg = db.CountryFlags.FindImageAddressesByMotions(motions);
            return PartialView("_ProjectionMotions", motions);
        }
    }
}