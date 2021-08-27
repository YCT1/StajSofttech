using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDesignPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContex _context;

        public UnitOfWork(DatabaseContex context)
        {
            _context = context;
            Students = new StudentRepository(_context);
        }

        public IStudentRepository Students { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
