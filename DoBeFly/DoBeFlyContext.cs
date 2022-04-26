using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    public class DoBeFlyContext : DbContext
    {
        public DbSet<Flight> Flightset { get; set; }
        public DbSet<Pilot> PilotSet { get; set; }
        public DbSet<Passenger> PassengerSet { get; set; }
        public DbSet<Booking> BookingSet { get; set; }

        //SQL Express

        public static string ConnexionString { get; set; } = @"Server=(localDB)\MSSQLLocalDB;Database=DoBeFly;"+
                                                            "Trusted_Connection=True;App=EFCoreApp2021;MultipleActiveResultSets=true";


        public DoBeFlyContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(ConnexionString);

            //builder.UseLazyLoadingPoxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Booking>().HasKey(x => new { x.FlightNo, x.PassengerId });
        }

    }
}
