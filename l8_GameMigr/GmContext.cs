using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l8_GameMigr
{
    public class GmContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GmContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB ;Initial Catalog= GameShop;Integrated Security=True");
        }
    }
}
