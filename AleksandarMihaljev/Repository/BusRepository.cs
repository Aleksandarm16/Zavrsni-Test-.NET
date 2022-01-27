using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Repository
{
    public class BusRepository : IDisposable, IBusRepository
    {
        public ApplicationDbContext db = new ApplicationDbContext();

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


        public IEnumerable<Bus> GetAll()
        {
            return db.Buses.OrderBy(x=>x.Line);
        }

        public IEnumerable<Bus> GetAllSortedByYear()
        {
            return db.Buses.OrderBy(x => x.Year).ThenBy(x=>x.Line);
        }

        public IEnumerable<Bus> GetAllSortedByYearWithParams(string type)
        {
            return db.Buses.Where(x => x.Line == type).OrderByDescending(x => x.Year);
        }

        public Bus GetId(int id)
        {
            return db.Buses.FirstOrDefault(x => x.Id == id);
        }
    }
}