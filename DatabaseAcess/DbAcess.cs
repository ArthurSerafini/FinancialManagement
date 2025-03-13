using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatabaseConstructor;

namespace DatabaseAcess
{
    public class DbAcess : MyDbContext
    {
        
        public string DbPath { get; }

        static void Main(string[] args)
        {
            Console.WriteLine("Acess Database");
        }

        public DbAcess()
        {
            DbPath = "/db/paymentDB.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
