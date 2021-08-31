using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MUNVoter.Models;
using Microsoft.AspNet.Identity;
namespace MUNVoter.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
       
        // GET: Home
        
        [Route("{sessionIDParameter?}")]
        [Route("Home/{sessionIDParameter?}")]
        public ActionResult Index(string sessionIDParameter)
        {
            
            if (string.IsNullOrWhiteSpace(sessionIDParameter) || sessionIDParameter=="Index")
            {
                //return Content("<h1>This is a Register Page</h1><h2>It is under construction</h2>");
                
                return Redirect("session/index");
            }
            int sessionID = int.Parse(sessionIDParameter);
            // Old one
            /*
            ViewBag.motionNumber = Database.Motions.Count;
            DatabaseContext db = new DatabaseContext();
           
            return View(Database.Motions);
            */

            //int sessionID = 0;
            var db = new UnitOfWork(new DatabaseContext());

            List<Motion> _list = new List<Motion>()
            {
                new Motion(){title="Discussing Women Rights in Afganistan" , type="Moderated", sponsorCountry="Turkey", totalTime=10, indTime=60,SessionId=0},
                new Motion(){title="Liberation of South Sudan" , type="Moderated", sponsorCountry="France", totalTime=11, indTime=60,SessionId=0},
                new Motion(){title="Decline of life expectance" , type="Moderated", sponsorCountry="United Kingdom", totalTime=10, indTime=90,SessionId=1},
                new Motion(){title="Working on the Draft Resolution" , type="Unmoderated", sponsorCountry="USA", totalTime=10, indTime=0,SessionId=0},
                new Motion(){title="Prev Motion" , type="Extention", sponsorCountry="Tuvalu", totalTime=5, indTime=60,SessionId=0},
                new Motion(){title="EU Relation with Taliban" , type="Moderated", sponsorCountry="Bulgaristan", totalTime=10, indTime=60,SessionId=1},

            };
            //db.Motions.AddRange(_list);db.Complete();


            if (!db.Sessions.isSessionExsist(sessionID) || !db.Sessions.isUserHaveRightToAccess(sessionID,User.Identity.GetUserId()))
            {
                return Content("<h2>Error: Session do not exsist or you have not access to it </h2>");
            }

            //Viewbag Operations
            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.sessionID = sessionID;
           
            
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            return View(db.Motions.GetMotionsBySessionId(sessionID).ToList());
        }

        [HttpPost]
        [Route("{sessionIDParameter?}")]
        public ActionResult Index(string sessionIDParameter, string title, string type,  string country, float totalTime, float? indTime) {

            //string sessionIDParameter = "0";
            int sessionID = int.Parse(sessionIDParameter);
            var db = new UnitOfWork(new DatabaseContext());

            Motion newMotion = new Motion() { title = title, type = type, sponsorCountry = country, totalTime = totalTime, indTime = indTime, SessionId=sessionID };
            db.Motions.Add(newMotion);

            db.Complete();

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;

            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            //return View(db.Motions.GetMotionsBySessionId(sessionID).ToList());
            return Redirect("/"+ sessionID.ToString());
            
        }


        [Route("Home/Delete/{sessionIDParameter?}")]
        
        public ActionResult Delete(string sessionIDParameter)
        {
            int sessionID = int.Parse(sessionIDParameter);
            var db = new UnitOfWork(new DatabaseContext());
            db.Motions.DeleteFirst(sessionID);
            db.Complete();

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            //return View("Index", db.Motions.GetMotionsBySessionId(sessionID).ToList());
            return Redirect("/" + sessionID.ToString());
        }
        [Route("Home/Clear/{sessionIDParameter?}")]
        public ActionResult Clear(string sessionIDParameter)
        {

            int sessionID = int.Parse(sessionIDParameter);
            //int sessionID = 0;
            var db = new UnitOfWork(new DatabaseContext());
            db.Motions.RemoveRange(db.Motions.GetMotionsBySessionId(sessionID));
            db.Complete();

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            //return View("Index",db.Motions.GetMotionsBySessionId(sessionID).ToList());
            return Redirect("/" + sessionID.ToString());
        }
    }
}