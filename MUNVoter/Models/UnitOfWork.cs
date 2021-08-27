using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Motions = new MotionRepository(_context);
        }

        public IMotionRepository Motions { get; private set; }

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