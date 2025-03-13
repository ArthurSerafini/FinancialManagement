using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConstructor
{
     public class MyDbContext : DbContext
    {

        public DbSet<PaymentInfo> PaymentInfos { get; set; }

        public string DbPath { get; set; }

        public MyDbContext()
        {
            DbPath = "./db/paymentDB.db";
            //DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "db/paymentDB.db");
            //DbPath = "C:/Users/arthu/OneDrive/Documentos/EstudoCsharp/FinancialManagment/DatabaseApp/paymentDB.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
 }
