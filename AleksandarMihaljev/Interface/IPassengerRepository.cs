using AleksandarMihaljev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleksandarMihaljev.Interface
{
    public interface IPassengerRepository
    {
        IQueryable<BusSumDto> GetAllBussesAndPassengers();
        IQueryable<Passenger> GetAll();
        Passenger GetId(int id);
        IQueryable<Passenger> GetAllByAdress(string adress);
        void Add(Passenger passenger);
        Passenger Update(Passenger passenger);
        void Delete(Passenger passenger);

        IQueryable<Passenger> SearchByYear(int Start, int End);

    }
}
