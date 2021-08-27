using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork(new DatabaseContex()))
            {

                List<Student> myStudents = new List<Student>() {
                    new Student(){Name="Yekta",Surname="Tursun",Age=22,GPA=3.4F},
                    new Student(){Name="Cem",Surname="Tursun",Age=18,GPA=2.9F},
                    new Student(){Name="Ahmet",Surname="Maranki",Age=32,GPA=1.9F},
                    new Student(){Name="Fatmanur",Surname="Gündoğan",Age=17,GPA=0.1F},
                    new Student(){Name="Jesmine",Surname="Markson",Age=21,GPA=2.3F},
                };
                //unitOfWork.Students.AddRange(myStudents);
                


                Console.WriteLine("Most Succesfull student: {0}, {1}", unitOfWork.Students.GetTopGPAStudent().Name, unitOfWork.Students.GetTopGPAStudent().Surname);
                unitOfWork.Complete();
                Console.WriteLine("Success");
                Console.ReadLine();
            }
            
        }
    }
}
