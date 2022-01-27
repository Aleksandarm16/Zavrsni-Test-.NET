using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Repository
{ 

    public class PassengerRepository : IDisposable, IPassengerRepository
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public void Add(Passenger passenger)
        {
            db.Passengers.Add(passenger);
            db.SaveChanges();
        }

        public void Delete(Passenger passenger)
        {

            db.Passengers.Remove(passenger);
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }


        public IQueryable<Passenger> GetAll()
        {
            return db.Passengers.OrderBy(x => x.NameAndLastName);
        }

        public IQueryable<BusSumDto> GetAllBussesAndPassengers()
        {
            IQueryable<Passenger> pass = GetAll();
            var result = pass.GroupBy(
                p => p.Bus,
                p => p.Id,

                (busses, id) => new BusSumDto()
                {
                    Id = busses.Id,
                    Year = busses.Year,
                    BusLine = busses.Line,
                    SumPassenger = id.Count()
                }).OrderByDescending(x => x.SumPassenger);
            return result;

        }

        public IQueryable<Passenger> GetAllByAdress(string adress)
        {
            return db.Passengers.Where(x=>x.Adress==adress).OrderBy(x=>x.Adress);
        }

        public Passenger GetId(int id)
        {
            return db.Passengers.FirstOrDefault(x=>x.Id==id);
        }

        public IQueryable<Passenger> SearchByYear(int Start, int End)
        {
            return db.Passengers.Where(x => x.Year >= Start && x.Year <= End).OrderBy(x=>x.Year);
        }

        public Passenger Update(Passenger passenger)
        {
            db.Entry(passenger).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
                return passenger;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

    }
}
