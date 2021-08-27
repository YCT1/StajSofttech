using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDesignPattern
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        
        public StudentRepository(DatabaseContex context): base(context)
        {
            
        }

        public Student GetTopGPAStudent()
        {
            return DatabaseContex.Students.OrderByDescending(i => i.GPA).First();

        }

        public IEnumerable<Student> GetTopGPAStudentAll()
        {
            return DatabaseContex.Students.OrderByDescending(i => i.GPA);

        }

        public Student GetGPA(int id)
        {
            return DatabaseContex.Students.Find(id);
        }


        public DatabaseContex DatabaseContex
        {
            get { return Context as DatabaseContex; }
        }
    }
}
