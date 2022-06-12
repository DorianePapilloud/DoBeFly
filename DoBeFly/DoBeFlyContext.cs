using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    public class DoBeFlyContext : DbContext
    {
        public DbSet<Flight> FlightSet { get; set; }
        public DbSet<Pilot> PilotSet { get; set; }
        public DbSet<Passenger> PassengerSet { get; set; }
        public DbSet<Booking> BookingSet { get; set; }
        public DbSet<Booking> TicketSet { get; set; }

        public static readonly ILoggerFactory loggerFactory =
            LoggerFactory.Create(
            builder =>
            {
                builder.AddConsole();
            });

        //SQL Express

       public static string ConnexionString { get; set; } = "Data Source=153.109.124.35;Initial Catalog=DoBeFly;User ID=6231db;Password=Pwd46231.;Pooling=False";

        public DoBeFlyContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(ConnexionString);
            builder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
            //builder.UseLazyLoadingPoxies();
        }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Booking>().HasKey(x => new { x.BookingId, x.PassengerId });
        }*/

    }
}
