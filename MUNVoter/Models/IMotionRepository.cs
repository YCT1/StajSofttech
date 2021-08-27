using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNVoter.Models
{
    public interface IMotionRepository : IRepository<Motion>
    {
        int GetMotionNumberBySessionId(int id);

        IEnumerable<Motion> GetMotionsBySessionId(int id);
        List<Motion> Sort(List<Motion> unordered);

        void DeleteFirst(int SessionId);
    }
}
