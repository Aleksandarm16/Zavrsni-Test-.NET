using AleksandarMihaljev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleksandarMihaljev.Interface
{
    public interface IBusRepository
    {
        IEnumerable<Bus> GetAll();
        Bus GetId(int id);
        IEnumerable<Bus> GetAllSortedByYear();
        IEnumerable<Bus> GetAllSortedByYearWithParams(string type);
    }
}
