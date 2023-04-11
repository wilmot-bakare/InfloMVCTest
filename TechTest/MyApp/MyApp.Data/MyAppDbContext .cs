using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class MyAppDbContext:DbContext
    {
        public MyAppDbContext() : base("name=MyAppConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
