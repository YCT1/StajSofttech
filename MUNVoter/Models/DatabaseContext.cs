using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("DatabaseConnection")
        {
            
        }

        public DbSet<Motion> Motions { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}