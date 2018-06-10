using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Wrzutka.Models.Wrzutka;

namespace Wrzutka.Data
{
    public class WrzutkaContext : DbContext
    {
        public WrzutkaContext() : base("DefaultConnection")
        {

        }

        public DbSet<File> Files { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}