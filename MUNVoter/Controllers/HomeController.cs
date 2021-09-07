using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MUNVoter.Models;
using Microsoft.AspNet.Identity;
using MUNVoter.Hubs;

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
            int sessionID;
            if(!int.TryParse(sessionIDParameter, out sessionID))
            {
                return Redirect("session/index");
            }
            // Old one
            /*
            ViewBag.motionNumber = Database.Motions.Count;
            DatabaseContext db = new DatabaseContext();
           
            return View(Database.Motions);
            */

            //int sessionID = 0;
            var db = new UnitOfWork(new DatabaseContext());
            /*
            List<Motion> _list = new List<Motion>()
            {
                new Motion(){title="Discussing Women Rights in Afganistan" , type="Moderated", sponsorCountry="Turkey", totalTime=10, indTime=60,SessionId=0},
                new Motion(){title="Liberation of South Sudan" , type="Moderated", sponsorCountry="France", totalTime=11, indTime=60,SessionId=0},
                new Motion(){title="Decline of life expectance" , type="Moderated", sponsorCountry="United Kingdom", totalTime=10, indTime=90,SessionId=1},
                new Motion(){title="Working on the Draft Resolution" , type="Unmoderated", sponsorCountry="USA", totalTime=10, indTime=0,SessionId=0},
                new Motion(){title="Prev Motion" , type="Extention", sponsorCountry="Tuvalu", totalTime=5, indTime=60,SessionId=0},
                new Motion(){title="EU Relation with Taliban" , type="Moderated", sponsorCountry="Bulgaristan", totalTime=10, indTime=60,SessionId=1},

            };*/
            //db.Motions.AddRange(_list);db.Complete();
            //CountryFlag test = new CountryFlag() { CountryName = "Turkey", CountryCode = "TR", ImgCode = "TR.jpg" };
            //db.CountryFlags.Add(test); db.Complete();

            if (!db.Sessions.isSessionExsist(sessionID))
            {
                return Content("<h2>Error: Session do not exsist or you have not an access right to it </h2>");
            }else if (!db.Sessions.isUserHaveRightToAccess(sessionID, User.Identity.GetUserId()))
            {
                return Redirect("View/Index/" + sessionID);
            }

            //Viewbag Operations
            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.sessionID = sessionID;
           
            
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;

            List<Motion> motions = db.Motions.GetMotionsBySessionId(sessionID).ToList();
            ViewBag.countryImg = db.CountryFlags.FindImageAddressesByMotions(motions);
            return View(motions);
        }

        [HttpPost]
        [Route("{sessionIDParameter?}")]
        public ActionResult Index(string sessionIDParameter, string title, string type,  string country, float? totalTime, float? indTime, string titleVoting, string titlePaper, string titleDebate) {

            //string sessionIDParameter = "0";
            int sessionID = int.Parse(sessionIDParameter);
            var db = new UnitOfWork(new DatabaseContext());

            if(type == "Voting")
            {
                title = titleVoting;
            }else if(type == "Paper")
            {
                title = titlePaper;
            }else if(type == "Debate")
            {
                title = titleDebate;
            }

            // Let's check if the user enter country code as two letter
            if(country.Length == 2)
            {
                country = db.CountryFlags.FindCountryNameByCode(country);
            }
            Motion newMotion = new Motion() { title = title, type = type, sponsorCountry = country, totalTime = totalTime, indTime = indTime, SessionId=sessionID };
            newMotion.calculateOrder();
            db.Motions.Add(newMotion);
            
            db.Complete();

            //SessionHub.BroadcastData();
            SessionHub.BroadCastDataSpesfic(sessionID);

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;

            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            //return View(db.Motions.GetMotionsBySessionId(sessionID).ToList());
            return Redirect("/"+ sessionID.ToString());
            
        }


        [Route("Home/Delete/{sessionIDParameter?}")]
        [Authorize]
        public ActionResult Delete(string sessionIDParameter)
        {
            int sessionID = int.Parse(sessionIDParameter);
            var db = new UnitOfWork(new DatabaseContext());

            // Firtly let's check that autonticated user is the owener of the user, 
            // If not, let's redirect back to view only page
            Session thisSession = db.Sessions.findSessionById(sessionID);
            if(thisSession.UserId != User.Identity.GetUserId())
            {
                return Redirect("/" + sessionID.ToString());
            }
            db.Motions.DeleteFirst(sessionID);
          
            db.Complete();

            //SessionHub.BroadcastData();
            SessionHub.BroadCastDataSpesfic(sessionID);

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

            // Firtly let's check that autonticated user is the owener of the user, 
            // If not, let's redirect back to view only page
            Session thisSession = db.Sessions.findSessionById(sessionID);
            if (thisSession.UserId != User.Identity.GetUserId())
            {
                return Redirect("/" + sessionID.ToString());
            }

            db.Motions.RemoveRange(db.Motions.GetMotionsBySessionId(sessionID));
            db.Complete();

            //SessionHub.BroadcastData();
            SessionHub.BroadCastDataSpesfic(sessionID);

            ViewBag.motionNumber = db.Motions.GetMotionNumberBySessionId(sessionID);
            ViewBag.SessionID = sessionID;
            ViewBag.ConferenceName = db.Sessions.findSessionById(sessionID).ConferenceName;
            ViewBag.ComitteeName = db.Sessions.findSessionById(sessionID).CommitteeName;
            //return View("Index",db.Motions.GetMotionsBySessionId(sessionID).ToList());
            return Redirect("/" + sessionID.ToString());
        }

        [HttpPost]
        [Route("Home/Edit")]
        public ActionResult Edit(string sessionIDParameter, int id, string title, string type, string country, float? totalTime, float? indTime)
        {
            int sessionID = int.Parse(sessionIDParameter);
            var db = new UnitOfWork(new DatabaseContext());

            // Firtly let's check that autonticated user is the owener of the user, 
            // If not, let's redirect back to view only page
            Session thisSession = db.Sessions.findSessionById(sessionID);
            if (thisSession.UserId != User.Identity.GetUserId())
            {
                return Redirect("/" + sessionID.ToString());
            }


            db.Motions.Remove(db.Motions.Get(id));

            if (country.Length == 2)
            {
                country = db.CountryFlags.FindCountryNameByCode(country);
            }

            Motion newMotion = new Motion() { title = title, type = type, sponsorCountry = country, totalTime = totalTime, indTime = indTime, SessionId = sessionID };
            db.Motions.Add(newMotion);
            db.Complete();
            SessionHub.BroadCastDataSpesfic(sessionID);
            return Redirect("/" + sessionID.ToString());
        }

        [Authorize]
        public ActionResult DeleteMotion(string sessionIDParameter, int id)
        {
            var db = new UnitOfWork(new DatabaseContext());
            int sessionID = int.Parse(sessionIDParameter);

            // Firtly let's check that autonticated user is the owener of the user, 
            // If not, let's redirect back to view only page
            Session thisSession = db.Sessions.findSessionById(sessionID);
            if (thisSession.UserId != User.Identity.GetUserId())
            {
                return Redirect("/" + sessionID.ToString());
            }

            db.Motions.Remove(db.Motions.Get(id));
            db.Complete();
            SessionHub.BroadCastDataSpesfic(sessionID);
            return Redirect("/" + sessionID.ToString());
            
            
        }


    }
}