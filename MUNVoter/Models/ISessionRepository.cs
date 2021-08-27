using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNVoter.Models
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> getUserSession(string UserId);

        bool isSessionExsist(int Id);

        bool isUserHaveRightToAccess(int Id,string UserId);

        Session findSessionById(int Id);

        void editSession(int Id, string CommitteeName, string ConferenceName);
    }
}
