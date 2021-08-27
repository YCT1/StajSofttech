using MUNVoter.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(DatabaseContext context) : base(context)
        {

        }
        public DatabaseContext DatabaseContext
        {
            get { return Context as DatabaseContext; }
            
        }

        public IEnumerable<Session> getUserSession(string UserId) {
            // Check that user exsist in the list 
            IdentityDataContext Userdb = new IdentityDataContext();
            if(Userdb.Users.Find(UserId) != null)
            {
                return DatabaseContext.Sessions.Where(i => i.UserId==UserId).OrderByDescending(i=>i.Id);

            }
            return null;
        }

        public bool isSessionExsist(int Id)
        {
            return (DatabaseContext.Sessions.Find(Id) != null);
   
        }

        public bool isUserHaveRightToAccess(int Id, string UserId)
        {
            IdentityDataContext Userdb = new IdentityDataContext();
            if (Userdb.Users.Find(UserId) != null)
            {
                return (DatabaseContext.Sessions.Find(Id).UserId == UserId);

            }
            return false;
        }

        public Session findSessionById(int Id)
        {
            return DatabaseContext.Sessions.Where(i => i.Id == Id).FirstOrDefault();
        }

        public void editSession(int Id, string CommitteeName, string ConferenceName)
        {
            DatabaseContext.Sessions.Where(i => i.Id == Id).ToList().ForEach(i=>i.ConferenceName=ConferenceName);
            DatabaseContext.Sessions.Where(i => i.Id == Id).ToList().ForEach(i => i.CommitteeName = CommitteeName);
        }
    }
}