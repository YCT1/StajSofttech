using Microsoft.AspNet.Identity;
using MUNVoter.Identity;
using MUNVoter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MUNVoter.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        // GET: Session
        [HttpGet]
        public ActionResult Index()
        {
            var db = new UnitOfWork(new DatabaseContext());
            var sessionList =  db.Sessions.getUserSession(User.Identity.GetUserId());
            if(sessionList == null)
            {
                ViewBag.sesssionNumber = 0;
            }
            else
            {
                ViewBag.sessionNumber = sessionList.Count();
            }
            db.Complete();
            return View(sessionList);
        }

        [HttpPost]
        public ActionResult Index(string ConferenceName, string CommiteeName)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            Session newSession = new Session() {
                ConferenceName = ConferenceName,
                CommitteeName = CommiteeName,
                SessionCode = "tesyt",
                UserId = User.Identity.GetUserId(),
            };

            var db = new UnitOfWork(new DatabaseContext());
            db.Sessions.Add(newSession);
            db.Complete();
            var sessionList = db.Sessions.getUserSession(User.Identity.GetUserId());

            
            return View(sessionList);
        }

        public ActionResult Delete(int sessionId)
        {
            var db = new UnitOfWork(new DatabaseContext());
            db.Sessions.Remove(db.Sessions.findSessionById(sessionId));
            db.Complete();
            var sessionList = db.Sessions.getUserSession(User.Identity.GetUserId());
            return Redirect("Index");
        }
    }
}