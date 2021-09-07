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
        [Route("View/Index/{sessionID?}")]
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

        

        [Route("View/Projection/{sessionID?}")]
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
    }
}