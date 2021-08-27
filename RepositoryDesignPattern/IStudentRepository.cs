using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDesignPattern
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetTopGPAStudent();

        IEnumerable<Student> GetTopGPAStudentAll();
        Student GetGPA(int id);
    }
}
