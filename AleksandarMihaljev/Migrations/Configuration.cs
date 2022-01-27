namespace AleksandarMihaljev.Migrations
{
    using AleksandarMihaljev.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AleksandarMihaljev.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AleksandarMihaljev.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Buses.AddOrUpdate(x => x.Id,
                new Bus() { Id=1,Line="2",Year=2008},
                new Bus() { Id=2, Line = "9A",Year=2020},
                new Bus() { Id=3, Line = "9",Year=2008}
                
                
                );
            context.Passengers.AddOrUpdate(x => x.Id,
                new Passenger() { Id=1,NameAndLastName="Jelena Petrovic",Adress="Narodnog fronta 6",Year=1987,CardType="Godisnja",BusId=1},
                new Passenger() { Id=2,NameAndLastName="Janko Lukic",Adress="Lasla Gala 15",Year=1997,CardType="Mesecna",BusId=3},
                new Passenger() { Id=3,NameAndLastName="Nikola Aleksic",Adress="Radnicka 8",Year=1986,CardType="Nedeljna",BusId=1},
                new Passenger() { Id=4,NameAndLastName="Luka Markovic",Adress="Temerinska 23",Year=1998,CardType="Nedeljna",BusId=2},
                new Passenger() { Id=5,NameAndLastName="Aleksandra Mirkovic",Adress="Marka Miljanova 38",Year=1985,CardType="Godisnja",BusId=3}

                
                );
            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
