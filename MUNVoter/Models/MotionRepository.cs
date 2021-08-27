using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class MotionRepository : Repository<Motion>, IMotionRepository
    {
        public MotionRepository(DatabaseContext context) : base(context)
        {

        }

        public DatabaseContext DatabaseContext
        {
            get { return Context as DatabaseContext; }
        }

        public int GetMotionNumberBySessionId(int id)
        {
            return DatabaseContext.Motions.Where(i => i.SessionId == id).Count();
        }

        public IEnumerable<Motion> GetMotionsBySessionId(int id)
        {

            return Sort(DatabaseContext.Motions.Where(i => i.SessionId == id).ToList());
        }

        public void DeleteFirst(int SessionId)
        {
            List<Motion> sessionMotions = GetMotionsBySessionId(SessionId).ToList();
            sessionMotions = Sort(sessionMotions);
            Remove(sessionMotions[0]);
        }

        public void DeleteAllBySessionId(int SessionId)
        {
            List<Motion> sessionMotions = GetMotionsBySessionId(SessionId).ToList();
            RemoveRange(sessionMotions);
        }
        public List<Motion> Sort(List<Motion> unordered)
        {
            for (int i = 0; i < unordered.Count() - 1; i++)
            {
                for (int j = 1; j <= unordered.Count() - 1; j++)
                {
                    if (unordered[j - 1] < unordered[j])
                    {
                        var temp = unordered[j - 1];
                        unordered[j - 1] = unordered[j];
                        unordered[j] = temp;
                    }
                }
            }

            return unordered;
        }
    }
}